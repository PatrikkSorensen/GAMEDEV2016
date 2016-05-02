using UnityEngine;
using System.Collections;
using DG.Tweening; 

public class MazeRisingScript : MonoBehaviour {
    public GameObject mazeToMove;
    public float yPosition = -90.0f;
    public float duration = 10.0f;
    public AnimationCurve curve; 
    public KeyCode keycode;

    private CameraShake camShake; 

    void Start()
    {
        camShake = Camera.main.GetComponent<CameraShake>(); 
    }

    void Update()
    {
        if (Input.GetKey(keycode))
            TriggerScene(); 
    }

    void TriggerScene()
    {
        mazeToMove.transform.DOMoveY(yPosition, duration).SetEase(curve);
        camShake.Shake();
    }
}
