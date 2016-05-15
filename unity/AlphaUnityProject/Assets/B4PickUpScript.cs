using UnityEngine;
using System.Collections;

public class B4PickUpScript : MonoBehaviour {

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
		if(other.gameObject.tag == "RedPickUp")
		{
            sfxSource.Play();
			other.gameObject.SetActive(false);
		}
	}
}