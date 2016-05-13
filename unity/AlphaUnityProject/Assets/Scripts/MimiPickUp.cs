using UnityEngine;
using System.Collections;

public class MimiPickUp : MonoBehaviour {

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
