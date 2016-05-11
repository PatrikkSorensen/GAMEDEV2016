using UnityEngine;
using System.Collections;
using DG.Tweening; 

public class FlickeringLight : MonoBehaviour {

    public AnimationCurve lightCurve;
    public Light lightSource;
    public float duration, endIntensity = 2.0f; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.X))
            FlickerLight(); 

	}

    public void FlickerLight()
    {
        lightSource.DOIntensity(endIntensity, duration).SetEase(lightCurve); 
    }
}
