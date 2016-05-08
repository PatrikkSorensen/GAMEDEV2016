using UnityEngine;
using System.Collections;

public class RotatePoint : Point {

    public GameObject[] switchPoints;
    public float changeDuration; 

    public GameObject activePoint;

    void Start()
    {
        ShouldDraw = true;

        if (switchPoints.Length == 0)
            Debug.LogWarning(gameObject.name + ": There is no switchPoints assigned to rotatePoint");
        else 
            activePoint = switchPoints[0];

        //Initiate(activePoint.transform.position, m_duration);
        //base.BeginDrawing();

    }

    protected override void Update()
    {

        if (Input.GetKey(KeyCode.Y) && m_isInitiated)
        {
            ChangeEndPoint(switchPoints[0], changeDuration);
        }
           
        if (Input.GetKey(KeyCode.V) && m_isInitiated)
        {
            ChangeEndPoint(switchPoints[1], changeDuration);
        }

        base.Update(); 
    }

    protected override void DrawLines()
    {
        base.DrawLines();

    }

    protected override void CreateLineTracer(Vector3 position)
    {
        base.CreateLineTracer(position);
    }

    public override void Initiate(Vector3 v, float duration)
    {
        base.Initiate(v, duration);
    }

    protected override void evaluateStatus()
    {
    }

    public void AttachLineToPlayer(GameObject player)
    {
        m_isDrawn = false;
        IsDrawing = true;
        m_targetPoint = player.transform.position;
        Vector3[] newpoints = { transform.position, player.transform.position};
        m_points = newpoints;

        CreateLineTracer(transform.position);
        BeginDrawing();
    }

    public void ChangeEndPoint(GameObject endPoint, float duration)
    {
        Debug.Log("Changing end point to " + endPoint);
        IsDrawing = true;
        activePoint = endPoint; 

        Vector3[] newpoints = { transform.position, transform.position };
        m_points = newpoints;
        m_isDrawn = false;
        m_targetPoint = endPoint.transform.position; 
        m_duration = duration; 

        CreateLineTracer(transform.position);
        BeginDrawing(); 
    }
}
