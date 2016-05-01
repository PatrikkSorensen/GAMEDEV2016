using UnityEngine;
using System.Collections;

public class ChanelEnergyScript : MonoBehaviour {

    public float chanelTime; 
	
    private GameObject B4, MiMi;
    private PlayerStatusScript B4Status, MiMiStatus;

    // Helper variables 
    bool canChanel, channelling, isChanneled;
    float startTime;  
          
	void Start () {
        B4 = GameObject.FindGameObjectWithTag("B4");
        MiMi = GameObject.FindGameObjectWithTag("MiMi");
        B4Status = B4.GetComponent<PlayerController>().playerStatus;
        MiMiStatus = MiMi.GetComponent<PlayerController>().playerStatus;

        canChanel = !B4Status.getChannelStatus();
    }
	
	void Update () {
        CheckChanel();

        if (isChanneled)
        {
            B4Status.setChannelStatus(true);
            MiMiStatus.setChannelStatus(true); 
        }
	}

    void CheckChanel()
    {
        if (canChanel && !isChanneled)
        {

            if (Input.GetButtonDown("Channelling") && canChanel && B4Status.getBondStatus())
            {
                startTime = Time.time;
                channelling = true;
            }

            if (Input.GetButtonUp("Channelling"))
            {
                float timeDifference = Time.time - startTime;
                channelling = false;
                startTime = 0;
            }

            if (channelling)
            {
                ChanelEnergy();
            }
        }
    }

    void ChanelEnergy()
    {
        float timeDifference = Time.time - startTime;

        if (timeDifference > chanelTime && timeDifference < chanelTime + 0.2f)
        {
            Debug.Log("Channelled for " + chanelTime + " seconds");
            canChanel = false;
            channelling = false;
            isChanneled = true;
        }
        else
        {
            Debug.Log("timeDifference: " + timeDifference + ", is less than 3.0f");
        }
    }
}
