using UnityEngine;
using System.Collections;
using DG.Tweening;
using System.Collections.Generic;

public class ElevatorPuzzleScript : MonoBehaviour {

    public GameObject elevator;
    public float elevatorForce;
    public float timeBeforeStopping;
    public float bootTime = 1.0f; 
    public Color MiMiColor;
    public Color B4Color;
    public KeyCode debugKeycode;

    private enum PlayerCodes {
        MiMi, 
        B4       
    }
    private GameObject B4, MiMi; 
    private List <GameObject> buttons = new List<GameObject>(); 

	// Use this for initialization
	void Start () {
        B4 = GameObject.FindGameObjectWithTag("B4");
        MiMi = GameObject.FindGameObjectWithTag("MiMi");

        foreach (Transform t in transform)
        { // If in chronologically order in hierachy, the mapping should be correct
           
            buttons.Add(t.gameObject);
        }
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(debugKeycode))
        {
            float newPosition = elevator.transform.position.y + elevatorForce;
            Debug.Log("newPosition: " + newPosition); 
            //elevator.transform.DOMoveY(newPosition, timeBeforeStopping);

            AssignCube(PlayerCodes.B4); 
        }
	}

    void AssignCube(PlayerCodes playerCode)
    {
        if(playerCode == PlayerCodes.B4)
        {
            // Do stuff 
        }
        else
        {

        }

        buttons[7].transform.GetChild(0).GetComponent<Renderer>().material.DOColor(B4Color, bootTime); 
    }

    /**
     * 
     *    MiMi = ¤ , B4 = # 

            A | B | C_
           ____________
        1 |_0_|_1_|_2_|   
        2 |_3_|_4_|_5_| 
        3 |_6_|_7_|_8_|  
     * 
     * 
     */
}
