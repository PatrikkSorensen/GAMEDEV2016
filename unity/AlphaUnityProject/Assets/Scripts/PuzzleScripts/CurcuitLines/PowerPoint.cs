using UnityEngine;
using System.Collections;

public class PowerPoint : Point
{

    public GameObject lightstation;
    private CurcuitChanneller powerSourceLines;
    
    void Start()
    {
        powerSourceLines = lightstation.GetComponentInChildren<CurcuitChanneller>(); 
    } 
    
    protected override void Update()
    {
        if (!ShouldDraw)
            evaluateStatus();

        //if (ShouldDraw)
        //    Debug.Log("I should draw!");

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
        if (!powerSourceLines)
            Debug.LogWarning(gameObject.name + ": There was no curcuit line component connected to lightstation: " + lightstation.name);

        if (powerSourceLines.HasChannelled)
        {
            ShouldDraw = true;
        }
    }

    public override void BeginDrawing()
    {
        base.BeginDrawing();
    }


}

