using UnityEngine;
using System.Collections;

public class CheckPlayerIsInside : MonoBehaviour {

    public bool hasPlayers, hasMiMi, hasB4; 

	// Use this for initialization
	void Start () {
        hasPlayers = false; 
	}
	
	// Update is called once per frame
	void Update () {
        if (hasMiMi && hasB4)
            hasPlayers = true; 
	}

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "MiMi")
            hasMiMi = true;

        if (other.tag == "B4")
            hasB4 = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "MiMi")
            hasMiMi = false;

        if (other.tag == "B4")
            hasB4 = false;
    }
}
