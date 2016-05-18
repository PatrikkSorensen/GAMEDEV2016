using UnityEngine;
using System.Collections;

public class ChangeRotationPoint : MonoBehaviour {

    public RotatePoint rotatePoint;
    public GameObject otherSwitch;
    public bool isActive; 

    void OnTriggerStay(Collider other) 
    {
        if (other.gameObject.tag == "B4" && !isActive || other.gameObject.tag == "MiMi" && !isActive)
        {
            rotatePoint.ChangeEndPoint(otherSwitch, rotatePoint.changeDuration);
            otherSwitch.GetComponent<ChangeRotationPoint>().isActive = true;  

            // TODO: Play sound
        }

    }
}
