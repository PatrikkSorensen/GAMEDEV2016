using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

    public float speed = 30;
    public float movingTurnSpeed = 10;
    public float stationaryTurnSpeed = 180;
    public float fallSpeed = 0.0f;
    public float distanceToGround = 2.5f; 
    public bool keyboardMiMi, velocityMode = false;

    public PlayerStatusScript playerStatus;

    private Rigidbody rb;
    private Animator anim; 
    private string horizontal;
    private string vertical;
    private bool isGrounded = true; 
    private GameObject B4, MiMi;
    private PlayerStatusScript B4Status, MiMiStatus;

    void Awake()
    {
        SetInputs();
        SetVariables(); 
    }

    void FixedUpdate()
    {
        CheckGrounded(); 
        playerStatus.setSpeed(speed);
        float h = Input.GetAxis(horizontal);
        float v = Input.GetAxis(vertical);
        Move(h, v);
    }
    
    void CheckGrounded()
    {
        RaycastHit hit;

        //Debug.Log(hit.point);
        //Debug.DrawRay(transform.position, -Vector3.up * 10.0f, Color.green);
        //Debug.Log(hit.transform.gameObject.tag);

        Vector3 RayCastFrom = transform.position;
        if (gameObject.tag == "MiMi")
            RayCastFrom += new Vector3(0.0f, 0.5f, 0.0f);

        if (gameObject.tag == "B4")
            RayCastFrom += Vector3.up;



        Debug.DrawLine(RayCastFrom, Vector3.zero);
        if (Physics.Raycast(RayCastFrom, -Vector3.up, out hit, 100.0f))
        {
            if (hit.distance > distanceToGround)
            {
                isGrounded = false;
                //Debug.Log("I should add force towards the ground!" + hit.distance);
            } else
            {
                //Debug.Log(hit.distance + ", " + transform.position);
                isGrounded = true; 
            }  
        }
            
    }

    void Move(float h, float v)
    {
        Vector3 movement = new Vector3(0.0f, 0.0f, 0.0f);

        // For Joysticks, mostly optimized ps4 controller
        if (gameObject.tag == "MiMi")
        {
            v *= -1;
            float deadzone = 0.25f;
            Vector2 stickInput = new Vector2(h, v);
            if (stickInput.magnitude > deadzone)
            {
                movement.x = h;
                movement.z = v;
            }

            movement = Camera.main.transform.TransformDirection(movement);
            movement = Quaternion.AngleAxis(-Camera.main.transform.rotation.eulerAngles.x, Camera.main.transform.right) * movement;
        } else
        {
            movement = new Vector3(h, 0.0f, v);
            movement = Camera.main.transform.TransformDirection(movement);

            // normalizes camera rotation vector so we can move backwards
            movement = Quaternion.AngleAxis(-Camera.main.transform.rotation.eulerAngles.x, Camera.main.transform.right) * movement; 
        }

        if (!isGrounded)
            movement.y += fallSpeed * -1; 

        if(velocityMode)
            rb.velocity = movement * speed; 
        else
            rb.AddForce(movement * speed);

        //TODO: Evaluate this on controller: rb.AddForce(movement.normalized * Time.deltaTime * speed);

        

        float m_speed = movement.normalized.magnitude;

        //anim.SetFloat("Speed", m_speed);

        // Rotation
        if (movement.magnitude > 1f) movement.Normalize();
            movement = transform.InverseTransformDirection(movement);

        float m_TurnAmount = Mathf.Atan2(movement.x, movement.z);
        float m_ForwardAmount = movement.z;
        float turnSpeed = Mathf.Lerp(stationaryTurnSpeed, movingTurnSpeed, m_ForwardAmount);

        transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
    }

    public void SetVariables()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>(); 
        playerStatus = gameObject.AddComponent<PlayerStatusScript>(); // useful for other scripts

        //B4 = GameObject.FindGameObjectWithTag("B4");
        //MiMi = GameObject.FindGameObjectWithTag("MiMi");
        //B4Status = B4.GetComponent<PlayerController>().playerStatus;
        //MiMiStatus = MiMi.GetComponent<PlayerController>().playerStatus;
    }

    public void SetInputs()
    {
        if (gameObject.tag == "B4")
        {
            horizontal = "Horizontal";
            vertical = "Vertical";
        }


        if (gameObject.tag == "MiMi")
        {
            if (keyboardMiMi)
            {
                vertical = "MiMiKeyboardV";
                horizontal = "MiMiKeyboardH";
            }
            else
            {
                horizontal = "RightPadHorizontal";
                vertical = "RightPadVertical";
            }
        }
    }

    //TODO: Set empower status in another way. 
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ChanelObject")
        {
            Debug.Log(gameObject.name + " triggered with:  " + other.gameObject.name);
            //B4.GetComponent<PlayerStatusScript>().setCanEmpowerStatus(false);
            //MiMi.GetComponent<PlayerStatusScript>().setCanEmpowerStatus(false);
        }
    }
   
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "ChanelObject")
        {
            //B4.GetComponent<PlayerStatusScript>().setCanEmpowerStatus(true);
            //MiMi.GetComponent<PlayerStatusScript>().setCanEmpowerStatus(true);
        }
    }
}
