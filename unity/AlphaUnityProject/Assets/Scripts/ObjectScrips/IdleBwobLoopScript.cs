using UnityEngine;
using System.Collections;

public class IdleBwobLoopScript : MonoBehaviour {
    public AudioClip loopSound;

    private AudioSource sfxSource;
	// Use this for initialization
	void Start () {
        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.playOnAwake = false;
        sfxSource.clip = loopSound;
        sfxSource.loop = true;
        sfxSource.spatialBlend = 1.0f;
        sfxSource.volume = 0.5f;
        sfxSource.Play();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
