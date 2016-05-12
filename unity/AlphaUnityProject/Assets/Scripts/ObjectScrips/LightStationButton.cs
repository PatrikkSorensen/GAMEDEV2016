using UnityEngine;
using System.Collections;

public class LightStationButton : MonoBehaviour {

    public GameObject fireFlies; 

    private GameObject B4, MiMi; 
    private bool m_canChanel;

    public bool CanChanel
    {
        get { return m_canChanel; }
        set { m_canChanel = value;}
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "B4")
        {
            fireFlies.SetActive(true); 
            CanChanel = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "B4")
        {
            fireFlies.SetActive(false);
            CanChanel = false;
        }
    }
}
