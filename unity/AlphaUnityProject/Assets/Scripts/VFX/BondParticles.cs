using UnityEngine;
using System.Collections;

public class BondParticles : MonoBehaviour {

    public ParticleSystem ps;
    public float width, height = 1.0f; 


    GameObject B4, MiMi;
    private float deadZone = 1.0f; 
    void Start()
    {
        B4 = GameObject.FindGameObjectWithTag("B4");
        MiMi = GameObject.FindGameObjectWithTag("MiMi");
    }

    void Update()
    {
        SetPosition();
        SetShapeSize(); 
    }


    void SetPosition()
    {
        Vector3 v = Vector3.Lerp(B4.transform.position, MiMi.transform.position, 0.5f);
        transform.LookAt(B4.transform); 
        transform.position = v;


    }

    void SetShapeSize()
    {
        // Set box: 
        float distance = Vector3.Distance(MiMi.transform.position, B4.transform.position) - deadZone;
        Vector3 v = new Vector3(width, height, distance);
        ParticleSystem.ShapeModule sm = ps.shape;
        sm.box = v;
    }

}
