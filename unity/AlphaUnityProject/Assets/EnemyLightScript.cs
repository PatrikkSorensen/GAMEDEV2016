using UnityEngine;
using System.Collections;

public class EnemyLightScript : MonoBehaviour {

    public float minConeSize = 40.0f; 
    public float  maxConeSize = 50.0f; 
    
    private GameObject MiMi, B4; 
    private Light lightSource;
    private SphereCollider triggerZone; 

    void Start()
    {
        MiMi = GameObject.FindGameObjectWithTag("MiMi");
        B4 = GameObject.FindGameObjectWithTag("B4");
        triggerZone = GetComponent<SphereCollider>();
        lightSource = GetComponent<Light>(); 
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            StartCoroutine(FadeOutLight());
        }

        if (Input.GetKey(KeyCode.H))
        {
            StartCoroutine(FadeInLight()); 
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.attachedRigidbody) // TODO: Access this on playerScript
            other.attachedRigidbody.AddForce(Vector3.up * -4.0f); // TODO: Make this progressional and slow rotation


    }

    IEnumerator FadeOutLight()
    {
        while(lightSource.spotAngle > minConeSize)
        {
            lightSource.spotAngle += -0.01f;
            triggerZone.radius += -0.001f;
            yield return null;
        }

        // TODO: Make animation curve

        // TODO: Make transform for caught player, e.g DOTWeen 

        // TODO: Colors. 
    }

    IEnumerator FadeInLight()
    {
        Debug.Log("Fading in light which has spotAngle: " + lightSource.spotAngle + ", maxConsize: " + maxConeSize);
        while (lightSource.spotAngle < maxConeSize)
        {
            Debug.Log("Fading in...");
            lightSource.spotAngle += 0.01f;
            triggerZone.radius += 0.001f;
            yield return null;
        }
    }



}
