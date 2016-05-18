using UnityEngine;
using System.Collections;

public class SpeedChangeEventScript : MonoBehaviour {

    public float newSpeed, newFallSpeed, newDashForce, newDrag, newAngularDrag;
    private GameObject B4, MiMi;
    private bool b4 = false, mimi = false, done = false;

	// Use this for initialization
	void Start () {
        B4 = GameObject.FindGameObjectWithTag("B4");
        MiMi = GameObject.FindGameObjectWithTag("MiMi");
	}
	
	// Update is called once per frame
	void Update () {
	    if(b4 && mimi)
        {
            done = true;
        }

        if (done)
        {
            gameObject.active = false;
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == B4)
        {
            B4.GetComponent<PlayerController>().setSpeed(newSpeed);
            B4.GetComponent<PlayerController>().setUnBoostedSpeed(newSpeed);
            b4 = true;
        }

        if (other.gameObject == MiMi)
        {
            MiMi.GetComponent<PlayerController>().setSpeed(newSpeed);
            MiMi.GetComponent<PlayerController>().setUnBoostedSpeed(newSpeed);
            mimi = true;
        }

        other.gameObject.GetComponent<PlayerController>().setProperties(newFallSpeed, newDashForce, newDrag, newAngularDrag);

    }
}
