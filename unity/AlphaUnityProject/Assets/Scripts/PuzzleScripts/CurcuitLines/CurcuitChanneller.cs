using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CurcuitChanneller : MonoBehaviour {

    // Context
    public GameObject lineStart;

    // Visuals 
    public float startWidth, endWidth = 0.3f;
    public Material lineMaterial;

    private GameObject m_lineTracer;
    private LineRenderer m_lr;
    private bool m_channeling, m_hasChannelled, m_isWaiting = false;

    public bool HasChannelled
    {
        get { return m_hasChannelled; }
        set { m_hasChannelled = value; }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.C) && !m_channeling)
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
        Debug.Log("Beginning while loop."); 
        while (m_lineTracer.GetComponent<Point>())
        {
            Debug.Log("Start of while loop: " + m_lineTracer); 
            Point p = m_lineTracer.GetComponent<Point>();

            if (p.GetType() == typeof(RotatePoint))
            {
                Debug.Log("RotatePoint encountered..");
                RotatePoint rp = m_lineTracer.GetComponent<RotatePoint>();
                rp.Initiate(rp.activePoint.transform.position, rp.m_duration);

                while (!rp.IsDrawn)
                {
                    m_lineTracer = rp.activePoint;
                    Debug.Log("waiting on ..." + m_lineTracer.name);
                    while(rp.endPoint.name != "rotatepointend1")
                    {
                        m_lineTracer = rp.activePoint;
                        p.BeginDrawing();
                        Debug.Log("Inside endpoint check loop");
                        yield return new WaitForSeconds(1.0f);
                    }

                    yield return new WaitForSeconds(1.0f);
                    p.BeginDrawing();
                }

            } else if(p.GetType() == typeof(Point))
            {
                Debug.Log("Standard Point encountered..");
                
                if (!p.endPoint)
                {
                    Debug.Log(p.name + " had no end point, so can't draw further");
                    break; 
                }
                    

                // Initiate
                p.Initiate(p.endPoint.transform.position, p.m_duration);

                // Visuals: 
                m_lr = m_lineTracer.GetComponent<LineRenderer>();
                m_lr.SetWidth(startWidth, endWidth);
                m_lr.material = lineMaterial;
                m_lr = m_lineTracer.GetComponent<LineRenderer>();

                while (!p.IsDrawn)
                {
                    if (p.ShouldDraw)
                        p.BeginDrawing();

                    Debug.Log("waiting on ..." + m_lineTracer.name);
                    yield return new WaitForSeconds(1.0f);
                }

            //Destroy(m_lineTracer.GetComponent<Point>());
            m_lineTracer = m_lineTracer.GetComponent<Point>().endPoint;
            if (m_lineTracer == null)
            {
                Debug.Log("I SHOULD BREAK WHILE LOOP");
            } else
            {
                    Debug.Log(m_lineTracer);
            }

            HasChannelled = true;
            // Do end line visuals here results here... e.g light up the whole line...
            }
        }

        Debug.Log("Curcuit lines has finished drawing all your points.");
        GameObject.Find("RoundElectronic").GetComponent<Animator>().SetBool("hasPower", true);
        // ------------------------------------------------- END ------------------------------------------------- // 
    }
}
