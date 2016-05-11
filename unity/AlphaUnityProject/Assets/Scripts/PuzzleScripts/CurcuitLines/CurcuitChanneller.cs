using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CurcuitChanneller : MonoBehaviour {

    // Context
    public GameObject lineStart;
    public KeyCode debugKey; 

    // Visuals 
    public float startWidth, endWidth = 0.3f;
    public Material lineMaterial;

    private GameObject m_lineTracer;
    private LineRenderer m_lr;
    private bool m_channeling, m_hasChannelled, m_isWaiting = false;
    private Point m_currentPoint;  

    public bool HasChannelled
    {
        get { return m_hasChannelled; }
        set { m_hasChannelled = value; }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(debugKey) && !m_channeling)
        {
            m_channeling = true; 
            Debug.Log("Started Coroutine"); 
            StartCoroutine(BeginChanneling());
        }
    }

    IEnumerator BeginChanneling()
    {
        if (HasChannelled || m_channeling || m_isWaiting)
            StopCoroutine(BeginChanneling());

        m_channeling = true;
        // ------------------------------------------------- BEGIN ------------------------------------------------- // 
        m_lineTracer = lineStart;
        Vector3 endPoint = lineStart.GetComponent<Point>().endPoint.transform.position; 
        m_lineTracer.GetComponent<Point>().Initiate(endPoint, 5.0f);
        m_lineTracer.GetComponent<Point>().BeginDrawing();

        // Visuals: 
        m_lr = m_lineTracer.GetComponent<LineRenderer>();
        m_lr.SetWidth(startWidth, endWidth);
        m_lr.material = lineMaterial;
        m_lr = m_lineTracer.GetComponent<LineRenderer>();
       
        while (!m_lineTracer.GetComponent<Point>().IsDrawn)
        {
            
            Debug.Log("Chaneller: waiting for: " + m_lineTracer.name + " to draw...");
            yield return new WaitForSeconds(1.0f);
        }

        m_lineTracer = m_lineTracer.GetComponent<Point>().endPoint;

        while (m_lineTracer.GetComponent<Point>())
        {
            Point m_currentPoint = m_lineTracer.GetComponent<Point>();

            if (m_currentPoint.GetType() == typeof(RotatePoint))
            {
                RotatePoint rp = m_lineTracer.GetComponent<RotatePoint>();
                rp.Initiate(rp.activePoint.transform.position, rp.m_duration); // TODO: Do it with m_currentPoint instead

                while (!rp.IsDrawn)
                {
                    GameObject rotateEndPoint = rp.activePoint;
                    Point m_endPoint = rotateEndPoint.GetComponent<Point>();

                    while (!m_endPoint.endPoint) // RotatePoint decides which is the right endpoint, right now it is a blind point
                    {
                        m_currentPoint.BeginDrawing();
                        m_endPoint = rp.activePoint.GetComponent<Point>();
                        yield return new WaitForSeconds(1.0f);
                    }

                    m_currentPoint.BeginDrawing();
                    yield return new WaitForSeconds(1.0f);
                }
            }
            else if (m_currentPoint.GetType() == typeof(PowerPoint))
            {
                m_currentPoint.Initiate(m_currentPoint.endPoint.transform.position, m_currentPoint.m_duration);

                while (!m_currentPoint.IsDrawn)
                {
                    while (!m_currentPoint.ShouldDraw)
                    {
                        m_currentPoint.BeginDrawing();
                        Debug.Log("Encountered PowerPoint, shouldDraw: " + m_currentPoint.ShouldDraw);
                        yield return new WaitForSeconds(1.0f);
                    }

                    m_currentPoint.ShouldDraw = true; // TODO: fix this bug
                    m_currentPoint.BeginDrawing();
                    yield return new WaitForSeconds(1.0f);
                }

            }
            else if (m_currentPoint.GetType() == typeof(Point))
            {
                m_currentPoint.Initiate(m_currentPoint.endPoint.transform.position, m_currentPoint.m_duration);
                

                while (!m_currentPoint.IsDrawn)
                {
                    if (m_currentPoint.ShouldDraw)
                        m_currentPoint.BeginDrawing();

                    Debug.Log("waiting on ..." + m_lineTracer.name);
                    yield return new WaitForSeconds(1.0f);
                } 
            } 
            else
            {
                Debug.LogWarning("Curcuit chaneller is trying to run through a gameobject which has a undefined point class.");
                break; 
            }

            m_lineTracer = m_lineTracer.GetComponent<Point>().endPoint;
        }

        Debug.Log("Curcuit lines has finished drawing all your points.");
        GameObject.Find("RoundElectronic").GetComponent<Animator>().SetBool("hasPower", true);
        // ------------------------------------------------- END ------------------------------------------------- // 
    }

    private void DrawRotatePoint(RotatePoint rp)
    {

    }

    bool EvaluateRotatePoint(RotatePoint point)
    {

        return point.IsDrawn;
    }

    bool HandlePowerPoint()
    {
        return true;
    }

    private void SetLineVisuals()
    {
        m_lr = m_lineTracer.GetComponent<LineRenderer>();
        m_lr.SetWidth(startWidth, endWidth);
        m_lr.material = lineMaterial;
        m_lr = m_lineTracer.GetComponent<LineRenderer>();
    }
}
