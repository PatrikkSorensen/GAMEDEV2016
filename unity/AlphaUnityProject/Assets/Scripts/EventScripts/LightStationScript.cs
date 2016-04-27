using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class LightStationScript : MonoBehaviour {

    public ParticleSystem sparkParticles, haloParticles;
    public Color materialEndColor, emissionColor; 
    public float chanelTime, materialFadeInTime = 3.0f;
    public List<GameObject> AIHelpers = new List<GameObject>();
    public GameObject curcuitLines, powerSphere, meshLine; 

    private bool canChanel, channeling, isActive, b = false;
    private PlayerController B4Controller, MiMiController;
    private float startTime = 0.0f;



    public bool IsActive
    {
        get { return isActive; }
        set { isActive = value;}
    }


    void Start()
    {
        B4Controller = GameObject.FindGameObjectWithTag("B4").GetComponent<PlayerController>();
        MiMiController = GameObject.FindGameObjectWithTag("MiMi").GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.V))
        {
            ChangeSphereMaterial(); 

        }
        // TODO: Refactor this
        if (canChanel && !isActive)
        {
            if (Input.GetButtonDown("Channelling") && canChanel && B4Controller.isBonded())
            {
                startTime = Time.time;
                channeling = true;
                sparkParticles.Play();
            }

            if (Input.GetButtonUp("Channelling"))
            {
                float timeDifference = Time.time - startTime;
                channeling = false;
                startTime = 0;
                sparkParticles.Stop();
            }

            if (channeling)
            {
                ChanelEnergyOnPlatform();
            }
        }
    }

    void OnTriggerStay(Collider other)
    {

            canChanel = true;

    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "MiMi")
        {
            canChanel = false;
        }
    }

    void ChanelEnergyOnPlatform()
    {
        float timeDifference = Time.time - startTime;

        if (timeDifference > chanelTime && timeDifference < chanelTime + 0.2f && channeling)
        {
            Debug.Log("Channelled for three seconds");
            sparkParticles.Play();
            haloParticles.Play();
            canChanel = false;
            channeling = false;
            IsActive = true;


            // Activate events
            ActivateAgents();
            ActivateLines();

            // Materials and visuals
           ChangeSphereMaterial();

        }
        else
        {
            Debug.Log("timeDifference: " + timeDifference + ", is less than 3.0f");
        }
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
        if (curcuitLines.GetComponent<CurcuitLines>())
            curcuitLines.GetComponent<CurcuitLines>().ChanelLines();
        else
            Debug.LogWarning("There are no curcuitlines script to the gameobject specified");
    }

}
