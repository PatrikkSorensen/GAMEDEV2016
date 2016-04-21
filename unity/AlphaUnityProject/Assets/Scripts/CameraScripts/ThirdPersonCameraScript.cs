using UnityEngine;
using System.Collections;

public class ThirdPersonCameraScript : MonoBehaviour {

    [SerializeField]
    public float distanceAway = 8.0f;
    [SerializeField]
    public float distanceUp = 6.0f;
    [SerializeField]
    private float smooth = 3.0f;
    private float cameraPan = 0.0f;

    [SerializeField]
    private float maxPlayerDistance; 
    //TODO: Do camera bounds 

    private Transform playerOne;
    private Transform playerTwo;
    private Vector3 middlePosition; 
    private Vector3 targetPosition; 

    void Start()
    {
        playerOne = GameObject.FindGameObjectWithTag("B4").transform;
        playerTwo = GameObject.FindGameObjectWithTag("MiMi").transform;
        Vector3 offset = new Vector3(2, 0, 0);
    }

    void FixedUpdate()
    {
        middlePosition = findMiddlePosition(playerOne.position, playerTwo.position);
        //targetPosition = middlePosition + Vector3.up * distanceUp - playerOne.forward - playerTwo.forward * distanceAway;

        if (Input.GetButton("CamRight"))
        {
            cameraPan = cameraPan + Time.deltaTime;

        }
        if (Input.GetButton("CamLeft"))
        {
            cameraPan = cameraPan - Time.deltaTime;

        }



        Vector3 offset = new Vector3(Mathf.Sin(cameraPan) * 2, 0, Mathf.Sin(cameraPan + (Mathf.PI/2)) * 2);
        targetPosition = middlePosition + Vector3.up * distanceUp - offset * distanceAway;

        //Debug.DrawRay(playerOne.position, Vector3.up * distanceUp, Color.red);
        //Debug.DrawRay(playerOne.position, -1f * playerOne.forward * distanceAway, Color.red );
        //Debug.DrawRay(playerTwo.position, -1f * playerTwo.forward * distanceAway, Color.red);
        //Debug.DrawLine(playerOne.position, targetPosition, Color.green);
        //Debug.DrawLine(playerTwo.position, targetPosition, Color.green);
        //Debug.DrawLine(transform.position, middlePosition, Color.blue);

        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smooth);
        transform.LookAt(middlePosition);
    }

    Vector3 findMiddlePosition(Vector3 vec1, Vector3 vec2)
    {
        Vector3 middlePosition = (vec1 + vec2) / 2.0f;
        return middlePosition;
    }

}
