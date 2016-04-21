using UnityEngine;
using System.Collections;
using DG.Tweening;

public class IntroSceneEvent : MonoBehaviour {

    public AudioClip clip; 

    private GameObject B4, MiMi;
    private AudioSource audioSource;
    private AudioSource musicSource;

    private CameraController cameraController; 
    void Start()
    {
        B4 = GameObject.FindGameObjectWithTag("B4");
        MiMi = GameObject.FindGameObjectWithTag("MiMi");
        audioSource = GameObject.FindGameObjectWithTag("AudioController").GetComponent<AudioSource>();
        musicSource = GameObject.FindGameObjectWithTag("MusicController").GetComponent<AudioSource>();
        cameraController = Camera.main.GetComponent<CameraController>();
        cameraController.changeCameraType(CameraController.CameraTypes.SINGLE_PERSON_CAMERA);
        MiMi.GetComponent<PlayerController>().enabled = false;

        //TODO: Add this on MiMiPrefab
        //MiMi.GetComponent<PullOfLove>().enabled = false;
        B4.GetComponent<PlayerController>().enabled = false;
        B4.GetComponent<PullOfLove>().enabled = false;
        B4.GetComponent<EstablishBond>().enabled = false;

        Vector3 targPos = B4.transform.position + new Vector3(0.0f, 2.0f, -2.0f);
        StartCoroutine(MoveCameraToPosition(targPos, 5.0f));

        StartCoroutine(MusicFadeIn(0.15f ,1.0f, 20.0f));
    }

    IEnumerator MusicFadeIn(float from, float to, float duration)
    {

        float midVol = from + (to * 0.30f);
        float midDuration = duration * 0.5f;

        musicSource.volume = from;
        musicSource.Play();
        musicSource.DOFade(midVol, midDuration);
        yield return new WaitForSeconds(midDuration);
        musicSource.DOFade(to, duration-midDuration);
    }

    IEnumerator MoveCameraToPosition(Vector3 newPosition, float time)
    {
        //TODO: Clean up this script
        Camera.main.GetComponent<CameraFollow>().enabled = false;

        float elapsedTime = 0;
        Vector3 startingPos = Camera.main.transform.position;
        while (elapsedTime < time)
        {
            Vector3 nextPosition = Vector3.Lerp(startingPos, newPosition, (elapsedTime / time));
            Camera.main.transform.position = nextPosition;
            elapsedTime += Time.deltaTime;
            yield return null; 
        }

        yield return new WaitForSeconds(2.0f);

        Camera.main.GetComponent<CameraFollow>().enabled = true;
        Camera.main.GetComponent<CameraFollow>().smooth = 0.5f;
        audioSource.PlayOneShot(clip, 0.7F);
        yield return new WaitForSeconds(3.0f);

        B4.GetComponent<PlayerController>().enabled = true;
        Camera.main.GetComponent<CameraFollow>().smooth = 4.0f;


    }
}
