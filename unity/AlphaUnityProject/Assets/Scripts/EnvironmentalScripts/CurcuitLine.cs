using UnityEngine;
using System.Collections;
using DG.Tweening;

public class CurcuitLine : MonoBehaviour {

    private Vector3[] m_points;
    private GameObject lineTracer; 
    private LineRenderer lr;
    private Vector3 targetPoint; 
    private bool isInitiated, isDrawn = false;

    public bool IsDrawn
    {
        get { return isDrawn; }
        set { isDrawn = value;}
    }

    void Update () {
        if (!isInitiated || isDrawn)
            return;

        if (Vector3.Distance(lineTracer.transform.position, targetPoint) <= 0.5f)
            isDrawn = true;
            
        DrawLines(); 
	}

    private void DrawLines()
    {
        m_points[1] = lineTracer.transform.position; 
        lr.SetPositions(m_points);
        
    }

    public void Initiate(Vector3 b)
    {
        //Debug.Log("Intiating with params: " + transform.position + ", " + b);
        lr = gameObject.AddComponent<LineRenderer>();
        lr.SetVertexCount(2); 
        
        Vector3[] points = new Vector3[2];
        points[0] = transform.position;
        points[1] = transform.position;

        CreateLineTracer(transform.position);
        targetPoint = b;
        lineTracer.transform.DOMove(targetPoint, 5.0f);
        
        m_points = points; 

        isInitiated = true; 
    }

    void CreateLineTracer(Vector3 position)
    {
        lineTracer = new GameObject();
        lineTracer.transform.position = position;
        lineTracer.name = "LineTracer";
    }
}
