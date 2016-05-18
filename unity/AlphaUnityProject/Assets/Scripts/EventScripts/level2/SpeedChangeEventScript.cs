using UnityEngine;
using System.Collections;

public class SpeedChangeEventScript : MonoBehaviour {

    public float newSpeed, newFallSpeed, newDashForce, newDrag, newAngularDrag;
    public bool affectsB4, affectsMiMi;
    private GameObject B4, MiMi;
    private bool b4 = false, mimi = false, done = false;

	// Use this for initialization
	void Start () {
        B4 = GameObject.FindGameObjectWithTag("B4");
        MiMi = GameObject.FindGameObjectWithTag("MiMi");
        if (!affectsB4)
        {
            b4 = true;
        }
        if (!affectsMiMi)
        {
            mimi = true;
        }
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

        if (other.gameObject == B4 && affectsB4)
        {
            B4.GetComponent<PlayerController>().setSpeed(newSpeed);
            B4.GetComponent<PlayerController>().setUnBoostedSpeed(newSpeed);
            B4.GetComponent<PlayerController>().setProperties(newFallSpeed, newDashForce, newDrag, newAngularDrag);
            b4 = true;
        }

        if (other.gameObject == MiMi && affectsMiMi)
        {
            MiMi.GetComponent<PlayerController>().setSpeed(newSpeed);
            MiMi.GetComponent<PlayerController>().setUnBoostedSpeed(newSpeed);
            MiMi.GetComponent<PlayerController>().setProperties(newFallSpeed, newDashForce, newDrag, newAngularDrag);
            mimi = true;
        }

    }
}
