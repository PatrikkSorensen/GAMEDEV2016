using UnityEngine;
using System.Collections;

public class RibbonEndScript : MonoBehaviour {

    public AudioClip FadeInMusicClip;

    private bool m_MiMiOnPlatform, m_B4OnPlatform = false;
    private AudioSource musicController;
    // Use this for initialization
    void Start()
    {
        musicController = GameObject.FindGameObjectWithTag("MusicController").GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "B4")
            m_B4OnPlatform = true; ;

        if (coll.gameObject.tag == "MiMi")
            m_MiMiOnPlatform = true;

        // Fade in music here 
        if (m_MiMiOnPlatform && m_B4OnPlatform)
        {
            musicController.clip = FadeInMusicClip;
            musicController.Play();
        }

    }
}
