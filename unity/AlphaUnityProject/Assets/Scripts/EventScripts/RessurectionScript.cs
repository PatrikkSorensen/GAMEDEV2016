using UnityEngine;
using System.Collections;
using DG.Tweening;

public class RessurectionScript : MonoBehaviour {

    public AudioClip clip, musicClip;
    public RobotChatScript chat;

    private AudioSource audioSource;
    private Animator anim;
    private GameObject playerOne, playerTwo, storyWall;
    private EventController eventController;
    private bool sceneIsFinished = false;  

    void Awake()
    {
        GameObject controller = GameObject.FindGameObjectWithTag("AudioController");
        audioSource = controller.AddComponent<AudioSource>();
        playerOne = GameObject.FindGameObjectWithTag("B4");
        playerTwo = GameObject.FindGameObjectWithTag("MiMi");
        storyWall = GameObject.FindGameObjectWithTag("StoryWall"); // Make this variable public
        //eventController = GameObject.FindGameObjectWithTag("EventController").GetComponent<EventController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "B4")
        {
            Debug.Log("Trigger entered");
            triggerRessurcetionEvent();
        }
    }

    void Update()
    {
        if (sceneIsFinished)
        {
            playerTwo.GetComponent<PlayerController>().enabled = true;
            playerOne.GetComponent<PullOfLove>().enabled = true;
            playerOne.GetComponent<EstablishBond>().enabled = true;

            // Change camera
            CameraController cameraController = Camera.main.GetComponent<CameraController>();
            cameraController.changeCameraType(CameraController.CameraTypes.THIRD_PERSON_CAMERA);
            Debug.Log("End of routine"); 

            Destroy(GetComponent<RessurectionScript>());
        }
    }

    public void triggerRessurcetionEvent()
    {
        Debug.Log("EVENT: ressurection event");

        // trigger coroutines
        StartCoroutine(FadeInWallStory());

        // trigger animations

        // trigger sounds 
        audioSource.clip = clip;
        audioSource.Play();
        GameObject.FindGameObjectWithTag("MusicController").GetComponent<AudioSource>().clip = musicClip;
        StartCoroutine(MusicFadeIn(0.15f, 1.0f, 8.0f, GameObject.FindGameObjectWithTag("MusicController").GetComponent<AudioSource>()));
        //at.startChat();
        // send notification to event handler
        // eventController.handleEvent(ressurectionEvent);
    }

    IEnumerator FadeInWallStory()
    {
        for (float f = 0; f <= 1; f += 0.01f)
        {
            if (!storyWall)
                break; 

            Color c = storyWall.GetComponent<Renderer>().material.color;
            c.a = f;
            storyWall.GetComponent<Renderer>().material.color = c;
            //Debug.Log("Fading in! " + c.a + ", " + storyWall.name);
            yield return null;
        }

        sceneIsFinished = true;
    }

    IEnumerator MusicFadeIn(float from, float to, float duration, AudioSource musicSource)
    {
        musicSource.volume = from;
        musicSource.Play();
        musicSource.DOFade(to, duration);
        yield return null;
    }
}
