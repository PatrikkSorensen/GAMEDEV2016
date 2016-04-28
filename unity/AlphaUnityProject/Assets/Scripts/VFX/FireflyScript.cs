using UnityEngine;
using System.Collections;

public class FireflyScript : MonoBehaviour {
    public float xBound;
    public float yBound;
    public float zBound;
    public float speed; 

    private bool onRoute = false;
    private Vector3 origin;
    private Vector3 destination; 
    private GameObject container; 

    void Start()
    {
        container = transform.parent.gameObject;
        origin = transform.position; 
    }

    void Update()
    {
        if (!onRoute)
        {
            destination = FindRoute();
        }

        Debug.DrawLine(transform.position, destination, Color.red);

        if (onRoute)
        {
            Move(); 
        }
        
        if(transform.position == destination)
        {
            onRoute = false; 
        }
    }

    void Move()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, destination, step);
    }

    Vector3 FindRoute()
    {
        float x = Random.Range(origin.x + xBound, origin.x + xBound * -1);
        float y = Random.Range(origin.y + yBound, origin.y + yBound * -1);
        float z = Random.Range(origin.z + zBound, origin.z + zBound * -1);
        onRoute = true; 
        return new Vector3(x, y, z);
    } 

    void DebugDrawing()
    {
        Debug.DrawRay(container.transform.position, Vector3.zero + new Vector3(xBound, 0, 0));
        Debug.DrawRay(container.transform.position, Vector3.zero + new Vector3(-xBound, 0, 0));
        Debug.DrawRay(container.transform.position, Vector3.zero + new Vector3(0, yBound, 0));
        Debug.DrawRay(container.transform.position, Vector3.zero + new Vector3(0, -yBound, 0));
        Debug.DrawRay(container.transform.position, Vector3.zero + new Vector3(0, 0, zBound));
        Debug.DrawRay(container.transform.position, Vector3.zero + new Vector3(0, 0, -zBound));

        // DEBUGGING: 
        Vector3[] debugPoints = new Vector3[4];

        Vector3 debugVector = container.transform.position + new Vector3(xBound, yBound, zBound);
        Vector3 debugVector2 = container.transform.position + new Vector3(xBound, yBound, -zBound);
        Vector3 debugVector3 = container.transform.position + new Vector3(-xBound, yBound, -zBound);
        Vector3 debugVector4 = container.transform.position + new Vector3(-xBound, yBound, zBound);

        Debug.DrawLine(debugVector, debugVector2, Color.blue);
        Debug.DrawLine(debugVector2, debugVector3, Color.blue);
        Debug.DrawLine(debugVector3, debugVector4, Color.blue);
        Debug.DrawLine(debugVector4, debugVector, Color.blue);

        debugVector.y += -2 * yBound;
        debugVector2.y += -2 * yBound;
        debugVector3.y += -2 * yBound;
        debugVector4.y += -2 * yBound;

        Debug.DrawLine(debugVector, debugVector2, Color.blue);
        Debug.DrawLine(debugVector2, debugVector3, Color.blue);
        Debug.DrawLine(debugVector3, debugVector4, Color.blue);
        Debug.DrawLine(debugVector, debugVector4, Color.blue);
    }
}
