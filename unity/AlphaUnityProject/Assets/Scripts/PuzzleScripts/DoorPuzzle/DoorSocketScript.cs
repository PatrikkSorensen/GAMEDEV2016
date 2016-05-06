using UnityEngine;
using System.Collections;
using DG.Tweening;

public class DoorSocketScript : MonoBehaviour {

    public GameObject Door;
    public GameObject Socket;
    public GameObject AttachTrigger;
    public AudioClip Click;
    private AudioSource source;

	// Use this for initialization
	void Start () 
    {
        source = gameObject.AddComponent<AudioSource>();
        source.clip = Click;
        source.loop = false;
        source.playOnAwake = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DoorSocket")
        {
            openDoor();
            Debug.Log("Open Door");
            FixedJoint joint = gameObject.AddComponent<FixedJoint>();
            joint.connectedBody = Socket.GetComponent<Rigidbody>();
            AttachTrigger.GetComponent<PlugPullScript>().unattachObject();
            AttachTrigger.GetComponent<PlugPullScript>().setFinished();
            source.Play();
        }
    }

    void openDoor()
    {
        Door.transform.DOMove(Door.transform.position + new Vector3(0, -5.5f, 0), 2);

    }
}
