using UnityEngine;
using System.Collections;
using DG.Tweening;
using System;

public class Point : MonoBehaviour {

    public float m_duration; 

    protected Vector3[] m_points;
    protected GameObject m_lineTracer;
    protected LineRenderer m_lr;
    protected Vector3 m_targetPoint;
    protected bool m_isInitiated, m_isDrawn, m_isDrawing;
    protected bool m_shouldDraw;

    public bool IsDrawn
    {
        get { return m_isDrawn; }
        set { m_isDrawn = value; }
    }

    public bool ShouldDraw
    {
        get { return m_shouldDraw; }
        set { m_shouldDraw = value;}
    }

    protected virtual void Update () {
        

        if (!m_isInitiated || m_isDrawn || !ShouldDraw)
            return;

        if (Vector3.Distance(m_lineTracer.transform.position, m_targetPoint) <= 0.5f)
        {
            Debug.Log(gameObject.name + " has finished drawing!");
            m_isDrawing = false; 
            IsDrawn = true;
        }


        DrawLines();
    }

    protected virtual void DrawLines()
    {
        m_points[1] = m_lineTracer.transform.position;
        m_lr.SetPositions(m_points);

    }

    public virtual void Initiate(Vector3 v, float duration)
    {
        Debug.Log("Point initiated with targetEndpoint: " + v + ", and duration: " + duration);
        m_lr = gameObject.AddComponent<LineRenderer>();
        m_lr.SetVertexCount(2);

        Vector3[] points = new Vector3[2];
        points[0] = transform.position;
        points[1] = transform.position;

        m_targetPoint = v;
        m_duration = duration;
        m_points = points;

        CreateLineTracer(transform.position);

        m_isInitiated = true;
        evaluateStatus();

    }

    public virtual void BeginDrawing()
    {
        m_lineTracer.transform.DOMove(m_targetPoint, 5.0f);
        m_isDrawing = true; 
    }

    protected virtual void CreateLineTracer(Vector3 position)
    {
        m_lineTracer = new GameObject();
        m_lineTracer.transform.position = position;
        m_lineTracer.name = "LineTracer";
    }

    protected virtual void evaluateStatus()
    {
        m_shouldDraw = true; 
    }

}
