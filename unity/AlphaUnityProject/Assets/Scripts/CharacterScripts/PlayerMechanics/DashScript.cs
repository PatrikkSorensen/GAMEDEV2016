using UnityEngine;
using System.Collections;

public class DashScript : MonoBehaviour {

    public float dashForce = 10.0f;
    public GameObject B4;
    public float inputDelay = 0.2f;
    public float dashTime = 1.0f;
    public AudioClip dashSound;

    private bool channelling, dashing = false;
    private float startTime, dashStartTime;
    private float m_rigidbodyDrag;
    private float m_speed;
<<<<<<< HEAD
=======
    private AudioSource sfxSource;

    void Start()
    {
        sfxSource.playOnAwake = false;
        sfxSource.loop = false;
        sfxSource.clip = dashSound;
    }
>>>>>>> 8b01916dfb8c05b4d5fa343b3f59b0eac601893e

    private AudioSource sfxSource;
    private float m_orgSpeed;
    private float m_originalDrag;

    void Start()
    {
        m_orgSpeed = GetComponent<PlayerController>().speed;
        m_originalDrag = GetComponent<Rigidbody>().drag;
    }

	void Update () 
    {
        if (Input.GetButtonDown("B4Dash"))
        {
            channelling = true;
            startTime = Time.time;
        }

        if (Input.GetButtonUp("B4Dash"))
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
            sfxSource.Play();
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
