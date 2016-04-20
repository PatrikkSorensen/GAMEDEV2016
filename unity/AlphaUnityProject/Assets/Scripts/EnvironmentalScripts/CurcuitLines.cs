using UnityEngine;
using System.Collections;

public class CurcuitLines : MonoBehaviour {

    public GameObject lineStart;
    public Transform[] lineDots;

    private LineRenderer lineRenderer;
    //TODO: Make this be a for each children instead of public assignment
    void Start()
    {
        lineRenderer = lineStart.AddComponent<LineRenderer>();
        lineRenderer.SetVertexCount(lineDots.Length);
    }
    void Update()
    {
        DrawLines();
    }

    void DrawLines()
    {
        if (lineDots.Length == 0)
            return;

        Debug.Log("LineDots length: " + lineDots.Length);

        for(int i = 0; i < lineDots.Length; i++)
        {
            Debug.Log("Setting line..." + i + " to position: " + lineDots[i].position);
            lineRenderer.SetPosition(i, lineDots[i].position); 
        }
    }
}
