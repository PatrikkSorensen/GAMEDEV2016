using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class BeaconPuzzleScript : MonoBehaviour {
    public GameObject wallToMove; 
    public GameObject[] lightStations;
    public Vector3 DOTMove = new Vector3(0.0f, 10.0f, 0.0f);
    private List <LightStationScript> lightstationScripts = new List<LightStationScript>();
    private bool shouldEventFire, eventTriggered = false; 

    void Start()
    {
        foreach(GameObject g in lightStations)
        {
            lightstationScripts.Add(g.GetComponent<LightStationScript>()); 
        }
    }

    void Update()
    {
        if(lightstationScripts.Count == 0)
        {
            Debug.LogWarning("Destroying puzzle, there is no scripts assigned to the lightstations");
            Destroy(gameObject.GetComponent<BeaconPuzzleScript>()); 
        }

        foreach (LightStationScript ls in lightstationScripts)
        {
            if (!ls.IsActive)
            {
                shouldEventFire = false;
                break;
            }

            shouldEventFire = true;
        }

        if (shouldEventFire && !eventTriggered)
        {
            triggerEvent(); 
        }

        if (Input.GetKey(KeyCode.C))
            triggerEvent();
    }

    void triggerEvent()
    {
        StartCoroutine(GetComponent<SplitWallsScript>().SplitWalls());
    }
}
