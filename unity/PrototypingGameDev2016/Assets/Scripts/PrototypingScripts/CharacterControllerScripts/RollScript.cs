﻿using UnityEngine;
using System.Collections;

public class RollScript : MonoBehaviour {

	public float speed; 
	private Rigidbody rb;
    Quaternion targetRotation;



    public Quaternion TargetRotation
    {
        get { return targetRotation; }
    }

    void Start() {
        targetRotation = transform.rotation;
        rb = GetComponent<Rigidbody>(); 
	}

	void FixedUpdate(){
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		rb.AddForce(movement * speed);
	}
}
