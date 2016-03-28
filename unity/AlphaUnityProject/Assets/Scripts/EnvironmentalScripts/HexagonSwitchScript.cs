using UnityEngine;
using System.Collections;

public class HexagonSwitchScript : MonoBehaviour {

    public float lightIntensity = 5.0f;
    //public PlatformPuzzleEvent puzzleEvent; 
    private bool canChanel, channeling, hasPlayer, isActive = false; 
    private GameObject lightGameObject, player;
    private float startTime = 0.0f;
    private AudioSource[] audioSources; // 0 = song, 1 = beamup, 2 = beamdown, 3 = channelling, 4 = pull, 5 = combine
    private GameObject B4, MiMi;
    private ParticleSystem[] particles1, particles2;



    void Update()
    {
        if (hasPlayer && player != null)
        {
            if (player.GetComponent<PlayerController>().playerStatus.getBondStatus() && !canChanel)
            {
                LightPlatform();
                player.GetComponent<PlayerController>().playerStatus.setChannelStatus(true);
            } else
            {
                //Debug.Log("Bond status: " + player.GetComponent<PlayerController>().playerStatus.getBondStatus());
            }

            if (Input.GetButtonDown("Channelling") && canChanel)
            {
                startTime = Time.time;
                channeling = true;
                Debug.Log("Setting start timer to: " + startTime);
                audioSources[3].Play();
                //TODO: MiMis particles dont play (particles2). FIX IIIIT 
                particles1[0].Stop();
                particles1[1].Stop();
                //particles2[1].Stop();
                //particles2[2].Stop();

                particles1[1].Play();
                particles1[0].Play();
                //particles2[1].Play();
                //particles2[2].Play();
                
            }

            if (Input.GetButtonUp("Channelling"))
            {
                float timeDifference = Time.time - startTime;
                if (timeDifference < 3.0f)
                {
                    audioSources[3].Stop();
                }
                particles1[0].Stop();
                particles1[1].Stop();
                //particles2[1].Stop();
                //particles2[2].Stop();

                Debug.Log("End of channelling");
                channeling = false;
                startTime = 0;
            }

            if (channeling)
            {
                ChanelEnergyOnPlatform(); 
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered");
        hasPlayer = true; 

        if (other.tag == "B4"|| other.tag == "MiMi")
            player = other.gameObject; 
    }

    void OnTriggerExit(Collider other)
    {
        hasPlayer = false;

        if (other.tag == "B4" || other.tag == "MiMi")
        {
            Destroy(lightGameObject);
            canChanel = false;
            other.GetComponent<PlayerController>().playerStatus.setChannelStatus(false);
        }
    }

    void LightPlatform()
    { 
        lightGameObject = new GameObject("The Light");
        lightGameObject.AddComponent<Light>();
        Light light = lightGameObject.GetComponent<Light>();
        light.transform.position = transform.position + Vector3.up;
        light.intensity = lightIntensity;
        canChanel = true;
    }

    void ChanelEnergyOnPlatform()
    {
        float timeDifference = Time.time - startTime;

        if (timeDifference > 3.0f && timeDifference < 3.2f && channeling)
        {
            Debug.Log("Channelled for three seconds");
            isActive = true; 
            channeling = false;
        }
        else
        {
            Debug.Log("timeDifference: " + timeDifference + ", is less than 3.0f");

        }
    }

    public bool getStatus()
    {
        return isActive;
    }

    void Start()
    {
        B4 = GameObject.FindGameObjectWithTag("B4");
        MiMi = GameObject.FindGameObjectWithTag("MiMi");
        GameObject controller = GameObject.FindGameObjectWithTag("AudioController");
        audioSources = controller.GetComponents<AudioSource>();
        particles1 = B4.GetComponentsInChildren<ParticleSystem>();
        //particles2 = MiMi.GetComponentsInChildren<ParticleSystem>();
        //particles2 = GameObject.Find("ChannellingParticlesMiMi").GetComponents<ParticleSystem>(); 
        //Debug.Log("Hello this is length of particles array: " + particles1.Length);
    }
}
