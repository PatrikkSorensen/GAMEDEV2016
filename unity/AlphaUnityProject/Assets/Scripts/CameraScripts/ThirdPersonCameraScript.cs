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
    private Ray rayFwd;
    private Transform cam;
    private bool DidFwdRayHit = false;
    private int randomDir = 0; // 0 for left 1 for right


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

        RaycastHit hit;
        cam = Camera.main.transform;

        rayFwd = new Ray(cam.position, cam.forward);

        Vector3 rayRightV1 = cam.forward;
        rayRightV1 = Quaternion.AngleAxis(20, Vector3.up) * rayRightV1;

        Vector3 rayLeftV1 = cam.forward;
        rayLeftV1 = Quaternion.AngleAxis(-20, Vector3.up) * rayLeftV1;

        Vector3 rayRightV2 = cam.forward;
        rayRightV2 = Quaternion.AngleAxis(45, Vector3.up) * rayRightV2;

        Vector3 rayLeftV2 = cam.forward;
        rayLeftV2 = Quaternion.AngleAxis(-45, Vector3.up) * rayLeftV2;

        Vector3 rayRightV3 = cam.forward;
        rayRightV3 = Quaternion.AngleAxis(65, Vector3.up) * rayRightV3;

        Vector3 rayLeftV3 = cam.forward;
        rayLeftV3 = Quaternion.AngleAxis(-65, Vector3.up) * rayLeftV3;

        Vector3 rayRightV4 = cam.forward;
        rayRightV4 = Quaternion.AngleAxis(90, Vector3.up) * rayRightV4;

        Vector3 rayLeftV4 = cam.forward;
        rayLeftV4 = Quaternion.AngleAxis(-90, Vector3.up) * rayLeftV4;

        if (DidFwdRayHit == false)
        {
            randomDir = Mathf.RoundToInt(Random.value);
        }

        if (Physics.Raycast(rayFwd, out hit, 4.0f))
        {
            DidFwdRayHit = true;
            Debug.DrawLine(rayFwd.origin, hit.point);
            if (randomDir == 1) { cameraPan = cameraPan + 0.005f; }
            if (randomDir == 0) { cameraPan = cameraPan - 0.005f; }
        }
        else { DidFwdRayHit = false; }

        if (Physics.Raycast(cam.position, rayLeftV1, out hit, 4.0f))
        {
            Debug.DrawLine(cam.position, hit.point);
            cameraPan = cameraPan - 0.005f;
        }

        if (Physics.Raycast(cam.position, rayRightV1, out hit, 4.0f))
        {
            Debug.DrawLine(cam.position, hit.point);
            cameraPan = cameraPan + 0.005f;
        }
        
        if(Physics.Raycast(cam.position, rayLeftV2, out hit, 3.50f))
        {
            Debug.DrawLine(cam.position, hit.point);
            cameraPan = cameraPan - 0.005f;
        }

        if (Physics.Raycast(cam.position, rayRightV2, out hit, 3.5f))
        {
            Debug.DrawLine(cam.position, hit.point);
            cameraPan = cameraPan + 0.005f;
        }

        if (Physics.Raycast(cam.position, rayRightV3, out hit, 3.0f))
        {
            Debug.DrawLine(cam.position, hit.point);
            cameraPan = cameraPan + 0.005f;
        }

        if (Physics.Raycast(cam.position, rayLeftV3, out hit, 3.0f))
        {
            Debug.DrawLine(cam.position, hit.point);
            cameraPan = cameraPan - 0.005f;
        }

        if (Physics.Raycast(cam.position, rayRightV4, out hit, 3.0f))
        {
            Debug.DrawLine(cam.position, hit.point);
            cameraPan = cameraPan + 0.005f;
        }

        if (Physics.Raycast(cam.position, rayLeftV4, out hit, 3.0f))
        {
            Debug.DrawLine(cam.position, hit.point);
            cameraPan = cameraPan - 0.005f;
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
