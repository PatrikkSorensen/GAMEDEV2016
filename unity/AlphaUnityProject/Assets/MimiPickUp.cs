using UnityEngine;
using System.Collections;

public class MimiPickUp : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	void OnTriggerEnter(Collider other) 
	{
		if(other.gameObject.tag == "BluePickUp")
		{
			other.gameObject.SetActive(false);
		}
	}
}
