using UnityEngine;
using System.Collections;

public class BeaconLightScript : MonoBehaviour {
    public float delayTransistion = 4.0f; 
    public GameObject switch1, switch2, storyWall, sphereVFX;
    public GameObject[] enemies; 
    public Light lightProjector, sphereLight;

    [HideInInspector]
    public bool isActive = false; 
    private HexagonSwitchScript switchScript1, switchScript2;

    private bool isScenePlaying, isSceneFinished = false;
	
	void Start () {
        switchScript1 = switch1.GetComponent<HexagonSwitchScript>();
        switchScript2 = switch2.GetComponent<HexagonSwitchScript>();
        sphereLight.intensity = 0.0f; 

    }
    void Update()
    {
        if (switchScript1.getStatus() && switchScript1.getStatus() && !isScenePlaying && !isSceneFinished)
        {
            triggerScene();
        }

        if(isSceneFinished && !isActive)
        {
            Debug.Log("GameObject is active and scene is finished");
            isActive = true;
            //TODO: 
            // disable switches
            // disable this script    
         
        }
    }

    void triggerScene() {
        isScenePlaying = true; 

        // Trigger sounds

        // Trigger animations 

        // Alert other scripts and handle internal variables
        Debug.Log("Triggering event on: " + gameObject.name);

        // Change materials on model: 
        

        // Project light and fadein storywall
        StartCoroutine(FadeInWallStory()); 
    }

    IEnumerator FadeInWallStory()
    {
        Renderer rend = GetComponent<Renderer>();
        Material sphereMaterial = rend.materials[3];
        Color startColor = sphereMaterial.GetColor("_EmissionColor");
        Color endColor = Color.HSVToRGB(0, 0, 0.5f);

        // TODO: add lens flare
        // Light/power up sphere
        for (float f = 0; f <= 1; f += 0.005f)
        {
            sphereMaterial.SetColor("_EmissionColor", Color.HSVToRGB(0, 0, f));
            sphereLight.intensity += 0.025f;
            yield return null;
        }

        for (float f = 0; f <= 1; f += 0.005f)
        {
            if (!storyWall)
                break;
            Color c = storyWall.GetComponent<Renderer>().material.color;
            c.a = f;
            storyWall.GetComponent<Renderer>().material.color = c;
            
            yield return null;
        }

        sphereMaterial.SetColor("_EmissionColor", Color.white);
        yield return new WaitForSeconds(delayTransistion);

        // Fade out wall
        for (float f = 1; f >= 0; f -= 0.01f)
        {
            if (!storyWall)
                break;

            Color c = storyWall.GetComponent<Renderer>().material.color;
            c.a = f;
            storyWall.GetComponent<Renderer>().material.color = c;
            yield return null;
        }

        isScenePlaying = false; 
        isSceneFinished = true;

        if (enemies.Length != 0)
        {
            ActivateEnemies(); 
        }


    }

    public void ActivateEnemies()
    {
        foreach(GameObject enemy in enemies)
        {
            enemy.GetComponent<EnemyNavmeshScript>().SetTarget(GameObject.FindGameObjectWithTag("B4")); 
        }
    } 
}
