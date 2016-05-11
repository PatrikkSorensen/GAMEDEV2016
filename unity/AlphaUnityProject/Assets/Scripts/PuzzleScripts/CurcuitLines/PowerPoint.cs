using UnityEngine;
using System.Collections;

public class PowerPoint : Point
{

    public GameObject lightstation;
    private LightStationScript ls;     

    void Start()
    {
        ls = lightstation.GetComponent<LightStationScript>(); 
    } 
    
    protected override void Update()
    {
        if (!ShouldDraw)
            evaluateStatus();

        //Debug.Log("PowerPoint: Breaking update loop, params: [initiated:" + m_isInitiated + "] [isDrawn " + IsDrawn + "] + [shouldDraw: " + ShouldDraw + "]"); 
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
        if (!ls)
            Debug.LogWarning(gameObject.name + ": There was no lightstationscript connected to lightstation: " + lightstation.name);

        if (ls.IsActive)
        {
            Debug.Log("I should draw!");
            ShouldDraw = true;
        }
    }

    public override void BeginDrawing()
    {
        base.BeginDrawing();
    }


}

