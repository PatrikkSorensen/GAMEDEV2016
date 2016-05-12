using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public Transform player;
    public float smooth = 1.5f;         // The relative speed at which the camera will catch up
    public float distanceAway = 2.0f;
    public float distanceUp = 2.0f;
    public float correctionAmplifier = 0.005f;

    private Vector3 targetPosition; 

    private Vector3 offset;
    private float cameraPan = 0.0f;
    private Vector3 viewPan = new Vector3(0, 1.0f, 0);
    private Transform cam;
    private bool DidFwdRayHit = false;
    private int randomDir = 0; // 0 for left 1 for right
    //TODO: Integrate smooth
    private float distanceUpFix;
    private float distanceAwayFix;

    void Start()
    {
        //offset = transform.position - player.position;
        offset = new Vector3(-3, 1, 0);
        distanceUpFix = distanceUp;
        distanceAwayFix = distanceAway;
    }



    void FixedUpdate()
    {
        if (Input.GetButton("CamRight"))
        {
            cameraPan = cameraPan + Time.deltaTime * 1.2f;

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

        if (Physics.Raycast(cam.position, rayLeftV2, out hit, 3.50f))
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
            distanceUpFix = distanceUpFix + correctionAmplifier * 20;
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

        Vector3 offset = new Vector3(Mathf.Sin(cameraPan) * 2, 0, Mathf.Sin(cameraPan + (Mathf.PI / 2)) * 2);


        targetPosition = player.position + Vector3.up * distanceUp - offset * distanceAway;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smooth);
        transform.LookAt(player.position + viewPan);
        Debug.DrawLine(transform.position, player.position); 
    }

    public void changeOffset(float x, float y, float z)
    {
        offset.x += x;
        offset.y += y;
        offset.z += z;


    }
}

