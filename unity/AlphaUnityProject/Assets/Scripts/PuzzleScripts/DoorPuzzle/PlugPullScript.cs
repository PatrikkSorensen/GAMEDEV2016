using UnityEngine;
using System.Collections;

public class PlugPullScript : MonoBehaviour {

    private bool canAttach, attached;
    private GameObject MiMi;
    private FixedJoint joint, joint2;

	// Use this for initialization
	void Start () {

        MiMi = GameObject.FindWithTag("MiMi");
        canAttach = false;
        attached = false;
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (!attached) 
        { 

            if (Input.GetButtonDown("MiMiAttach") && canAttach)
            {
                //move character to point
                Debug.Log("Attach!!!!");
                attachObject();
            }
        }
        else
        {
            if (Input.GetButtonDown("MiMiAttach") && attached)
            {
                //move character to point
                Debug.Log("Unattach!!!!");
                unattachObject();
            }
        }        
	}

    void attachObject()
    {
        joint = gameObject.AddComponent<FixedJoint>();
        joint.connectedBody = MiMi.GetComponent<Rigidbody>();
        joint.enablePreprocessing = false;

        joint2 = MiMi.AddComponent<FixedJoint>();
        joint2.connectedBody = gameObject.GetComponent<Rigidbody>();
        joint2.enablePreprocessing = false;
        attached = true;
    }

    public void unattachObject()
    {
        Destroy(joint);
        Destroy(joint2);
        attached = false;
    }



    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "MiMi")
        {
            canAttach = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "MiMi")
        {
            canAttach = false;
        }
    }
}
