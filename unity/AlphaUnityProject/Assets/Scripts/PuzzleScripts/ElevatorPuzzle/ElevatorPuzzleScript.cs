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

    private GameObject B4, MiMi; 
    private List <GameObject> buttons = new List<GameObject>();
    private bool active, isMoving, shouldMove, isWaitingOnPlayer = false;
    private Animator anim;
    private Vector3 nextPosition; 

    private enum PlayerCodes
    {
        MiMi,
        B4
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

    void Start () {
        B4 = GameObject.FindGameObjectWithTag("B4");
        MiMi = GameObject.FindGameObjectWithTag("MiMi");
        nextPosition = transform.position + Vector3.up;
        anim = GetComponent<Animator>(); 
        foreach (Transform t in transform)
        {  
            buttons.Add(t.gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKey(debugKeycode))
            shouldMove = true; 
    }

	void FixedUpdate () {
        if (shouldMove)
            MovePlatForm(); 
	}

    void AssignCube(PlayerCodes playerCode)
    {
        if(playerCode == PlayerCodes.B4)
        {
          
        }
        else
        {

        }

        buttons[7].transform.GetChild(0).GetComponent<Renderer>().material.DOColor(B4Color, bootTime); 
    }

    void StartElevatorScene()
    {

    }

    void MovePlatForm()
    {
        float distance = Vector3.Distance(transform.position, nextPosition);
        if (distance > 0.2f)
        {
            Vector3 TranslateVec = Vector3.up * elevatorForce * Time.deltaTime;
            Debug.Log("newPosition: " + TranslateVec);
            transform.Translate(TranslateVec);
        }
        else
        {
            nextPosition = transform.position + Vector3.up;
            shouldMove = false;
        }
    }
}
