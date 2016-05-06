using UnityEngine;
using System.Collections;

public class ScrollingTextureScript : MonoBehaviour {

    public float scrollSpeed = 0.90f; 
    public float scrollSpeed2 = 0.90f; 

	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float offset = Time.time * scrollSpeed;
        float offset2 = Time.time * scrollSpeed2;

        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(offset, offset2);
	}
}
