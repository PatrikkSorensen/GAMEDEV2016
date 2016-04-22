using UnityEngine;
using System.Collections;
using DG.Tweening;

public class ChanelEventScript : MonoBehaviour {
    public float delayTransistion = 4.0f;
    public GameObject hexaSwitch, lightstation, doorToMove;
    public Vector3 DOTMove = new Vector3(0.0f, 10.0f, 0.0f);
    //TODO: Make animation curve for how to tween door
    public float moveSpeed = 10.0f; 

    private LightStationScript stationScript;
    private HexagonSwitchScript switchScript;
    private bool isScenePlaying, isSceneFinished, isActive = false;
	
	void Start () {
        switchScript = hexaSwitch.GetComponent<HexagonSwitchScript>();
        stationScript = lightstation.GetComponent<LightStationScript>(); 
    }

    void Update()
    {
        if (stationScript.IsActive)
        {
            if (!isScenePlaying && !isSceneFinished)
            {
                PlayActivatitonScene();
            }

            if (isSceneFinished && !isActive)
            {
                Debug.Log("GameObject is active and scene is finished");
                isActive = true;

                Destroy(switchScript);
                Destroy(gameObject.GetComponent<ChanelEventScript>()); // TODO: disable Update function and register event in another way.
            }
        }
    }

    void PlayActivatitonScene() {
        Debug.Log("playing activation scene");
        isScenePlaying = true;

        DOTween.Init(false, true, LogBehaviour.ErrorsOnly);
        doorToMove.transform.DOMove(DOTMove, moveSpeed).SetRelative().SetLoops(1, LoopType.Incremental);
        //TODO:  Play animations and sounds

        // Trigger spherical waves
        //StartCoroutine(SendSphericalWaves());

        // Play sounds
    }

    // TODO: Refacot this, legacy code
    IEnumerator SendSphericalWaves()
    {
        Renderer rend = GetComponent<Renderer>();
        Material sphereMaterial = rend.materials[3];
        Color startMColor = sphereMaterial.GetColor("_Color");
        Color startEColor = sphereMaterial.GetColor("_EmissionColor");

        for (float f = startMColor.a; f <= 1; f += 0.005f)
        {
            startMColor.a = f;
            startEColor = Color.HSVToRGB(0, 0, f/6); 
            sphereMaterial.SetColor("_Color", startMColor);
            sphereMaterial.SetColor("_EmissionColor", startEColor);
            yield return null;
        }

        sphereMaterial.SetColor("_Color", Color.white);
        yield return new WaitForSeconds(delayTransistion);

        ActivateRobotHelpers();

        isSceneFinished = true; 
    }

    public void ActivateRobotHelpers()
    {
        //TODO: Implement this
        /**
        for each robot in robot
            trigger awake anim
            trigger awake sounds 
            move robot to destination
            if robot at destination 
                emit change to scripts
                trigger end state
            return
        end for loop
        */
    } 

}
