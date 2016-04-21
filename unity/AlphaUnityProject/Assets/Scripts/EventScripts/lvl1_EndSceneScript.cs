﻿using UnityEngine;
using System.Collections;

public class lvl1_EndSceneScript : MonoBehaviour {

    bool canChanel, channeling = false; 
    int timesChanelled = 0;
    float startTime = 0.0f; 

    void Update()
    {
        if (Input.GetButtonDown("Channelling") && canChanel)
        {
            startTime = Time.time;
            channeling = true; 


        }

        if (Input.GetButtonUp("Channelling") && canChanel)
        {
            Debug.Log("Stopped channeling");
            channeling = false;
        }


        if (channeling)
        {
            ChanelEnergy();
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "MiMi")
        {
            canChanel = true; 

        }
    }

    void OnTriggerExit()
    {
        canChanel = false; 
    }

    void ChanelEnergy()
    {
        Debug.Log("Trying to chanel energy");
        float timeDifference = Time.time - startTime;

        if (timeDifference > 3.0f && timeDifference < 3.2f)
        {
            Debug.Log("Channelled for three seconds");
            channeling = false;
        }
        else
        {
            Debug.Log("timeDifference: " + timeDifference + ", is less than 3.0f or greater than 3.2");
        }
    }


    void switchLevel()
    {
        // Application.load(); 
    }
}
