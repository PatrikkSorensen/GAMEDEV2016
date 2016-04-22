using UnityEngine;
using System.Collections;

public class LightStationScript : MonoBehaviour {

    public ParticleSystem sparkParticles, haloParticles;

    private bool canChanel, channeling, isActive, b = false;
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
            if (Input.GetButtonDown("Channelling") && canChanel && B4Controller.isBonded())
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

            canChanel = true;

    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "MiMi")
        {
            canChanel = false;
        }
    }
    void TestFunction()
    {
        bool b = false;
        if (!b)
        {
            IsActive = true;
            b = true;
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
            channeling = false;
            IsActive = true;
        }
        else
        {
            Debug.Log("timeDifference: " + timeDifference + ", is less than 3.0f");
        }
    }

    public bool IsActive
    {
        get
        {
            return isActive;
        }

        set
        {
            isActive = value;
        }

    }
}
