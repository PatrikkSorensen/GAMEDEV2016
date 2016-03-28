using UnityEngine;
using System.Collections;

public class RessurectionScript : MonoBehaviour {

    public AudioClip clip;

    private AudioSource audioSource;
    private Animator anim;
    private GameObject playerOne, playerTwo, storyWall;
    private EventController eventController;
    private bool sceneIsFinished = false;  

    void Awake()
    {
        GameObject controller = GameObject.FindGameObjectWithTag("AudioController");
        audioSource = controller.GetComponent<AudioSource>();
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
}
