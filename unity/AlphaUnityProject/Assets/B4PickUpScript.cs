using UnityEngine;
using System.Collections;

public class B4PickUpScript : MonoBehaviour {

    public AudioClip pickUpSound;
    public float boostAmount = 20.0f;
    public float boostTime = 3.0f;

    private AudioSource sfxSource;
    private GameObject B4, MiMi;
    private PlayerController b4Con, MiMiCon;

	void Start () {
        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.loop = false;
        sfxSource.playOnAwake = false;
        sfxSource.clip = pickUpSound;

        B4 = GameObject.FindGameObjectWithTag("B4");
        MiMi = GameObject.FindGameObjectWithTag("MiMi");

        b4Con = B4.GetComponent<PlayerController>();
        MiMiCon = MiMi.GetComponent<PlayerController>();
	}

	void OnTriggerEnter(Collider other) 
	{
		if(other.gameObject.tag == "RedPickUp")
		{
            sfxSource.Play();
            if (!b4Con.isSpeedBoosted() && !MiMiCon.isSpeedBoosted())
            {
                b4Con.setSpeedBoosted(true);
                StartCoroutine(speedBoost());
            }
			other.gameObject.SetActive(false);
		}
	}

    IEnumerator speedBoost()
    {
        b4Con.setSpeed(b4Con.getUnBoostedSpeed() + boostAmount);
        MiMiCon.setSpeed(MiMiCon.getUnBoostedSpeed() + boostAmount);
        yield return new WaitForSeconds(boostTime);
        b4Con.setSpeed(b4Con.getUnBoostedSpeed());
        MiMiCon.setSpeed(MiMiCon.getUnBoostedSpeed());
        b4Con.setSpeedBoosted(false);
        MiMiCon.setSpeedBoosted(false);
    }
}