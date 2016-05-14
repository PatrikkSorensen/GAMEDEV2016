using UnityEngine;
using System.Collections;

public class Float : MonoBehaviour {
	public float amplitude;          //Set in Inspector 
	public float speed;                  //Set in Inspector 
	private Vector3 moveTo;
    private Vector3 position;

	void Start () 
	{
        position = transform.position;
    }

	void Update () 
	{
        moveTo = transform.position;
		moveTo.y = position.y + amplitude * Mathf.Sin(speed * Time.time);
		transform.position = moveTo;
	}

}

