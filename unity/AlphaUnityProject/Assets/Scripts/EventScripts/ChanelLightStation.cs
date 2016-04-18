using UnityEngine;
using System.Collections;

public class ChanelLightStation : MonoBehaviour {

    public ParticleSystem sparkParticles, haloParticles;

    private bool canChanel, channeling, isActive = false;
    private PlayerController B4Controller, MiMiController;
    private float startTime = 0.0f;

    void Start()
    {
        B4Controller = GameObject.FindGameObjectWithTag("B4").GetComponent<PlayerController>();
        MiMiController = GameObject.FindGameObjectWithTag("MiMi").GetComponent<PlayerController>();

    }

    void Update()
    {
        if (canChanel && !isActive)
        {
            if (Input.GetButtonDown("Channelling") && canChanel)
            {
                startTime = Time.time;
                channeling = true;
                sparkParticles.Play();
            }

            if (Input.GetButtonUp("Channelling"))
            {
                float timeDifference = Time.time - startTime;
                channeling = false;
                startTime = 0;
                sparkParticles.Stop();
            }

            if (channeling)
            {
                ChanelEnergyOnPlatform();
            }
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "MiMi" && MiMiController.isBonded())
        {
            Debug.Log("MiMi is triggered and bonded");
            canChanel = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "MiMi")
        {
            Debug.Log("Exited trigger");
            canChanel = false;
        }
    }

    void ChanelEnergyOnPlatform()
    {
        float timeDifference = Time.time - startTime;

        if (timeDifference > 3.0f && timeDifference < 3.2f && channeling)
        {
            Debug.Log("Channelled for three seconds");
            sparkParticles.Play();
            haloParticles.Play();
            canChanel = false; 
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


}
