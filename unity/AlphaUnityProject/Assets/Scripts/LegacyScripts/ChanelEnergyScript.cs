using UnityEngine;
using System.Collections;

public class ChanelEnergyScript : MonoBehaviour {

    public float chanelTime; 
	
    private GameObject B4, MiMi;
    private PlayerStatusScript B4Status, MiMiStatus;

    // Helper variables 
    bool canEmpower, channelling, isChanneled;
    float startTime;  
          
	void Start () {
        B4 = GameObject.FindGameObjectWithTag("B4");
        MiMi = GameObject.FindGameObjectWithTag("MiMi");
        B4Status = B4.GetComponent<PlayerController>().playerStatus;
        MiMiStatus = MiMi.GetComponent<PlayerController>().playerStatus;

        canEmpower = B4Status.getCanEmpowerStatus();
    }
	
	void Update () {
        CheckChanel();

        if (isChanneled)
        {
            B4Status.setEmpowerStatus(true);
            MiMiStatus.setEmpowerStatus(true);

            B4Status.setCanEmpowerStatus(false);
            MiMiStatus.setCanEmpowerStatus(false);
        }
	}

    void CheckChanel()
    {
        canEmpower = B4Status.getCanEmpowerStatus();
        

        if (canEmpower && !isChanneled)
        {

            if (Input.GetButtonDown("Channelling") && canEmpower && B4Status.getBondStatus())
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
            canEmpower = false;
            channelling = false;
            isChanneled = true;
        }
        else
        {
            Debug.Log("EMPOWERING: timeDifference: " + timeDifference + ", is less than 3.0f");
        }
    }
}
