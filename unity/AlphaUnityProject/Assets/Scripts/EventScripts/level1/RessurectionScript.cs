using UnityEngine;
using System.Collections;
using DG.Tweening;

public class RessurectionScript : MonoBehaviour {

    public AudioClip clip, musicClip;
    public RobotChatScript chat;
    public float fadeInTime, fadeOutTime = 5.0f; 

    private AudioSource sfxSource, musicSource;
    private GameObject B4, MiMi, sfxController, musicController;
    private bool sceneIsFinished = false;
	private bool triggered = false;

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
		if (other.tag == "B4" && triggered == false) {
			triggerRessurcetionEvent ();
			triggered = true;
            B4.GetComponent<PlayerController>().canMove = false;
            B4.GetComponent<EstablishBond>().enabled = true;
			sfxSource.clip = clip;
			sfxSource.Play();
		}
    }

    void Update()
    {

        if (sceneIsFinished)
        {
            B4.GetComponent<PlayerController>().canMove = true;
            MiMi.GetComponent<PlayerController>().enabled = true;
            B4.GetComponent<EstablishBond>().enabled = true;

            // Change camera
            CameraController cameraController = Camera.main.GetComponent<CameraController>();
            cameraController.changeCameraType(CameraController.CameraTypes.THIRD_PERSON_CAMERA);

        }

        if (B4.GetComponent<EstablishBond>().isBonded())
        {
            sceneIsFinished = true;
        }
    }

    public void triggerRessurcetionEvent()
    {
        StartCoroutine(MusicFade(fadeInTime, fadeOutTime, musicSource, musicClip));

        //at.startChat();
        // send notification to event handler
        // eventController.handleEvent(ressurectionEvent);

        //sceneIsFinished = true;
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
