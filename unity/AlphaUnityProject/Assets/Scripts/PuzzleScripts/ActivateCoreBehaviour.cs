using UnityEngine;
using System.Collections;

public class ActivateCoreBehaviour : MonoBehaviour {

    public GameObject pointEnergySource;
    
    void Update()
    {
        if (pointEnergySource.GetComponent<Point>().IsDrawn)
            Debug.Log("I am activated!"); 
    } 
}
