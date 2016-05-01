using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChannelSwitch : MonoBehaviour {

    public float chanelTime = 2.0f;
    public GameObject lightStations; 

    private CurcuitLines[] m_powerlines;
    private CurcuitLines m_switchLines;
    private bool isActive, hasB4, hasMiMi, hasPower = false;
    private bool channelling; 
    private float startTime;

    public bool HasPower
    {
        get { return hasPower; }
        set { hasPower = value; }
    }

    public bool IsActive
    {
        get {return isActive; }
        set {isActive = value;}
    }

    void Start () {
        m_powerlines = lightStations.GetComponentsInChildren<CurcuitLines>(); 
        m_switchLines = GetComponent<CurcuitLines>();     
	}

    void Update()
    {
        if (!CheckPower())
            return;

        Debug.Log("I have power!");
        if (hasB4 && hasMiMi && !IsActive)
        {
            if (Input.GetButtonDown("Channelling"))
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
                ChanelEnergyOnSwitch();
            }
        }
    }

    private bool CheckPower()
    {
        bool shouldHavePower = true;
        foreach (CurcuitLines lines in m_powerlines)
        {
            if (!lines.HasChannelled)
                shouldHavePower = false;
        }

        return shouldHavePower; 

    }

    void ChanelEnergyOnSwitch()
    {
        float timeDifference = Time.time - startTime;

        if (timeDifference > chanelTime && timeDifference < chanelTime + 0.2f)
        {
            Debug.Log("Channelled for three seconds");
            IsActive = true;
        }
        else
        {
            Debug.Log("timeDifference: " + timeDifference + ", is less than 3.0f");
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "B4")
            hasB4 = true;


        if (other.tag == "MiMi")
            hasMiMi = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "B4")
            hasB4 = false;


        if (other.tag == "MiMi")
            hasMiMi = false;
    }

    private void CurcuitLinesStatus()
    {
        foreach (CurcuitLines lines in m_powerlines)
        {
            if (!lines.HasChannelled)
                return;
        }

        IsActive = true; 
    }
}
