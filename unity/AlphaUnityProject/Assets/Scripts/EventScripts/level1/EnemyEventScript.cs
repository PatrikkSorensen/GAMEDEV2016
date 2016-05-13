using UnityEngine;
using System.Collections;
using DG.Tweening;

public class EnemyEventScript : MonoBehaviour {
    //TODO: Refactor to list, for better insertion and removal
    //TODO: Optimize EnemyEventScript, EnemyNavmeshScript and PlayerController to be more readable
    GameObject[] enemies, sceneEnemies;
    GameObject b4, MiMi;
    [HideInInspector]
    public bool chasing; 

    void Start()
    {
        b4 = GameObject.FindGameObjectWithTag("B4");
        MiMi = GameObject.FindGameObjectWithTag("MiMi"); 

        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        sceneEnemies = GameObject.FindGameObjectsWithTag("SceneEnemy"); 
    }

    void FixedUpdate()
    {
        if (chasing)
            UpdateChasePosition(); 

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "B4" ||other.tag == "MiMi")
            TriggerEnemyEvent();

    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "B4")
        {
            Debug.Log("B4 exitted");
            StartCoroutine(TriggerExitScene());
        }
    }

    void UpdateChasePosition()
    {
        for(int i = 0; i<enemies.Length; i++)
        {
            //remove from enemies[]
            if (enemies[i] == null || !enemies[i].GetComponent<NavMeshAgent>().enabled)
                return;
               
            if (enemies[i].GetComponent<NavMeshAgent>() && b4)
            {
                enemies[i].GetComponent<EnemyNavmeshScript>().SetTarget(b4); 
            }
        }
    }

    void TriggerEnemyEvent()
    {
        Debug.Log("Event is firing");

        // Play sound 

        // Adjust light

        // Play animations from enemies
        chasing = true; 
    }

    IEnumerator TriggerExitScene()
    {
        Debug.Log("Triggering exit scene!");
        b4.GetComponent<PlayerController>().enabled = false;
        //TODO: Remove all character controls and mechanics through the playerController script
        MiMi.GetComponent<PlayerController>().enabled = false;
        Debug.Log("Triggering exit scene" + sceneEnemies.Length);
        foreach(GameObject sceneEnemy in sceneEnemies)
        {
            Debug.Log("SceneEnemy: " + sceneEnemy.name);
            StartCoroutine(sceneEnemy.GetComponent<BondedEnemyAI>().TriggerScene());
             
        }

        // The delay for sceneEnemies is by default 8 seconds
        yield return new WaitForSeconds(6.0f);

        //TODO: Make Camera shake a static function, ressource: http://unitytipsandtricks.blogspot.dk/2013/05/camera-shake.html
        Camera.main.GetComponent<CameraShake>().Shake();
        RenderSettings.ambientLight = Color.black;
        GameObject alphaCube = GameObject.Find("AlphaSplash");
        Light directionalLight = GameObject.Find("Directional light").GetComponent<Light>();
        directionalLight.intensity = 0.0f;
        RenderSettings.ambientIntensity = 0.0f;
        yield return new WaitForSeconds(2.0f);

        // Splash wall
        bool b = false; 
        

        for (float f = 0; f<100; f += 0.05f)
        {
            Camera.main.GetComponent<ThirdPersonCameraScript>().distanceAway += f;
            Camera.main.GetComponent<ThirdPersonCameraScript>().distanceUp += f;


            yield return new WaitForSeconds(0.1f);
            if(!b)
                directionalLight.intensity += f * 0.01f;

            if (f > 2.0f)
            {
                Color c = alphaCube.GetComponent<Renderer>().material.color;
                c.a = f * 0.1f;
                alphaCube.GetComponent<Renderer>().material.color = c;
            }

        }


        
        



    }
}
