﻿using UnityEngine;
using System.Collections;

public class EstablishBond : MonoBehaviour {

    public Material lineMaterial; 
    public float bondWidthBegin = 0.3f;
    public float bondWidthEnd = 0.3f;
    public float bondCubeMass = 0.1f; 

    public float damper = 0.2f;
    public float tolerance = 1.0f; 
    public float springPower = 10.0f;

    public float maxSpringDistance = 10.0f;
    public AudioClip bondclip, successClip, destroyClip;

    private GameObject B4, MiMi, audioController;
    private bool bondEstablished, chargeBond = false;
    private float startTime = 0.0f;

    private LightScript ls;
    private AudioSource bondAudio; 
    private Light lightSource;
    private PlayerStatusScript B4Status;
    private PlayerStatusScript MiMiStatus;

	public Shader shader2;

    void Start()
    {
        B4 = GameObject.FindGameObjectWithTag("B4");
        MiMi = GameObject.FindGameObjectWithTag("MiMi");
        B4Status = B4.GetComponent<PlayerController>().playerStatus;
        MiMiStatus = MiMi.GetComponent<PlayerController>().playerStatus;

    }

    void Awake()
    {
        // Light
        gameObject.AddComponent<LightScript>();
        gameObject.AddComponent<Light>();
        ls = GetComponent<LightScript>();
        lightSource = GetComponent<Light>();
        ls.enabled = false;
        lightSource.enabled = false;

        // Sound 
        audioController = GameObject.FindGameObjectWithTag("AudioController");
        if (!audioController)
        {
            Debug.LogWarning("No audio Controller found. Creating one");
            audioController = new GameObject();
            audioController.name = "AudioController";
            audioController.tag = "AudioController";
        }
        bondAudio = audioController.AddComponent<AudioSource>();
        bondAudio.clip = bondclip;
    }

    void Update()
    {
        if (Input.GetButtonDown("EstablishBond") && !GetComponent<LineRenderer>() && !chargeBond)
        {
            ls.enabled = true;
            ls.minIntensity = 1;
            ls.maxIntensity = 6;
            ls.pulseSpeed = 2;
			ls.color = new Color(1, 100, 200, 0);
            lightSource.enabled = true;
            startTime = Time.time;
            chargeBond = true;
            bondAudio.Play(); 
        }

        float timeDifference = Time.time - startTime;
        if (Input.GetButtonUp("EstablishBond"))
        {
            chargeBond = false;
            lightSource.enabled = false;
            ls.enabled = false; 
            startTime = 0;
            if (timeDifference < 3.0f)
            {
                bondAudio.Stop();
            }
        }

        if (timeDifference >= 3.0f && timeDifference < 3.2f && chargeBond)
        {
            CreateBond();
            chargeBond = false; 
        }

        if (bondEstablished && GetComponent<LineRenderer>())
        {
            UpdateBond(); 
        }

        if(Input.GetKey(KeyCode.Q) && GetComponent<LineRenderer>())
        {
            DestroyBond(); 
        }
    }

    public void CreateBond()
    {
        if (!gameObject.GetComponent<LineRenderer>())
        {
            // Visuals: 
            gameObject.AddComponent<LineRenderer>();
            LineRenderer lr = GetComponent<LineRenderer>();

            // Linerenderer: 
            lr.material = lineMaterial;
            lr.SetWidth(bondWidthBegin, bondWidthEnd);
            Vector3[] points = { B4.transform.position + new Vector3(0.0f, 6.0f, 0.0f), MiMi.transform.position + new Vector3(0.0f, 6.0f, 0.0f) };
            lr.SetPositions(points);
            bondEstablished = true;

            // Springjoint: 
            GameObject cube = CreateSpringCube();
            CreateSpringJoint(B4, cube.GetComponent<Rigidbody>());
            CreateSpringJoint(MiMi, cube.GetComponent<Rigidbody>());

            // Light
            ls.enabled = false;

            // Updating playerStatusScript 
            B4Status.setBondStatus(true);
            MiMiStatus.setBondStatus(true);

            //Audio 
            AudioSource sucessAudioSource = audioController.AddComponent<AudioSource>();
            sucessAudioSource.clip = successClip;
            sucessAudioSource.Play(); 
        }
    }

    GameObject CreateSpringCube()
    {
        GameObject gb = new GameObject();
        gb.name = "BondCube";
        gb.transform.position = new Vector3(0.0f, 0.5f, 0.0f) + ((MiMi.transform.position + transform.position) / 2.0f);
        gb.AddComponent<BondCubeScript>();
        SphereCollider sc = gb.AddComponent<SphereCollider>();
        //BoxCollider bc = gb.AddComponent<BoxCollider>();
        //bc.size = new Vector3(0.5f, 0.5f, 0.5f);
        Rigidbody rb = gb.AddComponent<Rigidbody>();
        rb.mass = bondCubeMass; 
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ; 

        return gb; 
    }

    void CreateSpringJoint(GameObject origin, Rigidbody connectedBody)
    {
        Debug.Log("Creating spring joint");
        SpringJoint joint = origin.AddComponent<SpringJoint>();
        joint.connectedBody = connectedBody;
        joint.spring = springPower;
        joint.maxDistance = maxSpringDistance;
        joint.damper = damper;
        joint.tolerance = tolerance; 
        //joint.autoConfigureConnectedAnchor = false;
        //joint.connectedAnchor = new Vector3(-4, 0, 0);
    }

    void UpdateBond()
    {
        LineRenderer lr = gameObject.GetComponent<LineRenderer>();
        Vector3[] points = { B4.transform.position + new Vector3(0.0f, 0.3f, 0.0f), MiMi.transform.position + new Vector3(0.0f, 0.3f, 0.0f) };
        lr.SetPositions(points);
    } 

    void DestroyBond()
    {
        Destroy(gameObject.GetComponent<LineRenderer>());
        Destroy(gameObject.GetComponent<SpringJoint>());
        B4Status.setBondStatus(false);
        MiMiStatus.setBondStatus(false);
        //audioSources[2].Play();

        AudioSource destroyAudioSource = audioController.AddComponent<AudioSource>();
        destroyAudioSource.clip = destroyClip;
        destroyAudioSource.Play();
    }

    public bool isBonded()
    {
        return bondEstablished;
    }

    void CreateParticles()
    {

    }
}
