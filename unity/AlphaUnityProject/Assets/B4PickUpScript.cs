using UnityEngine;
using System.Collections;

public class B4PickUpScript : MonoBehaviour {

	void Start () {

	}

	void OnTriggerEnter(Collider other) 
	{
		if(other.gameObject.tag == "RedPickUp")
		{
			other.gameObject.SetActive(false);
		}
	}
}