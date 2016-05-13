using UnityEngine;
using System.Collections;
using DG.Tweening;

public class SlopePuzzleScript : MonoBehaviour {

    public GameObject elevator;

	void Update () {
        if (Input.GetKeyDown(KeyCode.H))
            MovePlatformUp();

        if (Input.GetKeyDown(KeyCode.P))
            MovePlatformDown();
    }

    void MovePlatformUp()
    {
        elevator.transform.DOMoveY(5.0f, 3.0f); 
    }

    void MovePlatformDown()
    {
        elevator.transform.DOMoveY(0.0f, 3.0f);
    }
}
