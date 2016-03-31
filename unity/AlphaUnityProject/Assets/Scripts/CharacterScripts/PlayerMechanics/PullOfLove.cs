using UnityEngine;
using System.Collections;

public class PullOfLove : MonoBehaviour {
    GameObject B4, MiMi;
    PlayerStatusScript B4Status, MiMiStatus;

    // Time variables for not spamming pull of love 
    public float timeDelay = 0.5f;
    public float pullEffect = 100.0f;
    public AudioClip pullClip;
    float startTime = 0.0f;

    //private AudioSource[] audioSources; // 0 = song, 1 = beamup, 2 = beamdown, 3 = channelling, 4 = pull, 5 = combine
    private AudioSource audioSource;

    // Debugging information: 
    Vector3 middlePosition; 

	void Start () {
        B4 = GameObject.FindGameObjectWithTag("B4");
        MiMi = GameObject.FindGameObjectWithTag("MiMi");

        // Sound 
        GameObject controller = GameObject.FindGameObjectWithTag("AudioController");
        if (!controller)
        {
            Debug.LogWarning("No audio Controller found. Creating one");
            controller = new GameObject();
            controller.name = "AudioController";
            controller.tag = "AudioController";
        }
        audioSource = controller.AddComponent<AudioSource>();
        audioSource.clip = pullClip;
    }

    void Update()
    {

        // This needs to be set here, but ideally needs to happen in another function. 
        // This is caused since playerStatusScript is created in the Start of another function i think 
        // TODO: Fix it
        if (!B4 || !MiMi)
        {
            Debug.LogWarning("WARNING: Cannot find B4 and MiMI on the scene. Destroying Pull of love script.");
            Destroy(gameObject.GetComponent<PullOfLove>());
            return;
        }

        MiMiStatus = MiMi.GetComponent<PlayerController>().playerStatus;
        B4Status = B4.GetComponent<PlayerController>().playerStatus;
            
        // Makes sure we can't spam the button
        if(B4Status.getBondStatus() && Input.GetButtonDown("B4Pull"))
        {
            float timeDifference = Time.time - startTime;
            if (timeDifference > timeDelay)
            {
                startTime = Time.time;
                PullPlayer(MiMi);
            }
        }

        if (MiMiStatus.getBondStatus() && Input.GetButtonDown("MiMiPull"))
        {
            float timeDifference = Time.time - startTime;
            if (timeDifference > timeDelay)
            {
                startTime = Time.time;
                PullPlayer(B4);
            }
        }
    }

    void PullPlayer(GameObject character)
    {
        // Add force in player direction 
        middlePosition = (B4.transform.position + MiMi.transform.position) / 2.0f;
        Vector3 forceVector = middlePosition - character.transform.position;
        character.GetComponent<Rigidbody>().AddForce(forceVector * pullEffect);

        //For debugging
        Debug.DrawLine(character.transform.position, middlePosition, Color.white);
        Debug.Log("Adding force with vec3: " + forceVector);

        // Notify player scripts of the pull effect 
        character.GetComponent<PlayerController>().DetachEnemies(); 

        //Trigger coroutine for x seconds to start spheres chase again
        //audioSources[4].Play();
        audioSource.Play();
    }
}
