using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class LightStationScript : MonoBehaviour {

    public ParticleSystem sparkParticles, haloParticles;
    public Color materialEndColor, emissionColor; 
    public float chanelTime, materialFadeInTime = 3.0f;
    public List<GameObject> AIHelpers = new List<GameObject>();
    public GameObject powerSphere, meshLine, doorTrigger;
    public CurcuitChanneller curcuitLines; 
    public KeyCode DebugKey; 
    public AudioClip channellingClip, sucessClip;
    public LightStationButton B4Button;

    private GameObject B4, MiMi;
    private Animator B4Animator, MiMiAnimator; 
    private AudioSource chanelSource, sucessSource; 
    private bool canChanel, channelling, isActive = false;
    private PlayerStatusScript B4Status, MiMiStatus;
    private float startTime = 0.0f;
    private GameObject m_audioObject;

    private Animator anim; 

    public bool IsActive
    {
        get { return isActive; }
        set { isActive = value;}
    }

    void Start()
    {
        AssignAudioObject();
        B4 = GameObject.FindGameObjectWithTag("B4");
        MiMi = GameObject.FindGameObjectWithTag("MiMi");

        B4Animator = B4.GetComponent<Animator>();
        MiMiAnimator = MiMi.GetComponent<Animator>(); 

        B4Status = B4.GetComponent<PlayerStatusScript>();
        MiMiStatus = MiMi.GetComponent<PlayerStatusScript>();
        anim = GetComponent<Animator>(); 
    }

    void Update()
    {
        if (Input.GetKey(DebugKey) && !isActive)
            ActivateLightStation(); 

        if (canChanel && !isActive)
        {
            if (B4Button)
                if (!B4Button.CanChanel)
                    return; 

            if (Input.GetButtonDown("Channelling") && canChanel && B4Status.getBondStatus())
            {
                startTime = Time.time;
                channelling = true;
                sparkParticles.Play();
                B4Animator.SetBool("channelling", true);
                MiMiAnimator.SetBool("channelling", true); 
            }

            if (Input.GetButtonUp("Channelling"))
            {
                channelling = false;
                B4Animator.SetBool("channelling", false);
                MiMiAnimator.SetBool("channelling", false); 
                startTime = 0;
                sparkParticles.Stop();

                if (chanelSource.isPlaying)
                    chanelSource.Stop();
            }

            if (channelling)
            {
                if(!chanelSource.isPlaying)
                    chanelSource.Play();
                 
                ChanelEnergyOnPlatform();
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "MiMi")
            canChanel = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "MiMi")
            canChanel = false;
    }

    void ChanelEnergyOnPlatform()
    {
        float timeDifference = Time.time - startTime;

        if (timeDifference > chanelTime && timeDifference < chanelTime + 0.2f)
        {
            ActivateLightStation();
        }
        else
        {
            Debug.Log("timeDifference: " + timeDifference + ", is less than 3.0f");
        }
    }

    void ActivateLightStation()
    {
        Debug.Log("Channelled for three seconds");
        if (doorTrigger != null)
            doorTrigger.GetComponent<DoorSocketScript>().incrementEnergy();


        sparkParticles.Stop();
        haloParticles.Play();
        canChanel = false;
        channelling = false;
        IsActive = true;

        // Sounds and animation 
        sucessSource.Play();
        anim.SetBool("active", true);

        // Activate events
        ActivateAgents();
        ActivateLines();

        // Materials and visuals
        ChangeSphereMaterial();

        // Animation 
        B4Animator.SetBool("channelling", false);
        MiMiAnimator.SetBool("channelling", false); 
    }

    void ChangeSphereMaterial()
    {
        Material sphereMaterial = powerSphere.GetComponent<MeshRenderer>().material;
        Material lineMaterial = meshLine.GetComponent<MeshRenderer>().materials[1];

        sphereMaterial.DOColor(materialEndColor, materialFadeInTime);

        Color startEColor = sphereMaterial.GetColor("_EmissionColor");

        Debug.Log("E color: " + startEColor);
        StartCoroutine(FadeInEmmision(sphereMaterial, startEColor, emissionColor));
    }

    IEnumerator FadeInEmmision(Material m, Color startColor, Color endColor)
    {
        float incrementor = 0.01f; 
        for(float f = startColor.grayscale; f < endColor.grayscale; f += incrementor)
        {
            startColor = startColor + new Color(incrementor, incrementor, incrementor);
            m.SetColor("_EmissionColor", startColor);
            yield return new WaitForSeconds(0.01f); 
        }

    }

    void ActivateAgents()
    {
        foreach (GameObject gb in AIHelpers)
        {
            Agent agent = gb.GetComponent<Agent>();
            agent.ActivateAgent(); 
        }
    }

    void ActivateLines()
    {
        if (curcuitLines)
            StartCoroutine(curcuitLines.BeginChanneling());
        else
            Debug.LogWarning("There are no curcuitlines script to the gameobject specified");
    }

    void AssignAudioObject()
    {
        m_audioObject = new GameObject();
        m_audioObject.name = "lightstation_AudioObject";
        m_audioObject.transform.parent = transform;

        chanelSource = m_audioObject.AddComponent<AudioSource>();
        chanelSource.clip = channellingClip; 

        sucessSource = m_audioObject.AddComponent<AudioSource>();
        sucessSource.clip = sucessClip; 
    }
}
