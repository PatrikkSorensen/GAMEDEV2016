using UnityEngine;
using System.Collections;

public class AttachLine : MonoBehaviour {

    public GameObject lineOrigin;

    private RotatePoint originPoint;
    private bool attached = false;
    private bool canLineBeMoved = true; 
    private GameObject attachedObject; 

    void Start()
    {
        originPoint = lineOrigin.GetComponent<RotatePoint>();
    }

    void Update()
    {
        //if (attached)
            //UpdateLine(); 

    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "B4" && !originPoint.IsDrawing)
        {
            if (!attached)
            {
                attached = true;
                originPoint.AttachLineToPlayer(gameObject);
            }


            //if (!attached && canLineBeMoved)
            //{
            //    Debug.Log("Attaching line to player...");
            //    attached = true;
            //    canLineBeMoved = false; 
            //    attachedObject = other.gameObject;
            //} else if(attached && canLineBeMoved)
            //{
            //    canLineBeMoved = false; 
            //    attached = false;
            //    attachedObject = gameObject;
            //    lineOrigin.GetComponent<LineRenderer>().SetPosition(1, attachedObject.transform.position);
            //}
        }
    }

    void OnTriggerExit(Collider other)
    {
        canLineBeMoved = true; 
    }

    void UpdateLine()
    {
        Debug.Log("Updating line...");
        //originPoint.AttachLineToPlayer(gameObject); 
        //lineOrigin.GetComponent<LineRenderer>().SetPosition(1, attachedObject.transform.position); 
    }
}
