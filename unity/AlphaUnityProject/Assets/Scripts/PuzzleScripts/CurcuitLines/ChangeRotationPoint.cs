using UnityEngine;
using System.Collections;

public class ChangeRotationPoint : MonoBehaviour {

    public RotatePoint rotatePoint;
    public GameObject otherSwitch; 

    void OnTriggerStay(Collider other) 
    {
        if (other.gameObject.tag == "B4")
            Debug.Log("I should switch rp."); 

        rotatePoint.ChangeEndPoint(otherSwitch, rotatePoint.changeDuration);
        //rotatePoint.ChangeEndPoint()
    }
}
