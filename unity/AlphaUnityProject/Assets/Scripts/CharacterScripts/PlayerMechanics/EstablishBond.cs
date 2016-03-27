using UnityEngine;
using System.Collections;

public class EstablishBond : MonoBehaviour {

    //TODO: Consider if category values should be in a class... 
    [SerializeField]
    public float bondWidthBegin = 0.2f;

    [SerializeField]
    public float bondWidthEnd = 0.4f;

    [SerializeField]
    public float damper = 0.1f;

    [SerializeField]
    public float maxSpringDistance = 10.0f;

    public AudioClip clip;

    private GameObject B4, MiMi;

    private bool bondEstablished, chargeBond = false;
    //TODO: Make time variables public and editable
    private float startTime = 0.0f;

    private LightScript ls;
    private AudioSource[] audioSources; // 0 = song, 1 = beamup, 2 = beamdown, 3 = channelling, 4 = pull, 5 = combine
    private Light lightSource;
    private PlayerStatusScript B4Status;
    private PlayerStatusScript MiMiStatus;

    void Awake()
    {
        B4 = GameObject.FindGameObjectWithTag("B4");
        MiMi = GameObject.FindGameObjectWithTag("MiMi");
        GameObject controller = GameObject.FindGameObjectWithTag("AudioController");
        //audioSources = controller.GetComponents<AudioSource>();
        B4Status = B4.GetComponent<PlayerController>().playerStatus;
        MiMiStatus = MiMi.GetComponent<PlayerController>().playerStatus;
        gameObject.AddComponent<LightScript>();
        gameObject.AddComponent<Light>(); 
        ls = GetComponent<LightScript>();
        lightSource = GetComponent<Light>(); 
        ls.enabled = false;
        lightSource.enabled = false; 

    }

    void Update()
    {
        if (Input.GetButtonDown("EstablishBond") && !GetComponent<LineRenderer>() && !chargeBond)
        {
            ls.enabled = true;
            ls.minIntensity = 1;
            ls.maxIntensity = 6;
            ls.pulseSpeed = 2;
            ls.color = Color.white;
            lightSource.enabled = true;
            startTime = Time.time;
            chargeBond = true;
            //audioSources[3].Play();
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
                //audioSources[3].Stop();
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

    void CreateBond()
    {
        if (!gameObject.GetComponent<LineRenderer>())
        {
            // Visuals: 
            gameObject.AddComponent<LineRenderer>();
            LineRenderer lr = GetComponent<LineRenderer>();
            lr.SetWidth(bondWidthBegin, bondWidthEnd);
            Vector3[] points = { B4.transform.position + new Vector3(0.0f, 2.0f, 0.0f), MiMi.transform.position + new Vector3(0.0f, 2.0f, 0.0f) };
            lr.SetPositions(points);
            bondEstablished = true;

            // Springjoint: 
            gameObject.AddComponent<SpringJoint>();
            SpringJoint joint = GetComponent<SpringJoint>();
            joint.connectedBody = MiMi.GetComponent<Rigidbody>(); 
            joint.maxDistance = maxSpringDistance;
            joint.damper = damper;
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = new Vector3(-4, 0, 0);

            // Light
            ls.enabled = false;
            //gameObject.GetComponent<Light>().enabled = false;

            // Updating playerStatusScript 
            // TODO: make it happen on both players 
            B4Status = B4.GetComponent<PlayerController>().playerStatus;
            MiMiStatus = MiMi.GetComponent<PlayerController>().playerStatus;
            B4Status.setBondStatus(true);
            MiMiStatus.setBondStatus(true);
            //audioSources[1].Play();
        }
    }

    void UpdateBond()
    {
        LineRenderer lr = gameObject.GetComponent<LineRenderer>();
        Vector3[] points = { B4.transform.position, MiMi.transform.position };
        lr.SetPositions(points);
    } 

    void DestroyBond()
    {
        Destroy(gameObject.GetComponent<LineRenderer>());
        Destroy(gameObject.GetComponent<SpringJoint>());
        B4Status.setBondStatus(false);
        MiMiStatus.setBondStatus(false);
        //audioSources[2].Play();
    }
}
