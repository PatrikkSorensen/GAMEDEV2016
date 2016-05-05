using UnityEngine;
using System.Collections;

public class SparkHintScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        //Trigger circular pulse particles
    }

    void OnTriggerExit(Collider other)
    {
        //turn off particles
    }
}
