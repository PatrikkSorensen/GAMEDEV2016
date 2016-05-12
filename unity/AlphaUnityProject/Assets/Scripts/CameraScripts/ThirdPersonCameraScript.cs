using UnityEngine;
using System.Collections;
using DG.Tweening;

public class ThirdPersonCameraScript : MonoBehaviour {

    public float distanceAway = 8.0f;
    public float distanceUp = 6.0f;
    public Vector3 offset = Vector3.zero;
    public float correctionAmplifier = 0.005f;

    private float smooth = 3.0f;
    private float cameraPan = 0.0f;
    private float maxPlayerDistance; 

    private Transform playerOne;
    private Transform playerTwo;
    private Vector3 middlePosition; 
    private Vector3 targetPosition;
    private Transform cam;
    private bool DidFwdRayHit = false;
    private int randomDir = 0; // 0 for left 1 for right
    private float distanceUpFix;
    private float distanceAwayFix;


    void Start()
    {
        playerOne = GameObject.FindGameObjectWithTag("B4").transform;
        playerTwo = GameObject.FindGameObjectWithTag("MiMi").transform;
        distanceUpFix = distanceUp;
        distanceAwayFix = distanceAway;
    }

    void FixedUpdate()
    {
        middlePosition = findMiddlePosition(playerOne.position, playerTwo.position);
        //targetPosition = middlePosition + Vector3.up * distanceUp - playerOne.forward - playerTwo.forward * distanceAway;

        if (Input.GetButton("CamRight"))
        {
            cameraPan = cameraPan + Time.deltaTime * 1.20f;

        }
        if (Input.GetButton("CamLeft"))
        {
            cameraPan = cameraPan - Time.deltaTime * 1.20f;

        }

        RaycastHit hit;
        cam = Camera.main.transform;

        Ray rayFwd = new Ray(cam.position, cam.forward);

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

        Vector3 rayDown = cam.forward;
        rayDown = Quaternion.AngleAxis(-90, Vector3.left) * rayDown;

        Vector3 rayBehind1 = cam.forward;
        rayBehind1 = Quaternion.AngleAxis(-245, Vector3.up) * rayBehind1;

        Vector3 rayBehind2 = cam.forward;
        rayBehind2 = Quaternion.AngleAxis(-180, Vector3.up) * rayBehind2;

        Vector3 rayBehind3 = cam.forward;
        rayBehind3 = Quaternion.AngleAxis(-115, Vector3.up) * rayBehind3;

        if (DidFwdRayHit == false)
        {
            randomDir = Mathf.RoundToInt(Random.value);
        }

        if (Physics.Raycast(rayFwd, out hit, 4.0f))
        {
            DidFwdRayHit = true;
            Debug.DrawLine(rayFwd.origin, hit.point);
            distanceAwayFix = distanceAwayFix + correctionAmplifier * 5; 
            if (randomDir == 1) { cameraPan = cameraPan + correctionAmplifier; }
            if (randomDir == 0) { cameraPan = cameraPan - correctionAmplifier; }
        }
        else { DidFwdRayHit = false; }

        if (Physics.Raycast(cam.position, rayLeftV1, out hit, 4.0f))
        {
            Debug.DrawLine(cam.position, hit.point);
            distanceAwayFix = distanceAwayFix + correctionAmplifier * 2; 
            cameraPan = cameraPan - correctionAmplifier;
        }

        if (Physics.Raycast(cam.position, rayRightV1, out hit, 4.0f))
        {
            Debug.DrawLine(cam.position, hit.point);
            distanceAwayFix = distanceAwayFix + correctionAmplifier * 2; 
            cameraPan = cameraPan + correctionAmplifier;
        }
        
        if(Physics.Raycast(cam.position, rayLeftV2, out hit, 3.50f))
        {
            Debug.DrawLine(cam.position, hit.point);
            cameraPan = cameraPan - correctionAmplifier;
        }

        if (Physics.Raycast(cam.position, rayRightV2, out hit, 3.5f))
        {
            Debug.DrawLine(cam.position, hit.point);
            cameraPan = cameraPan + correctionAmplifier;
        }

        if (Physics.Raycast(cam.position, rayRightV3, out hit, 3.0f))
        {
            Debug.DrawLine(cam.position, hit.point);
            cameraPan = cameraPan + correctionAmplifier;
        }

        if (Physics.Raycast(cam.position, rayLeftV3, out hit, 3.0f))
        {
            Debug.DrawLine(cam.position, hit.point);
            cameraPan = cameraPan - correctionAmplifier;
        }

        if (Physics.Raycast(cam.position, rayRightV4, out hit, 3.0f))
        {
            Debug.DrawLine(cam.position, hit.point);
            cameraPan = cameraPan + correctionAmplifier;
            distanceAwayFix = distanceAwayFix - correctionAmplifier * 20;
        }

        if (Physics.Raycast(cam.position, rayLeftV4, out hit, 3.0f))
        {
            Debug.DrawLine(cam.position, hit.point);
            cameraPan = cameraPan - correctionAmplifier;
            distanceAwayFix = distanceAwayFix - correctionAmplifier * 20;
        }

        if (Physics.Raycast(cam.position, rayDown, out hit, distanceUp))
        {
            Debug.DrawLine(cam.position, hit.point);
            distanceUpFix = distanceUpFix + correctionAmplifier*20;
        }

        if (Physics.Raycast(cam.position, rayBehind1, out hit, 1.0f))
        {
            //Debug.DrawLine(cam.position, hit.point);
            distanceAwayFix = distanceAwayFix - correctionAmplifier * 20;
        }

        if (Physics.Raycast(cam.position, rayBehind2, out hit, 1.0f))
        {
            Debug.DrawLine(cam.position, hit.point);
            distanceAwayFix = distanceAwayFix - correctionAmplifier * 20;
        }

        if (Physics.Raycast(cam.position, rayBehind3, out hit, 1.0f))
        {
            Debug.DrawLine(cam.position, hit.point);
            distanceAwayFix = distanceAwayFix - correctionAmplifier * 20;
        }

        if (distanceAwayFix <= distanceAway)
        {
            distanceAwayFix = distanceAwayFix + correctionAmplifier * 10;
            Debug.DrawLine(cam.position, hit.point);
            //Debug.Log(distanceUpFix + " " + distanceUp);
        }
        else
        {
            distanceAwayFix = distanceAwayFix - correctionAmplifier * 10;
        }

        if (distanceUpFix > distanceUp)
        {
            distanceUpFix = distanceUpFix - correctionAmplifier * 5;
            //Debug.Log(distanceUpFix + " " + distanceUp);
            Debug.DrawLine(cam.position, hit.point);
        }

        Vector3 m_offset = new Vector3(Mathf.Sin(cameraPan) * 2, 0, Mathf.Sin(cameraPan + (Mathf.PI/2)) * 2);
        targetPosition = middlePosition + Vector3.up * distanceUpFix - m_offset * distanceAwayFix;

        //Debug.DrawRay(playerOne.position, Vector3.up * distanceUp, Color.red);
        //Debug.DrawRay(playerOne.position, -1f * playerOne.forward * distanceAway, Color.red );
        //Debug.DrawRay(playerTwo.position, -1f * playerTwo.forward * distanceAway, Color.red);
        //Debug.DrawLine(playerOne.position, targetPosition, Color.green);
        //Debug.DrawLine(playerTwo.position, targetPosition, Color.green);
        //Debug.DrawLine(transform.position, middlePosition, Color.blue);

        // Rotation
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smooth);
        transform.LookAt(middlePosition + offset);
    }

    Vector3 findMiddlePosition(Vector3 vec1, Vector3 vec2)
    {
        Vector3 middlePosition = (vec1 + vec2) / 2.0f;
        return middlePosition;
    }

    void startZoom(float to)
    {
        StartCoroutine(Zoom(to));
    }

    void resetFOV()
    {
        StartCoroutine(ResettingFOV());


    }
    IEnumerator ResettingFOV()
    {
        Camera.main.DOFieldOfView(60, 5.0f);

        yield return null;
    }

    IEnumerator Zoom(float to)
    {
        Camera.main.DOFieldOfView(to, 5.0f);

        yield return null;
    }

    IEnumerator Guide(float angle)
    {
        /* 
         * Should take an object as param,
         * find angle of the line between middle-of-players and the param object
         * find the value closest to cameraPan that produces the angle pointing from middle-of-players to object of interest
         * tween cameraPan from old value to the one found above
         */

        yield return null;
    }

    public void changeOffset(float x, float y, float z)
    {
        offset.x += x;
        offset.y += y;
        offset.z += z;


    }

}
