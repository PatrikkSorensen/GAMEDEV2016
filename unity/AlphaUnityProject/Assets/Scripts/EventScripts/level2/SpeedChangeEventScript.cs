using UnityEngine;
using System.Collections;

public class SpeedChangeEventScript : MonoBehaviour {

    public float newSpeed;
    private GameObject B4, MiMi;

	// Use this for initialization
	void Start () {
        B4 = GameObject.FindGameObjectWithTag("B4");
        MiMi = GameObject.FindGameObjectWithTag("MiMi");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == B4)
        {
            B4.GetComponent<PlayerController>().setSpeed(newSpeed);
            B4.GetComponent<PlayerController>().setUnBoostedSpeed(newSpeed);           
        }

        if (other.gameObject == MiMi)
        {
            MiMi.GetComponent<PlayerController>().setSpeed(newSpeed);
            MiMi.GetComponent<PlayerController>().setUnBoostedSpeed(newSpeed);      
        }

    }
}
