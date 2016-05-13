using UnityEngine;
using System.Collections;
using DG.Tweening; 

public class TrailerCamera : MonoBehaviour {

    public float duration = 4.0f;
    public Vector3 endposition;
    public KeyCode keycode; 

    private Camera cam; 

    void Start()
    {
        cam = Camera.main; 
    }

    void Update()
    {
        if (Input.GetKey(keycode))
            PlayScene(); 
    }

    void PlayScene()
    {
        cam.transform.DOMove(endposition, duration);
    }
     
}
