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
    public float chanelTime; 
    public AnimationCurve fadeOutAmbientCurve;
    public AnimationCurve fadeOutUIImageCurve;
    public AudioClip InfectionSfx, infectionMusic, chargeUpSound;
    public Light ChannellingLight; 

    private TowerInfectionScript infectionScript;
    private FadeMaterial materialScript;
    private ThirdPersonCameraScript cameraScript; 

    private Image image; 
    private bool canChanel, channeling, isZoomed, isScenePlaying = false;
    private bool isLightFlickering = false; 
    private int timesChanelled = 0;
    private float startTime = 0.0f;
    private AudioSource sfxSource, chargeSource;
    private AudioSource musicSource;
    private float m_originalLightValue; 

    void Start()
    {
        cameraScript = Camera.main.GetComponent<ThirdPersonCameraScript>(); 
        image = UIImage.GetComponent<Image>();
        infectionScript = GetComponent<TowerInfectionScript>();
        materialScript = GetComponent<FadeMaterial>();
        sfxSource = gameObject.AddComponent<AudioSource>();
        chargeSource = gameObject.AddComponent<AudioSource>();
        musicSource = GameObject.FindGameObjectWithTag("MusicController").GetComponent<AudioSource>();
        m_originalLightValue = ChannellingLight.intensity; 
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
            ChannellingLight.DOIntensity(m_originalLightValue, 0.2f); 
            Debug.Log("Stopped channeling");
            channeling = false;
            isLightFlickering = false; 
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

        if (!chargeSource.isPlaying)
        {
            chargeSource.clip = chargeUpSound;
            chargeSource.Play(); 
        }

        if (!isLightFlickering && !isScenePlaying)
        {
            
            isLightFlickering = true;
            ChannellingLight.DOIntensity(3.0f, 0.2f); 
        }


        if (timeDifference > 1.5f && timeDifference < 1.7f)
        {
            Debug.Log("Channelled for x seconds");
            channeling = false;
            timesChanelled++;

            if (timesChanelled == 1)
            {
                StartCoroutine(PlayScene());

            }
        }
        else
        {

            Debug.Log("timeDifference: " + timeDifference + ", is less than 3.0f or greater than 3.2");
        }
    }

    IEnumerator PlayScene()
    {
        isScenePlaying = true; 
        Debug.Log("Playing scene"); 
        DOTween.To(() => RenderSettings.ambientLight, x => RenderSettings.ambientLight = x, Color.black, fadeAmbientTime).SetEase(fadeOutAmbientCurve);
        infectionScript.shouldPingPong = true;
        infectionScript.shouldPlayLightScene = true;

        sfxSource.clip = InfectionSfx;
        sfxSource.loop = false;
        sfxSource.Play();

        musicSource.Stop();
        musicSource.clip = infectionMusic;
        musicSource.Play();

        yield return new WaitForSeconds(10.0f);
        FadeInUIImage();
        yield return new WaitForSeconds(15.0f);

        SwitchLevel(); 
    }

    void SwitchLevel()
    {
        SceneManager.LoadScene(0);
    }

    void FadeInUIImage()
    {
        materialScript.SwitchMaterial(); 
        DOTween.To(() => image.color, x => image.color = x, Color.black, fadeUIImageTime).SetEase(fadeOutUIImageCurve);
    }

    IEnumerator AddCamOffset()
    {
        isZoomed = true; 
        Vector3 vect = Vector3.zero; 
        while(vect.y < camOffset.y)
        {
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
