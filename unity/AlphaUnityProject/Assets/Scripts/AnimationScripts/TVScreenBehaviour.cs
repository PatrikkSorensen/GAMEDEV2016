using UnityEngine;
using System.Collections;
using DG.Tweening; 

public class TVScreenBehaviour : MonoBehaviour {

    public Material offscreenMat; 
    Renderer r;
    Material tutScreen, offScreen; 

    void Start()
    {
        r = GetComponent<Renderer>();
        tutScreen = r.material; 
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            r.material = offscreenMat;
            offscreenMat = r.material; 
        }


        if (Input.GetKeyDown(KeyCode.X))
            r.material = tutScreen;

        if (Input.GetKeyDown(KeyCode.T))
            tutScreen.DOOffset(new Vector2(1.0f, 0.0f), 1.0f);
    }

}
