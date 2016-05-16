using UnityEngine;
using System.Collections;
using DG.Tweening;
using System;

public class Point : MonoBehaviour {

    public float m_duration;
    public GameObject nextPoint;
    public Material lineMaterial;
    public float endWidth = 0.3f;
    public float startWidth = 0.3f;

    protected Vector3[] m_points;
    protected GameObject m_lineTracer;
    protected LineRenderer m_lr;
    protected Vector3 m_targetPoint;
    protected bool m_isInitiated, m_isDrawn, m_isAttachedtoPlayer;
    protected bool m_shouldDraw;
    private bool isDrawing;

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

    public bool IsDrawing
    {
        get { return isDrawing; }
        set { isDrawing = value;}
    }


    protected virtual void Update () {


        if (!m_isInitiated || IsDrawn || !ShouldDraw)
        {
            //Debug.Log("Breaking update loop, params: [initiated:" + m_isInitiated + "] [isDrawn " + IsDrawn + "] + [shouldDraw: " + m_isDrawn + "]"); 
            return;
        }
           
            

        if (Vector3.Distance(m_lineTracer.transform.position, m_targetPoint) <= 0.5f)
        {
            Debug.Log(gameObject.name + " has reached its endpoint.");
            IsDrawing = false; 
            IsDrawn = true;
            //Destroy(m_lineTracer);
        } 

        DrawLines();
    }

    protected virtual void DrawLines()
    {
        Debug.Log("LineTracer: " + m_lineTracer.transform.position + " is headed towards:  " + m_targetPoint);
        m_points[1] = m_lineTracer.transform.position;
        m_lr.SetPositions(m_points);

    }

    public virtual void Initiate(Vector3 v, float duration)
    {
        Debug.Log("Line initiated with start point: " + transform.name + ": " + transform.position + " and line end point: " + v + ", with draw duration: " + duration);
        m_lr = gameObject.AddComponent<LineRenderer>();
        m_lr.SetVertexCount(2);
        m_lr.material = lineMaterial;
        m_lr.SetWidth(startWidth, endWidth);

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

    // TODO: Maybe combine
    public virtual void BeginDrawing()
    {
        m_lineTracer.transform.DOMove(m_targetPoint, m_duration);
        IsDrawing = true; 
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
