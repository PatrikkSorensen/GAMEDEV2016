using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CurcuitChanneller : MonoBehaviour {

    // Context
    public GameObject lineStart;
    public List<GameObject> lineObjects = new List<GameObject>();

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
        m_lineTracer.GetComponent<Point>().Initiate(lineObjects[0].transform.position, 5.0f);
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

        Destroy(m_lineTracer.GetComponent<Point>());

        // For all lineObjects, render lines between them. 
        for (int i = 0; i < lineObjects.Count - 1; i++)
        {
            m_lineTracer = lineObjects[i];
            Point p = m_lineTracer.GetComponent<Point>(); 
           p.Initiate(lineObjects[i + 1].transform.position, 2.0f);

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

            Destroy(m_lineTracer.GetComponent<Point>());
            HasChannelled = true;
            // Do end line visuals here results here... e.g light up the whole line...
        }
        // ------------------------------------------------- END ------------------------------------------------- // 
    }
}
