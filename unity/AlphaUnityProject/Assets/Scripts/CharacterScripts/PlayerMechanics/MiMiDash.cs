using UnityEngine;
using System.Collections;

public class MiMiDash : MonoBehaviour {

    public float dashForce = 10.0f;
    public GameObject MiMi;
    public float inputDelay = 0.2f;
    public float dashTime = 1.0f;

    private bool channelling, dashing = false;
    private float startTime, dashStartTime;
    private float m_rigidbodyDrag;
    private float m_speed;
    private float m_orgSpeed;
    private float m_originalDrag;

    void Start()
    {
        m_orgSpeed = GetComponent<PlayerController>().speed;
        m_originalDrag = GetComponent<Rigidbody>().drag; 
    }
    void Update ()
    {
        if (Input.GetButtonDown("MiMiDash"))
        {
            channelling = true;
            startTime = Time.time;
        }

        if (Input.GetButtonUp("MiMiDash"))
        {
            startTime = 0.0f;
            channelling = false;
            GetComponent<PlayerController>().speed = m_orgSpeed;
            GetComponent<Rigidbody>().drag = m_originalDrag;
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
            MiMi.GetComponent<Rigidbody>().AddForce(forceVector * dashForce, ForceMode.Force);
            dashStartTime = Time.time;
            dashing = true;
        }
    }
}
