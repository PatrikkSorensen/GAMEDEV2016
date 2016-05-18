using UnityEngine;
using System.Collections;

public class RotatePoint : Point {

    public GameObject defaultPoint; 
    public GameObject[] switchPoints;
    public float changeDuration; 
    public GameObject activePoint;

    private AudioSource m_source;
    private bool isLocked; 

    void Start()
    {
        m_source = GetComponent<AudioSource>(); 

        ShouldDraw = true;

        if (switchPoints.Length == 0)
            Debug.LogWarning(gameObject.name + ": There is no switchPoints assigned to rotatePoint");
        else
        {
            activePoint = defaultPoint;
            activePoint.GetComponent<ChangeRotationPoint>().isActive = true; 
        }

    }

    protected override void Update()
    {

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
    }

    public void AttachLineToPlayer(GameObject player)
    {
        m_isDrawn = false;
        IsDrawing = true;
        m_targetPoint = player.transform.position;
        Vector3[] newpoints = { transform.position, player.transform.position};
        m_points = newpoints;

        CreateLineTracer(transform.position);
        BeginDrawing();
    }

    public void ChangeEndPoint(GameObject endPoint, float duration)
    {
        IsDrawing = true;

        Debug.Log("Changing end point to " + endPoint);
        activePoint.GetComponent<ChangeRotationPoint>().isActive = false; 
        activePoint = endPoint;
        activePoint.GetComponent<ChangeRotationPoint>().isActive = true;

        Vector3[] newpoints = { transform.position, transform.position };
        m_points = newpoints;
        m_isDrawn = false;
        m_targetPoint = endPoint.transform.position; 
        m_duration = duration; 

        CreateLineTracer(transform.position);
        BeginDrawing(); 
    }

    public void LockRotationPoints()
    {
        foreach (GameObject gb in switchPoints)
            gb.GetComponent<ChangeRotationPoint>().LockSwitches();

        activePoint.GetComponent<ChangeRotationPoint>().activeParticles.Stop();

        if (!isLocked)
        {
            isLocked = true;
            m_source.Play();
        }
    }
}
