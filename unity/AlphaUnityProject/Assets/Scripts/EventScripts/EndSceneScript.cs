using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class EndSceneScript : MonoBehaviour {

    public GameObject UIImage;

    [SerializeField]
    public float fadeAmbientTime;

    [SerializeField]
    public float fadeUIImageTime;

    [SerializeField]
    public AnimationCurve fadeOutCurve; 

    private Image image; 
    private bool canChanel, channeling = false; 
    private int timesChanelled = 0;
    private float startTime = 0.0f; 

    void Start()
    {
        image = UIImage.GetComponent<Image>(); 
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
        {
            ChanelEnergy();
        }
    }
    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "MiMi")
        {
            canChanel = true; 

        }
    }

    void OnTriggerExit()
    {
        canChanel = false; 
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

            if (timesChanelled == 1)
                PlayScene();

            if (timesChanelled == 2)
            {
                FadeInUIImage(); 
            }
                

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
        DOTween.To(() => RenderSettings.ambientLight, x => RenderSettings.ambientLight = x, Color.black, fadeAmbientTime).SetEase(fadeOutCurve);
    }

    void SwitchLevel()
    {
        // Application.load(); 
        SceneManager.LoadScene(0);
    }

    void FadeInUIImage()
    {
        DOTween.To(() => image.color, x => image.color = x, Color.black, fadeUIImageTime).SetEase(fadeOutCurve);
    }


}
