using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class lvl2_endingEvent : MonoBehaviour {
    public AudioClip endingSound, endingMusic;
    public GameObject tower;
    public Image image; 
    public GameObject elevator;
    public float speed = 1.2f; 

    private bool shouldMove; 
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
        if (Input.GetKey(KeyCode.U))
            FadeInUIImage(); 

    }

    void FixedUpdate()
    {
        if (shouldMove)
            MovePlatForm();
    }

    public IEnumerator TriggerEnding()
    {
        TriggerSounds();
        DisablePlayers();
        MakeCameraStatic();

        yield return new WaitForSeconds(1.0f);
        shouldMove = true;
        yield return new WaitForSeconds(16.0f);
        FadeInUIImage();
        yield return new WaitForSeconds(12.0f);
        SceneManager.LoadScene(0);
    }

    void FadeInUIImage()
    {
        DOTween.To(() => image.color, x => image.color = x, Color.white, 8.0f);
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
        Camera.main.transform.DOMove(B4.transform.position, 10.0f);  
        Camera.main.transform.DOLookAt(tower.transform.position, 30.0f); 
    }

    void MovePlatForm()
    {
        elevator.transform.position = Vector3.MoveTowards(elevator.transform.position, tower.transform.position, speed);
    }

    void TriggerSounds()
    {
        sfxSource.Play();
    }
}
