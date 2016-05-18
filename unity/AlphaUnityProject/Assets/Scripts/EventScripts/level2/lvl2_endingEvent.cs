using UnityEngine;
using System.Collections;
using DG.Tweening; 
public class lvl2_endingEvent : MonoBehaviour {
    public AudioClip endingSound, endingMusic;
    public GameObject tower;
    public GameObject elevator; 

    private AudioSource sfxSource;
    private AudioSource musicSource;
    private GameObject B4, MiMi; 

	void Start () {
        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.clip = endingSound;

        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.clip = endingMusic;

        B4 = GameObject.FindGameObjectWithTag("B4");
        MiMi = GameObject.FindGameObjectWithTag("MiMi");
	}

	void Update () {
        if (Input.GetKey(KeyCode.X))
            StartCoroutine(TriggerEnding());
	}

    public IEnumerator TriggerEnding()
    {
        TriggerSounds();
        DisablePlayers();
        MakeCameraStatic();
        MovePlatformToTower(); 

        yield return null; 
    }

    void DisablePlayers()
    {
        B4.GetComponent<PlayerController>().enabled = false;
        MiMi.GetComponent<PlayerController>().enabled = false; 
    }

    void MakeCameraStatic()
    {
        //CameraFollow camFollow = Camera.main.GetComponent<CameraFollow>(); 
        //Camera.main.GetComponent<CameraController>().changeCameraType(CameraController.CameraTypes.SINGLE_PERSON_CAMERA); 
        Camera.main.transform.DOLookAt(tower.transform.position, 10.0f); 
    }

    void MovePlatformToTower()
    {
        elevator.transform.DOMove(tower.transform.position, 30.0f); 
    }

    void TriggerSounds()
    {
        sfxSource.Play();
    }
}
