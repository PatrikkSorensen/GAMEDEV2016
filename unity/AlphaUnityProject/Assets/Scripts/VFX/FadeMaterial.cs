using UnityEngine;
using System.Collections;
using DG.Tweening; 

public class FadeMaterial : MonoBehaviour {
    public GameObject m_gameObject, materialGameObject;
    public Color materialEndColor, emissionColor;
    public float duration = 2.0f; 

    private Renderer rend;
    private Material infectedMaterial; 
	// Use this for initialization
	void Start () {
        rend = m_gameObject.GetComponent<Renderer>();
        infectedMaterial = materialGameObject.GetComponent<Renderer>().material;
        Destroy(materialGameObject); 
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.H))
            SwitchMaterial();
    }
	
    void PlayFadeMaterial()
    {
        Material sphereMaterial = rend.materials[1]; 
        sphereMaterial.DOColor(materialEndColor, duration);

        Color startEColor = sphereMaterial.GetColor("_EmissionColor");

        Debug.Log("Playing fade material on mat: " + sphereMaterial.name);
        Debug.Log("E color: " + startEColor);
        StartCoroutine(FadeInEmmision(sphereMaterial, startEColor, emissionColor));
    }

    IEnumerator FadeInEmmision(Material m, Color startColor, Color endColor)
    {
        float incrementor = 0.01f;
        for (float f = startColor.grayscale; f < endColor.grayscale; f += incrementor)
        {
            startColor = startColor + new Color(incrementor, incrementor, incrementor);
            m.SetColor("_EmissionColor", startColor);
            yield return new WaitForSeconds(0.01f);

        }

    }

    public void SwitchMaterial()
    {
        Material[] newMaterials = rend.materials;
        
        newMaterials[1] = infectedMaterial;
        newMaterials[1].DOTiling(new Vector2(0.5f, 0.5f), 2.0f).SetLoops(-1, LoopType.Incremental);
        rend.materials = newMaterials;
    }
}
