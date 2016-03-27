using UnityEngine;
using System.Collections;

public class HexagonSwitchScript : MonoBehaviour {

    public float lightIntensity = 5.0f;
    //public PlatformPuzzleEvent puzzleEvent; 
    private bool canChanel, channeling, hasPlayer, isActive = false; 
    private GameObject lightGameObject, player;
    private float startTime = 0.0f;
    private AudioSource[] audioSources; // 0 = song, 1 = beamup, 2 = beamdown, 3 = channelling, 4 = pull, 5 = combine




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
                
            }

            if (Input.GetButtonUp("Channelling"))
            {
                float timeDifference = Time.time - startTime;
                if (timeDifference < 3.0f)
                {
                    audioSources[3].Stop();
                }

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
        GameObject controller = GameObject.FindGameObjectWithTag("AudioController");
        audioSources = controller.GetComponents<AudioSource>();

    }
}
