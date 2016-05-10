using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class LightStationScript : MonoBehaviour {

    public ParticleSystem sparkParticles, haloParticles;
    public Color materialEndColor, emissionColor; 
    public float chanelTime, materialFadeInTime = 3.0f;
    public List<GameObject> AIHelpers = new List<GameObject>();
    public GameObject curcuitLines, powerSphere, meshLine, doorTrigger;

    public AudioClip channellingClip, sucessClip;
    private AudioSource chanelSource, sucessSource; 

    private bool canChanel, channelling, isActive, b = false;
    private PlayerStatusScript B4Status, MiMiStatus;
    private float startTime = 0.0f;
    private GameObject m_audioObject; 

    public bool IsActive
    {
        get { return isActive; }
        set { isActive = value;}
    }


    void Start()
    {
        AssignAudioObject(); 

        B4Status = GameObject.FindGameObjectWithTag("B4").GetComponent<PlayerStatusScript>();
        MiMiStatus = GameObject.FindGameObjectWithTag("MiMi").GetComponent<PlayerStatusScript>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.V))
        {
            ChangeSphereMaterial(); 

        }
        // TODO: Refactor this
        if (canChanel && !isActive)
        {
            if (Input.GetButtonDown("Channelling") && canChanel && B4Status.getBondStatus())
            {
                startTime = Time.time;
                channelling = true;
                sparkParticles.Play();
            }

            if (Input.GetButtonUp("Channelling"))
            {
                //float timeDifference = Time.time - startTime;
                channelling = false;
                startTime = 0;
                sparkParticles.Stop();

                if (chanelSource.isPlaying)
                    chanelSource.Stop();
            }

            if (channelling)
            {
                if(!chanelSource.isPlaying)
                    chanelSource.Play();
                 
                ChanelEnergyOnPlatform();
            }
        }
    }

    void OnTriggerStay(Collider other)
    {

            canChanel = true;

    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "MiMi")
        {
            canChanel = false;
        }
    }

    void ChanelEnergyOnPlatform()
    {
        float timeDifference = Time.time - startTime;

        if (timeDifference > chanelTime && timeDifference < chanelTime + 0.2f)
        {
            Debug.Log("Channelled for three seconds");
            if(doorTrigger != null)
            { 
                doorTrigger.GetComponent<DoorSocketScript>().incrementEnergy();
            }


            sparkParticles.Play();
            haloParticles.Play();
            canChanel = false;
            channelling = false;
            IsActive = true;

            // Sounds 
            sucessSource.Play();  
            // Activate events
            ActivateAgents();
            ActivateLines();

            // Materials and visuals
           ChangeSphereMaterial();

        }
        else
        {
            Debug.Log("timeDifference: " + timeDifference + ", is less than 3.0f");
        }
    }

    void ChangeSphereMaterial()
    {
        Material sphereMaterial = powerSphere.GetComponent<MeshRenderer>().material;
        Material lineMaterial = meshLine.GetComponent<MeshRenderer>().materials[1];

        sphereMaterial.DOColor(materialEndColor, materialFadeInTime);

        Color startEColor = sphereMaterial.GetColor("_EmissionColor");

        Debug.Log("E color: " + startEColor);
        StartCoroutine(FadeInEmmision(sphereMaterial, startEColor, emissionColor));
    }

    IEnumerator FadeInEmmision(Material m, Color startColor, Color endColor)
    {
        float incrementor = 0.01f; 
        for(float f = startColor.grayscale; f < endColor.grayscale; f += incrementor)
        {
            startColor = startColor + new Color(incrementor, incrementor, incrementor);
            m.SetColor("_EmissionColor", startColor);
            yield return new WaitForSeconds(0.01f); 

        }

    }

    void ActivateAgents()
    {
        foreach (GameObject gb in AIHelpers)
        {
            Agent agent = gb.GetComponent<Agent>();
            agent.ActivateAgent(); 
        }
    }

    void ActivateLines()
    {
        if (curcuitLines)
            curcuitLines.GetComponent<CurcuitLines>().ChanelLines();
        else
            Debug.LogWarning("There are no curcuitlines script to the gameobject specified");
    }

    void AssignAudioObject()
    {
        m_audioObject = new GameObject();
        m_audioObject.name = "lightstation_AudioObject";
        m_audioObject.transform.parent = transform;

        chanelSource = m_audioObject.AddComponent<AudioSource>();
        chanelSource.clip = channellingClip; 

        sucessSource = m_audioObject.AddComponent<AudioSource>();
        sucessSource.clip = sucessClip; 
    }
}
