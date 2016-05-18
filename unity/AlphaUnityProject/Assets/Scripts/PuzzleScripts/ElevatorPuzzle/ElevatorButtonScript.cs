using UnityEngine;
using System.Collections;
using DG.Tweening;

public class ElevatorButtonScript : MonoBehaviour {

    private Color defaultColor; 

    private GameObject elevatorFrame, elevatorPlatform; 
    private bool isActive, isFadingIn, isFadingOut, hasFaded;
    private string playerToActivate;


	void Start () {
        elevatorFrame = transform.GetChild(0).gameObject;
        elevatorPlatform = transform.GetChild(1).gameObject;
         
        defaultColor = elevatorFrame.GetComponent<Renderer>().material.color; 
    }

    public void ActivateButton(string player, Color playerColor)
    {
        playerToActivate = player; 
        elevatorFrame.GetComponent<Renderer>().material.DOColor(playerColor, 2.0f);
        isActive = true; 
    }

    void OnTriggerStay(Collider coll)
    {
        if (coll.gameObject.tag == playerToActivate)
        {
            isActive = false;
            elevatorFrame.GetComponent<Renderer>().material.DOColor(defaultColor, 2.0f);
        }
    }

    public bool IsActive()
    {
        return isActive; 
    }

}
