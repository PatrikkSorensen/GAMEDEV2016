using UnityEngine;
using System.Collections;

public class PowerPoint : Point
{

    public GameObject lightstation;
    private LightStationScript ls;     
    private CurcuitChanneller m_lsChanneller; 

    void Start()
    {
        ls = lightstation.GetComponent<LightStationScript>();
        m_lsChanneller = lightstation.GetComponentInChildren<CurcuitChanneller>(); 
    } 
    
    protected override void Update()
    {
        if (!ShouldDraw)
            evaluateStatus();

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
        if (!m_lsChanneller)
            Debug.LogWarning(gameObject.name + ": has no curcuitchanneller found on " + lightstation.name);

        if (m_lsChanneller.HasChannelled)
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

