using UnityEngine;
using System.Collections;

public class PullOfLove : MonoBehaviour {

    public float pullForce = 100.0f;
    public AudioClip pullClip;

    private GameObject B4, MiMi;
    private float startTime = 0.0f;
    private float timeDelay = 0.5f;
     
	void Start () {
        B4 = GameObject.FindGameObjectWithTag("B4");
        MiMi = GameObject.FindGameObjectWithTag("MiMi");
    }

	void Update () {
        if (B4.GetComponent<PlayerController>().isBonded())
        {
            if (Input.GetButtonDown("MiMiPull"))
            {
                float timeDifference = Time.time - startTime;
                if (timeDifference > timeDelay)
                {
                    PullMiMiToB4();
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
        Vector3 forceDirection = MiMi.transform.position - B4.transform.position;
        Vector3 forceVector = forceDirection.normalized * 100;
        B4.GetComponent<Rigidbody>().AddForce(forceVector * pullForce);
    }

    void PullMiMiToB4()
    {
        Vector3 forceDirection = B4.transform.position - MiMi.transform.position;
        Vector3 forceVector = forceDirection.normalized * 100;
        MiMi.GetComponent<Rigidbody>().AddForce(forceVector * pullForce);
    }
}
