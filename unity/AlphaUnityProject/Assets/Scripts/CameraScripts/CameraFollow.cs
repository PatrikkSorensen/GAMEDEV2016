using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public Transform player;
    public float smooth = 1.5f;         // The relative speed at which the camera will catch up

    [SerializeField]
    public float distanceAway = 2.0f;
    [SerializeField]
    public float distanceUp = 2.0f;

    private Vector3 targetPosition; 

    private Vector3 offset;
    private float cameraPan = 0.0f;
    private Vector3 viewPan = new Vector3(0, 1.0f, 0);
    //TODO: Integrate smooth

    void Start()
    {
        //offset = transform.position - player.position;
        offset = new Vector3(-2, 1, 0);
    }



    void FixedUpdate()
    {
        if (Input.GetButton("CamRight"))
        {
            cameraPan = cameraPan + Time.deltaTime;

        }
        if (Input.GetButton("CamLeft"))
        {
            cameraPan = cameraPan - Time.deltaTime;

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

