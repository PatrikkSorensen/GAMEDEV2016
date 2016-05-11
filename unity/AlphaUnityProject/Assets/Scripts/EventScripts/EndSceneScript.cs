using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class EndSceneScript : MonoBehaviour {

    public GameObject UIImage;
    public Vector3 camOffset; 
    public float fadeAmbientTime;
    public float fadeUIImageTime;
    public AnimationCurve fadeOutAmbientCurve;
    public AnimationCurve fadeOutUIImageCurve;
    public AudioClip InfectionSfx;

    private TowerInfectionScript infectionScript;
    private FadeMaterial materialScript;
    private ThirdPersonCameraScript cameraScript; 

    private Image image; 
    private bool canChanel, channeling, isZoomed = false; 
    private int timesChanelled = 0;
    private float startTime = 0.0f; 

    void Start()
    {
        cameraScript = Camera.main.GetComponent<ThirdPersonCameraScript>(); 
        image = UIImage.GetComponent<Image>();
        infectionScript = GetComponent<TowerInfectionScript>();
        materialScript = GetComponent<FadeMaterial>(); 
    }

    void Update()
    {
        CheckInputs();
    }

    void CheckInputs()
    {
        if (Input.GetButtonDown("Channelling") && canChanel)
        {
            startTime = Time.time;
            channeling = true;
        }

        if (Input.GetButtonUp("Channelling") && canChanel)
        {
            Debug.Log("Stopped channeling");
            channeling = false;
        }


        if (channeling)
            ChanelEnergy();

    }
    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "MiMi")
        {
            if (!isZoomed)
            {
                StartCoroutine(AddCamOffset());
            }

            canChanel = true;
        }
        
    }

    void OnTriggerExit(Collider other)
    {
        canChanel = false;
        if (other.gameObject.tag == "MiMi")
        {
            if (isZoomed)
            {
                //StartCoroutine(RemoveCamOffset()); // TODO: Fix multiple coroutines, preferbly with stopCourtine
            }
        }

    }

    void ChanelEnergy()
    {
        Debug.Log("Trying to chanel energy");
        float timeDifference = Time.time - startTime;

        if (timeDifference > 3.0f && timeDifference < 3.2f)
        {
            Debug.Log("Channelled for three seconds");
            channeling = false;
            timesChanelled++;

            if (timesChanelled == 1) { }
                PlayScene();

            if (timesChanelled == 2)
                FadeInUIImage(); 
                

            if (timesChanelled == 3)
                SwitchLevel(); 
        }
        else
        {
            Debug.Log("timeDifference: " + timeDifference + ", is less than 3.0f or greater than 3.2");
        }
    }

    void PlayScene()
    {
        DOTween.To(() => RenderSettings.ambientLight, x => RenderSettings.ambientLight = x, Color.black, fadeAmbientTime).SetEase(fadeOutAmbientCurve);
        infectionScript.shouldPingPong = true;
        AudioSource sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.clip = InfectionSfx;
        sfxSource.loop = false;
        sfxSource.Play();
    }

    void SwitchLevel()
    {
        SceneManager.LoadScene(0);
    }

    void FadeInUIImage()
    {
        infectionScript.shouldPlayLightScene = true;
        materialScript.SwitchMaterial(); 
        DOTween.To(() => image.color, x => image.color = x, Color.black, fadeUIImageTime).SetEase(fadeOutUIImageCurve);
    }

    IEnumerator AddCamOffset()
    {
        isZoomed = true; 
        Vector3 vect = Vector3.zero; 
        while(vect.y < camOffset.y)
        {
            Debug.Log("Hello");
            vect.y += 0.02f;
            cameraScript.offset = vect;
            yield return new WaitForSeconds(0.001f);
        }
        
        yield return null; 
    }

    IEnumerator RemoveCamOffset()
    {
        isZoomed = false;
        Vector3 vect = Vector3.zero;
        while (vect.y > camOffset.y)
        {
            Debug.Log("Hello");
            vect.y -= 0.02f;
            cameraScript.offset = vect;
            yield return new WaitForSeconds(0.001f);
        }
        yield return null;
    }
}
