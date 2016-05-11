using UnityEngine;
using System.Collections;

public class BondCubeScript : MonoBehaviour {

    Vector3 Offset;
    Vector3 middlePosition;  
    GameObject B4, MiMi; 
	// Use this for initialization
	void Start () {
        Offset = Vector3.up + new Vector3(0.0f, 0.5f, 0.0f);
        B4 = GameObject.FindGameObjectWithTag("B4");
        MiMi = GameObject.FindGameObjectWithTag("MiMi");

    }
	
	// Update is called once per frame
	void Update () {
        //middlePosition = (B4.transform.position + MiMi.transform.position) / 2;
        //transform.position = middlePosition + Offset;
        if (Input.GetKey(KeyCode.H))
        {
            Debug.Log("Adding force");
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 10.0f);
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colliding");
       
    }
}
