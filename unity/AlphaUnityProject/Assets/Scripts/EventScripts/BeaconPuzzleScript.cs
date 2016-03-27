using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class BeaconPuzzleScript : MonoBehaviour {
    public GameObject wallToMove; 
    public GameObject[] lightStations;
    private List <BeaconLightScript> beaconScripts = new List<BeaconLightScript>();
    private bool shouldEventFire, eventTriggered = false; 

    void Start()
    {
        foreach(GameObject g in lightStations)
        {
            beaconScripts.Add(g.GetComponent<BeaconLightScript>()); 
        }
    }

    void Update()
    {
        if(beaconScripts.Count == 0)
        {
            Debug.Log("Destroying puzzle, there is no script assigned");
            Destroy(gameObject.GetComponent<BeaconPuzzleScript>()); 
        }

        foreach(BeaconLightScript b in beaconScripts)
        {
            if (!b.isActive)
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
    }

    void triggerEvent()
    {
        Debug.Log("Firing beacon event!");
        eventTriggered = true;

        DOTween.Init(false, true, LogBehaviour.ErrorsOnly);
        wallToMove.transform.DOMove(new Vector3(0.0f, 10.0f, 0), 10).SetRelative().SetLoops(1, LoopType.Incremental);
        
    }


}
