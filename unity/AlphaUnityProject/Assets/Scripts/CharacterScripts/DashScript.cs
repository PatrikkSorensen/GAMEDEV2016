using UnityEngine;
using System.Collections;

public class DashScript : MonoBehaviour {

    public float dashForce = 10.0f;
    public GameObject B4;
    public float inputDelay = 0.2f;
    public float dashTime = 1.0f; 

    private bool channelling, dashing = false;
    private float startTime, dashStartTime;
    private float m_rigidbodyDrag;
    private float m_speed;  

	void Update () {
        if (Input.GetButtonDown("B4Dash"))
        {
            Debug.Log("B4Dash pressed");
            channelling = true;
            startTime = Time.time;
        }

        if (Input.GetButtonUp("B4Dash"))
        {
            startTime = 0.0f;
            channelling = false;
            GetComponent<PlayerController>().speed = m_speed;
            GetComponent<Rigidbody>().drag = m_rigidbodyDrag;
        }

        if (channelling)
        {
            Dash();
        }

        if (dashing)
        {
            float timeDifference = Time.time - dashStartTime;
            if (timeDifference > dashTime && timeDifference < dashTime + 0.2f)
            {
                dashStartTime = 0.0f; 
                Debug.Log("Stop dashing!");
                GetComponent<Rigidbody>().drag = m_rigidbodyDrag;
                GetComponent<PlayerController>().speed = m_speed;
                dashing = false;
            } else
            {
                Debug.Log(timeDifference + " > " + dashTime);
            }
        }
    }

    void Dash()
    {
        float timeDifference = Time.time - startTime;
        if (timeDifference > inputDelay && timeDifference < inputDelay + 0.2f)
        {
            Debug.Log("Dashing...");
            startTime = 0.0f;

            //TODO: Make generic
            m_rigidbodyDrag = GetComponent<Rigidbody>().drag; 
            GetComponent<Rigidbody>().drag = 1.0f;

            m_speed = GetComponent<PlayerController>().speed;
            GetComponent<PlayerController>().speed = 0.0f; 

            Vector3 forceDirection = transform.forward; 
            Vector3 forceVector = forceDirection.normalized * 100;
            B4.GetComponent<Rigidbody>().AddForce(forceVector * dashForce, ForceMode.Force);
            dashStartTime = Time.time; 
            dashing = true; 
        }
    }


}
