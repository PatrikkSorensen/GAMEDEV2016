using UnityEngine;
using System.Collections;

public class B4SlingShot : MonoBehaviour {

    public GameObject lookAtObject;

    private bool canSlingShot = false;
    private GameObject B4; 
    private PlayerStatusScript B4Status;

    void Start()
    {
        B4 = GameObject.FindGameObjectWithTag("B4"); 
        B4Status = B4.GetComponent<PlayerStatusScript>(); 
    }

    void Update()
    {
        
    }
    void OnTriggerStay(Collider other)
    {
        B4Status.setCanSlingshot(true);
        if (Input.GetKey(KeyCode.B))
        {
            LockB4();
        }
        else
        {
            UnlockB4();
        }
    }

    void OnTriggerExit(Collider other)
    {
        B4Status.setCanSlingshot(false);
        UnlockB4(); 
    }

    void LockB4()
    {
        B4.GetComponent<PlayerController>().enabled = false;
        B4.transform.LookAt(lookAtObject.transform);
    }

    void UnlockB4()
    {
        B4.GetComponent<PlayerController>().enabled = true;
    }

    void BreakBond()
    {

    }
}
