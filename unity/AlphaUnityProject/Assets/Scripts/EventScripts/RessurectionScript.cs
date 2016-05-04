using UnityEngine;
using System.Collections;
using DG.Tweening;

public class RessurectionScript : MonoBehaviour {

    public AudioClip clip, musicClip;
    public RobotChatScript chat;
    public float fadeInTime, fadeOutTime = 5.0f; 

    private AudioSource sfxSource, musicSource;
    private GameObject B4, MiMi, sfxController, musicController;
    private EventController eventController;
    private bool sceneIsFinished = false;  

    void Start()
    {
        // TODO: Refactor this
        musicController = GameObject.FindGameObjectWithTag("MusicController");

        musicSource = musicController.GetComponent<AudioSource>(); 
        sfxSource = musicController.AddComponent<AudioSource>();

        B4 = GameObject.FindGameObjectWithTag("B4");
        MiMi = GameObject.FindGameObjectWithTag("MiMi");
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "B4")
            triggerRessurcetionEvent();
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.X))
            triggerRessurcetionEvent();

        if (sceneIsFinished)
        {
            MiMi.GetComponent<PlayerController>().enabled = true;
            B4.GetComponent<PullOfLove>().enabled = true;
            B4.GetComponent<EstablishBond>().enabled = true;

            // Change camera
            CameraController cameraController = Camera.main.GetComponent<CameraController>();
            cameraController.changeCameraType(CameraController.CameraTypes.THIRD_PERSON_CAMERA);

            Destroy(GetComponent<RessurectionScript>());
        }
    }

    public void triggerRessurcetionEvent()
    {
        StartCoroutine(MusicFade(fadeInTime, fadeOutTime, musicSource, musicClip));

        //at.startChat();
        // send notification to event handler
        // eventController.handleEvent(ressurectionEvent);

        sceneIsFinished = true;
    }


    IEnumerator MusicFade(float fadeOut, float fadeIn, AudioSource musicSource, AudioClip newClip)
    {
        musicSource.DOFade(0.0f, fadeOut);
        yield return new WaitForSeconds(fadeOut);

        musicSource.clip = newClip; 
        musicSource.volume = 0.0f;
        musicSource.Play();
        musicSource.DOFade(1.0f, fadeIn);

        yield return null;
    }

}
