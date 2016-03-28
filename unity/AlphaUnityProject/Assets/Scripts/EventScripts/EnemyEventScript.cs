using UnityEngine;
using System.Collections;

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
            TriggerExitScene(); 

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

    void TriggerExitScene()
    {
        b4.GetComponent<PlayerController>().enabled = false;
        //TODO: Remove all character controls and mechanics through the playerController script
        MiMi.GetComponent<PlayerController>().enabled = false;
        Debug.Log("Triggering exit scene" + sceneEnemies.Length);
        foreach(GameObject sceneEnemy in sceneEnemies)
        {
            Debug.Log("SceneEnemy: " + sceneEnemy.name);
            StartCoroutine(sceneEnemy.GetComponent<BondedEnemyAI>().TriggerScene());
             
        }
    }
}
