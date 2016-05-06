using UnityEngine;
using System.Collections;

public class MiMiDash : MonoBehaviour {

    public float dashForce = 10.0f;
    public GameObject MiMi;
    public float inputDelay = 0.2f;
    public float dashTime = 1.0f;

    private bool channelling, dashing = false;
    private float startTime, dashStartTime;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            channelling = true;
            startTime = Time.time;
        }

        if (Input.GetKeyUp(KeyCode.M))
        {
            startTime = 0.0f;
            channelling = false;
            GetComponent<PlayerController>().speed = 38.0f;
            GetComponent<Rigidbody>().drag = 6.0f;
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
                GetComponent<Rigidbody>().drag = 6.0f;
                GetComponent<PlayerController>().speed = 38.0f;
                dashing = false;
            }
            else
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
            GetComponent<Rigidbody>().drag = 1.0f;
            GetComponent<PlayerController>().speed = 0.0f;

            Vector3 forceDirection = transform.forward;
            Vector3 forceVector = forceDirection.normalized * 100;
            MiMi.GetComponent<Rigidbody>().AddForce(forceVector * dashForce, ForceMode.Force);
            dashStartTime = Time.time;
            dashing = true;
        }
    }
}
