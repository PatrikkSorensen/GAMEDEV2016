using UnityEngine;
using System.Collections;

public class EnemyNavmeshScript : MonoBehaviour {

    //TODO: Find optimal speed
    public float speed = 10.0f;
    public float recoveringTime = 3.0f; 

    private NavMeshAgent agent;
    private GameObject target; 
    private GameObject destination; 

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
    }
    void Update()
    {
        if (target && gameObject.GetComponent<NavMeshAgent>().enabled)
            MoveToTarget(target.transform.position);
    }
    public void SetTarget(GameObject chaseTarget)
    {
        target = chaseTarget; 
    }

    public void MoveToTarget(Vector3 destination)
    {
        agent.destination = destination; 
    }

    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Collided with: " + collision.gameObject.name);
        if(collision.gameObject.tag == "B4")
        {
            AttachStickySphere(collision.gameObject); 
        }
    }

    void AttachStickySphere(GameObject target)
    {
        // Adjust sphere and event behavioural components
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<SphereCollider>().enabled = false; 
        
        // Child gameObject and collision
        transform.position = target.transform.position + new Vector3(0.0f, 0.5f, 0.0f) + new Vector3(0.0f, 0.0f, 0.2f);
        transform.parent = target.transform;

        // Change material 
        GetComponent<Renderer>().material.color = Color.black;

        // SFX 

        // Edit player speed, and set status 
        target.GetComponent<PlayerController>().speed += -2.0f;
        target.GetComponent<PlayerController>().enemies.Add(gameObject);

        // Adjust ambient light  

        // Set internal variables
    }

    // Note: Triggered from PlayerController
    public IEnumerator DetachFromPlayer()
    {
        yield return new WaitForSeconds(recoveringTime);
        //Debug.Log(gameObject.name + " is waiting for assignment.");

        GetComponent<NavMeshAgent>().enabled = true;
        GetComponent<SphereCollider>().enabled = true;
    }
}
