using UnityEngine;
using System.Collections;

public class BondedEnemyAI : MonoBehaviour {

    public float delay = 10.0f; 
    public Vector3 startDestination;
    public GameObject endDestination; 
    private LineRenderer bond;
    private GameObject enemy1, enemy2;

    void Start()
    {
        enemy1 = transform.GetChild(0).gameObject;
        enemy2 = transform.GetChild(1).gameObject;

        bond = gameObject.AddComponent<LineRenderer>();
        bond.SetPosition(0, enemy1.transform.position);
        bond.SetPosition(1, enemy2.transform.position);
        bond.SetWidth(0.1f, 0.1f);
        startDestination = transform.position; 
        //GetComponent<NavMeshAgent>().destination = Vector3.up; 
    }

    void Update()
    {
        bond.SetPosition(0, enemy1.transform.position);
        bond.SetPosition(1, enemy2.transform.position);
    }

    public IEnumerator TriggerScene()
    {
        Debug.Log("Changing destination for: " + gameObject.name);
        GetComponent<NavMeshAgent>().destination = endDestination.transform.position;
        GetComponent<NavMeshAgent>().speed = 8.0f;

        yield return new WaitForSeconds(delay);

        gameObject.GetComponent<NavMeshAgent>().destination = startDestination;


    }
}
