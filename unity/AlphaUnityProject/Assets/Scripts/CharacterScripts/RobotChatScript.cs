using UnityEngine;
using System.Collections;

public class RobotChatScript : MonoBehaviour {

    public AudioClip[] B4vox, MiMivox;


    private AudioSource speaking;
    private AudioSource B4, MiMi;
    private int who;             //1 = B4        2 = MiMi


	// Use this for initialization
	void Start () {

        B4 = GameObject.FindGameObjectWithTag("B4").GetComponent<AudioSource>();
        MiMi = GameObject.FindGameObjectWithTag("MiMi").GetComponent<AudioSource>();

        B4vox = new AudioClip[]{(AudioClip)Resources.Load("Sound/Voices/B4/blabber"),
                                (AudioClip)Resources.Load("Sound/Voices/B4/blabber2"),
                                (AudioClip)Resources.Load("Sound/Voices/B4/blabber3"),
                                (AudioClip)Resources.Load("Sound/Voices/B4/bored"),
                                (AudioClip)Resources.Load("Sound/Voices/B4/happy"),
                                (AudioClip)Resources.Load("Sound/Voices/B4/happy2"),
                                (AudioClip)Resources.Load("Sound/Voices/B4/melancholy"),
                                (AudioClip)Resources.Load("Sound/Voices/B4/melancholy2"),
                                (AudioClip)Resources.Load("Sound/Voices/B4/sad"),
                                (AudioClip)Resources.Load("Sound/Voices/B4/sad2"),
                                (AudioClip)Resources.Load("Sound/Voices/B4/sick")};

        MiMivox = new AudioClip[]{(AudioClip)Resources.Load("Sound/Voices/MiMi/happy"),
                                     (AudioClip)Resources.Load("Sound/Voices/MiMi/sad")};

        if (Random.value > 0.5)
        {
            // iff greater than 0.5 assign mimi
            who = 2;
            speaking = MiMi;
        }
        else
        {
            //assign b4
            who = 1;
            speaking = B4;
        }

        //StartCoroutine(Chat()); //for debugging
	
	}

    public void SwitchSpeaker(){

        if(who == 1) //1 = B4        2 = MiMi
        {
            who = 2;
            speaking = MiMi;

        }
        else
        {
            who = 1;
            speaking = B4;

        }
    }
	
	// Update is called once per frame
	void Update () {
	    // check trigger conditions
	}

    public void startChat(){
        StartCoroutine(Chat());
    }

    public IEnumerator Chat()
    {
        float responseProb = 0.95f;
        int amtOfLoops = 1;
        //Debug.Log("Began chat");


        for (int i = 0; i < amtOfLoops; i++)
        {
            //Debug.Log("loop " + i + " began.");
            float timeToWait;

            //pick appropriate sound
            if(who == 1)
            {
                 int clipToBePlayed = Random.Range(0, B4vox.Length);
                 B4.clip = B4vox[clipToBePlayed];
                 timeToWait = B4vox[clipToBePlayed].length;

            }
            else
            {
                int clipToBePlayed = Random.Range(0, MiMivox.Length);
                MiMi.clip = MiMivox[clipToBePlayed];
                timeToWait = MiMivox[clipToBePlayed].length;
            }

            //play sound
            if(who == 1)
            {
                B4.Play();
            }
            else
            {
                MiMi.Play();
            }

            //wait
            yield return new WaitForSeconds(timeToWait + 0.5f);

            //should we get a response?
            float chance = Random.value;
            //Debug.Log(responseProb + " > " + chance);

            if (chance < responseProb)
            {
                amtOfLoops++;
                responseProb = responseProb - 0.05f;
            }

            SwitchSpeaker();
        }
        
        yield return null;
    }
}