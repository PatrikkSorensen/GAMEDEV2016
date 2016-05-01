using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class BeaconPuzzleScript : MonoBehaviour {
    public GameObject chanelSwitch; 
    public GameObject[] lightStations;
    public Vector3 DOTMove = new Vector3(0.0f, 10.0f, 0.0f);

    private SplitWallsScript splitWallsScript; 
    private bool isScenePlaying, isSceneFinished, isActive = false;

    private bool shouldEventFire, eventTriggered = false; 

    void Start()
    {
        splitWallsScript = GetComponent<SplitWallsScript>(); 
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.M) && !isScenePlaying)
            StartCoroutine(PlayScene()); 

        if(chanelSwitch.GetComponent<ChannelSwitch>().IsActive)
            StartCoroutine(PlayScene());
    }

    void CheckLightStations()
    {

    }

    IEnumerator PlayScene()
    {
        Debug.Log("playing activation scene");
        isScenePlaying = true;

        StartCoroutine(splitWallsScript.SplitWalls());
        yield return null;
    }
}
