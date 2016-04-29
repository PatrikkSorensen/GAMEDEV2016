using UnityEngine;
using System.Collections;

public class PullOfLove : MonoBehaviour {

    public float pullForce, middlePullForce, chanelPullForce = 50.0f;
    public AudioClip pullClip;
    public ForceMode forceMode;
    public bool chanelPull = false; 

    private GameObject B4, MiMi;
    private PlayerStatusScript B4Status, MiMiStatus; 
    private float startTime = 0.0f;
    private float timeDelay = 0.5f;
     
	void Start () {
        B4 = GameObject.FindGameObjectWithTag("B4");
        B4Status = B4.GetComponent<PlayerStatusScript>(); 

        MiMi = GameObject.FindGameObjectWithTag("MiMi");
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
                    if (B4Status.getChannelStatus() && !chanelPull)
                    {
                        PullMiMiToB4();
                    } else if (B4Status.getChannelStatus() && chanelPull) {
                        MiMiChanelPull(); 
                    } else {
                        MiddlePull();
                    }

                    startTime = Time.time;
                }
            }

            if (Input.GetButtonDown("B4Pull"))
            {
                float timeDifference = Time.time - startTime;
                if (timeDifference > timeDelay)
                {
                    PullB4ToMiMi();
                    startTime = Time.time;
                }
            }


        }
    }

    void PullB4ToMiMi()
    {
        Debug.Log("Pulling b4 to Mimi");
        Vector3 forceDirection = MiMi.transform.position - B4.transform.position;
        Vector3 forceVector = forceDirection.normalized * 100;
        B4.GetComponent<Rigidbody>().AddForce(forceVector * pullForce, forceMode);
    }

    void PullMiMiToB4()
    {
        Vector3 forceDirection = B4.transform.position - MiMi.transform.position;
        Vector3 forceVector = forceDirection.normalized * 100;
        MiMi.GetComponent<Rigidbody>().AddForce(forceVector * pullForce, forceMode);
    }

    void MiddlePull()
    {
        Debug.Log("Middle pull");
        Vector3 middlePosition = (MiMi.transform.position + transform.position) / 2.0f;
        Vector3 forceVector = middlePosition - MiMi.transform.position;
        MiMi.GetComponent<Rigidbody>().AddForce(forceVector.normalized * middlePullForce * 10, forceMode);
    }

    void B4ChanelPull()
    {
        Vector3 forceDirection = B4.transform.position - MiMi.transform.position;
        Vector3 forceVector = forceDirection.normalized * 100;
        B4.GetComponent<Rigidbody>().AddForce(forceVector * chanelPullForce, forceMode);
        Debug.Log("MiMI Chanel pull");

    }

    void MiMiChanelPull()
    {
        Vector3 forceDirection = B4.transform.position - MiMi.transform.position;
        Vector3 forceVector = forceDirection.normalized * 100;
        MiMi.GetComponent<Rigidbody>().AddForce(forceVector * chanelPullForce, forceMode);
        Debug.Log("MiMI Chanel pull");

    }
}
