using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Agent : MonoBehaviour {

    protected AudioSource audioSource;
    protected Animator anim;
    protected Vector3 dest; 
    protected NavMeshAgent navAgent;
    protected bool isActive, isBusy = false;

    // LEGACY, MIGHT BE REMOVED
    [HideInInspector]
    public List<GameObject> enemies = new List<GameObject>();

    public void DetachEnemies()
    {
        Debug.Log("Detaching enemies");
        foreach (GameObject enemy in enemies)
        {
            Debug.Log("Detaching enemy: " + enemy);
            enemy.transform.parent = null;
            //float speed += 2.0f;
            //enemy.GetComponent<NavMeshAgent>().destination = Vector3.zero;
            StartCoroutine(enemy.GetComponent<EnemyNavmeshScript>().DetachFromPlayer());

        }

        enemies.Clear();
    }

    public bool IsActive
    {
        get { return isActive; }
        set { isActive = value; }
    }

    public bool IsBusy
    {
        get { return isBusy; }
        set
        {
            Debug.Log("Setting isBusy to: " + value);
            isBusy = value;
        }
    }

    void Update()
    {
        // TODO: Implement behaviour or FSM 
        // Ressources: http://www.gamasutra.com/blogs/ChrisSimpson/20140717/221339/Behavior_trees_for_AI_How_they_work.php

        // Check current node or status 

        // Evaluate world 

        // Perform action 

        // Repeat

        // Hardcoded for now: 


    }
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        navAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        IsBusy = false; 
    }



    public virtual void ActivateAgent()
    {
        if (IsActive)
            return;

        Debug.Log(gameObject.name + " is activating.");
        IsActive = true; 
    }

    public virtual void DisableAgent()
    {
        if (!IsActive)
            return; 

        Debug.Log(gameObject.name + " is deactivating.");
        IsActive = false; 
    }

    public virtual void MoveAgent(Vector3 destination)
    {
        dest = destination;  
        Debug.Log("MoveAgent behaviour triggered with dest: " + dest);

        navAgent.destination = dest;
        IsBusy = true; 
    }
}
