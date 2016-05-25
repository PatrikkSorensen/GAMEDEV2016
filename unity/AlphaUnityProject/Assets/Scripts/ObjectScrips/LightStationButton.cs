using UnityEngine;
using System.Collections;
using DG.Tweening; 

public class LightStationButton : MonoBehaviour {
    public float m_lightIntensity = 5.0f;
    public float m_lightFadeDuration = 2.0f;
    public AudioClip clickSound;

    private GameObject B4, MiMi; 
    private Light m_lightSource; 
    private bool m_canChanel;
    private AudioSource sfxSource;

    public bool CanChanel
    {
        get { return m_canChanel; }
        set { m_canChanel = value;}
    }

    void Start()
    {
        B4 = GameObject.FindGameObjectWithTag("B4");
        MiMi = GameObject.FindGameObjectWithTag("MiMi");
        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.playOnAwake = false; 
        sfxSource.loop = false;
        sfxSource.clip = clickSound;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "B4")
        {
            sfxSource.pitch = 1.0f;
            sfxSource.Play();
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "B4" && !m_lightSource)
        {
            m_lightSource = MiMi.AddComponent<Light>();
            m_lightSource.DOIntensity(m_lightIntensity, m_lightFadeDuration);
            //fireFlies.SetActive(true); 
            CanChanel = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "B4")
        {
            //fireFlies.SetActive(false);
            Destroy(m_lightSource);
            CanChanel = false;
            sfxSource.pitch = 0.8f;
            sfxSource.Play();
        }
    }
}
