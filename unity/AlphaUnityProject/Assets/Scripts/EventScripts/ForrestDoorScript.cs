using UnityEngine;
using System.Collections;

public class ForrestDoorScript : MonoBehaviour {

    public GameObject character; 

    void Start()
    {
        Physics.IgnoreCollision(character.GetComponent<Collider>(), GetComponent<Collider>());
    }
}
