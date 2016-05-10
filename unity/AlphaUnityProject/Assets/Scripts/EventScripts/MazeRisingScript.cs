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
    private AudioSource[] SfxSounds;

    void Start()
    {
        camShake = Camera.main.GetComponent<CameraShake>();
        SfxSounds = gameObject.GetComponents<AudioSource>();

    }

    void Update()
    {
        if (Input.GetKey(keycode))
            TriggerScene(); 
    }

    void TriggerScene()
    {
        SfxSounds[0].Play();
        SfxSounds[1].Play();
        mazeToMove.transform.DOMoveY(yPosition, duration).SetEase(curve);
        camShake.Shake();
        new WaitForSeconds(duration);
        SfxSounds[1].Play();
    }
}
