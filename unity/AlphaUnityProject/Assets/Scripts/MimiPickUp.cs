using UnityEngine;
using System.Collections;

public class MimiPickUp : MonoBehaviour {

    public AudioClip pickUpSound;

    private AudioSource sfxSource;

	void Start () {
        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.loop = false;
        sfxSource.playOnAwake = false;
        sfxSource.clip = pickUpSound;
	
	}
	
	void OnTriggerEnter(Collider other) 
	{
		if(other.gameObject.tag == "BluePickUp")
		{
            sfxSource.Play();
			other.gameObject.SetActive(false);
		}
	}
}
