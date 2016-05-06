using UnityEngine;
using System.Collections;

public class PlugPullScript : MonoBehaviour {

    public GameObject WireAnchor;
    public Shader shader2;
    private bool canAttach, attached, finished;
    private GameObject MiMi;
    private FixedJoint joint, joint2;
    private LineRenderer lr;

	// Use this for initialization
	void Start () {

        MiMi = GameObject.FindWithTag("MiMi");
        lr = gameObject.AddComponent<LineRenderer>();

        lr.material = new Material(shader2);
        lr.material.color = new Color(0f, 1f, 0f, 0.05f);
        lr.SetWidth(0.5f, 0.5f);
        Vector3[] points = { gameObject.transform.position + new Vector3(0.0f, 0.0f, 0.0f), WireAnchor.transform.position + new Vector3(0.0f, 0.0f, 0.0f) };
        lr.SetPositions(points);

        canAttach = false;
        attached = false;
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (!attached) 
        { 

            if (Input.GetButtonDown("MiMiAttach") && canAttach && !finished)
            {
                //move character to point
                attachObject();
            }
        }
        else
        {
            if (Input.GetButtonDown("MiMiAttach") && attached && !finished)
            {
                //move character to point
                unattachObject();
            }
        }        
	}

    void Update()
    {
        Vector3[] points = { gameObject.transform.position + new Vector3(0.0f, 0.0f, 0.0f), WireAnchor.transform.position + new Vector3(0.0f, 0.03f, 0.0f) };
        lr.SetPositions(points);
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

    public void setFinished()
    {
        finished = true;
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
