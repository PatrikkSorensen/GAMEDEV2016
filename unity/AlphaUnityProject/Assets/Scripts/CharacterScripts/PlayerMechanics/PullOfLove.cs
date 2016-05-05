using UnityEngine;
using System.Collections;

public class PullOfLove : MonoBehaviour {

    public float movementPullForce, middlePullForce, empowerPullForce = 30.0f;
    public AudioClip pullClip;
    public ForceMode forceMode;

    private GameObject B4, MiMi;
    private PlayerStatusScript B4Status, MiMiStatus; 
    private float startTime = 0.0f;
    private float timeDelay = 0.5f;
     
	void Start () {
        B4 = GameObject.FindGameObjectWithTag("B4");
        MiMi = GameObject.FindGameObjectWithTag("MiMi");

        B4Status = B4.GetComponent<PlayerStatusScript>();
        MiMiStatus = MiMi.GetComponent<PlayerStatusScript>();
    }

	void Update () {
        if (B4Status.getBondStatus())
        {
            if (Input.GetButtonDown("MiMiPull"))
            {
                float timeDifference = Time.time - startTime;
                if (timeDifference > timeDelay)
                {
                    if (B4Status.getEmpowerStatus())
                        B4EmpowerPull();
                    //else if (B4Status.getEmpowerStatus()) // If MiMi is attached do middlepull
                        //B4MiddlePull()
                    else
                        B4MovementPull();

                    startTime = Time.time;
                }
            }

            if (Input.GetButtonDown("B4Pull"))
            {
                float timeDifference = Time.time - startTime;
                if (timeDifference > timeDelay)
                {
                    if (B4Status.getEmpowerStatus())
                        B4EmpowerPull();
                    //else if (B4Status.getEmpowerStatus()) // If MiMi is attached do middlepull
                    //B4MiddlePull()
                    else
                        B4MovementPull();
                }
            }
        }
    }

    // MovementPull
    void B4MovementPull()
    {
        Vector3 forceDirection = MiMi.transform.position - B4.transform.position;
        Vector3 forceVector = forceDirection.normalized * 100;
        B4.GetComponent<Rigidbody>().AddForce(forceVector * movementPullForce, forceMode);
    }

    void MiMiMovementPull()
    {
        Vector3 forceDirection = B4.transform.position - MiMi.transform.position;
        Vector3 forceVector = forceDirection.normalized * 100;
        MiMi.GetComponent<Rigidbody>().AddForce(forceVector * movementPullForce, forceMode);
    }

    // MiddlePull
    void B4MiddlePull()
    {
        Vector3 middlePosition = (MiMi.transform.position + transform.position) / 2.0f;
        Vector3 forceVector = middlePosition - MiMi.transform.position;
        MiMi.GetComponent<Rigidbody>().AddForce(forceVector.normalized * middlePullForce * 10, forceMode);
    }

    void MiMiMiddlePull()
    {
        Vector3 middlePosition = (MiMi.transform.position + transform.position) / 2.0f;
        Vector3 forceVector = middlePosition - MiMi.transform.position;
        MiMi.GetComponent<Rigidbody>().AddForce(forceVector.normalized * middlePullForce * 10, forceMode);
    }

    // EmpowerPull
    void B4EmpowerPull()
    {
        Vector3 forceDirection = B4.transform.position - MiMi.transform.position;
        Vector3 forceVector = forceDirection.normalized * 100;
        B4.GetComponent<Rigidbody>().AddForce(forceVector * empowerPullForce, forceMode);
        Debug.Log("MiMI Chanel pull");

    }

    void MiMiEmpowerPull()
    {
        Vector3 forceDirection = B4.transform.position - MiMi.transform.position;
        Vector3 forceVector = forceDirection.normalized * 100;
        MiMi.GetComponent<Rigidbody>().AddForce(forceVector * empowerPullForce, forceMode);
        Debug.Log("MiMI Chanel pull");
    }
}

//if (B4Status.getEmpowerStatus() && !chanelPull)
//{
//    PullMiMiToB4();
//} else if (B4Status.getEmpowerStatus() && chanelPull) {
//    MiMiChanelPull(); 
//} else {
//    MiddlePull();
//}