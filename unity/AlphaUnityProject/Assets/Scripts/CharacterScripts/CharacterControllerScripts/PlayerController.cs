using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

    public float speed = 30;
    public string horizontal;
    public string vertical;
    public float movingTurnSpeed = 10;
    public float stationaryTurnSpeed = 180;
    public PlayerStatusScript playerStatus;

    private Rigidbody rb;

    public bool keyboardMiMi, velocityMode = false; 
    
    
    void Awake()
    {
        SetInputs();
        SetVariables(); 
    }

    void FixedUpdate()
    {
        playerStatus.setSpeed(speed);
        float h = Input.GetAxis(horizontal);
        float v = Input.GetAxis(vertical);
        Move(h, v);
    }

    void Move(float h, float v)
    {
        Vector3 movement = new Vector3(0.0f, 0.0f, 0.0f);
        //Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical );
        //movement = Camera.main.transform.TransformDirection(movement);

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
        } else
        {
            movement = new Vector3(h, 0.0f, v);
            movement = Camera.main.transform.TransformDirection(movement); //Makes you fly since the camera points down slightly
            movement = Quaternion.AngleAxis(-Camera.main.transform.rotation.eulerAngles.x, Camera.main.transform.right) * movement;
            //Which is then fixed by rotating the movement vector based on how much the camera is pointing down

            
        }
      
        
        if(velocityMode)
            rb.velocity = movement * speed; 
        else
            rb.AddForce(movement * speed);
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
        playerStatus = gameObject.AddComponent<PlayerStatusScript>(); 
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
}
