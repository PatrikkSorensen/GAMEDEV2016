using UnityEngine;
using System.Collections;

public class ChangeRotationPoint : MonoBehaviour {

    public RotatePoint rotatePoint;
    public GameObject otherSwitch;
    public ParticleSystem activeParticles;
    public bool isActive, canChanel, channelling, isLocked;
    public float chanelTime; 

    private float startTime = 0.0f; 


    void Update()
    {
        if (isLocked)
            return; 

        if (isActive)
            activeParticles.Play();
        else
            activeParticles.Stop();

        if (canChanel)
        {
            if (Input.GetButtonDown("Channelling") && canChanel)
            {
                startTime = Time.time;
                channelling = true;
                //sparkParticles.Play();
            }

            if (Input.GetButtonUp("Channelling"))
            {
                channelling = false;
                startTime = 0;
                //sparkParticles.Stop();

                //if (chanelSource.isPlaying)
                //    chanelSource.Stop();
            }

            if (channelling)
            {
                //if (!chanelSource.isPlaying)
                //    chanelSource.Play();

                ChanelEnergyOnPlatform();
            }
        }


    }
    void OnTriggerStay(Collider other) 
    {
        //if (isActive)
            if (other.tag == "MiMi" || other.tag == "B4")
                canChanel = true;
    }

    void OnTriggerExit(Collider other)
    {

            if (other.tag == "MiMi" || other.tag == "B4")
                canChanel = false;
    }

    void ChanelEnergyOnPlatform()
    {
        float timeDifference = Time.time - startTime;

        if (timeDifference > chanelTime && timeDifference < chanelTime + 0.2f)
        {
            rotatePoint.ChangeEndPoint(otherSwitch, rotatePoint.changeDuration);
            otherSwitch.GetComponent<ChangeRotationPoint>().isActive = true;
            isActive = false;
            GetComponent<AudioSource>().Play(); 
        }
        else
        {
            Debug.Log("timeDifference: " + timeDifference + ", is less than 3.0f");
        }
    }

    public void LockSwitches()
    {
        otherSwitch.GetComponent<ChangeRotationPoint>().isLocked = true; 
        isLocked = true; 

    }
}
