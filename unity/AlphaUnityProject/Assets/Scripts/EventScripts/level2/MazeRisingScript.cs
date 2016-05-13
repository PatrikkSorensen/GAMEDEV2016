using UnityEngine;
using System.Collections;
using DG.Tweening; 

public class MazeRisingScript : MonoBehaviour {
    public GameObject mazeToMove;
    public float distanceToMove = -2.0f;
    public float duration = 10.0f;
    public AnimationCurve moveCurve, soundCurve; 
    public KeyCode keycode;
    public AudioClip RumbleSound, beginningSound;

    //private CameraShake camShake;
    private bool triggered = false;
    private AudioSource openSource, rumbleSource;

    void Start()
    {
        //camShake = Camera.main.GetComponent<CameraShake>();
        //camShake.shake_decay = duration;
        moveBelowGround();
    }

    void Update()
    {
        if (Input.GetKey(keycode) && !triggered)
        {
            triggered = true;
            TriggerScene();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!triggered)
        {
            triggered = true;
            TriggerScene();
        }
    }

    void moveBelowGround()
    {
        foreach (Transform child in mazeToMove.transform)
        {
            child.Translate(0,distanceToMove,0);
        }
    }

    void TriggerScene()
    {
        var rumbleSource = gameObject.AddComponent<AudioSource>();
        var openSource = gameObject.AddComponent<AudioSource>();
        openSource.clip = beginningSound;
        rumbleSource.clip = RumbleSound;
        rumbleSource.loop = true;
        //camShake.Shake();
        rumbleSource.Play();
        openSource.Play();
        StartCoroutine(MoveMaze());
        var fadeTime = mazeToMove.transform.childCount * 0.05f; //assuming flat distribution, n * (0.5 / 10)
        StartCoroutine(StopSound(duration, rumbleSource, fadeTime));
    }

    IEnumerator MoveMaze()
    {
        foreach (Transform child in mazeToMove.transform)
        {
            float location2move2 = child.position.y - distanceToMove;
            child.DOMoveY(location2move2, duration).SetEase(moveCurve);
            yield return new WaitForSeconds(Random.value/10);
        }
    }

    IEnumerator StopSound(float time2wait, AudioSource rumble, float fadeTime)
    {
        yield return new WaitForSeconds(time2wait);
        rumble.DOFade(0, fadeTime).SetEase(soundCurve);
        yield return new WaitForSeconds(fadeTime);
        rumble.Stop();
    }
}
