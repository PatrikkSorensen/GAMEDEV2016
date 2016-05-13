using UnityEngine;
using System.Collections;
using DG.Tweening;
using System;
using System.Collections.Generic;

public class TempleEventScript : MonoBehaviour {
    
    public GameObject doorToMove, lightstation1, lightstation2, startPoint, endPoint;
    public Vector3 DOTMove = new Vector3(0.0f, 10.0f, 0.0f);
    public float delayTransistion = 4.0f;
    public AnimationCurve doorcurve; 
    public float moveSpeed = 10.0f;
    public GameObject lineChaneller;
    public Point triggerPoint; 

    private CurcuitChanneller lineDrawer; 
    private AudioSource[] sfxSounds;
    
    private bool isScenePlaying, isSceneFinished = false;
	
	void Start () {
        lineDrawer = lineChaneller.GetComponent<CurcuitChanneller>(); 
        sfxSounds = doorToMove.GetComponents<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.G))
            PlayActivatitonScene();

        CurcuitLinesStatus(); 
    }

    private void CurcuitLinesStatus()
    {
        if (!triggerPoint.IsDrawn)
            return;

        if (!isScenePlaying)
            sfxSounds[0].Play();
        sfxSounds[1].Play();
        StartCoroutine(PlayActivatitonScene());
        new WaitForSeconds(moveSpeed);
        sfxSounds[1].Stop();
    }


    IEnumerator PlayActivatitonScene()
    {
        isScenePlaying = true;

        DOTween.Init(false, true, LogBehaviour.ErrorsOnly);
        doorToMove.transform.DOMove(DOTMove, moveSpeed).SetRelative().SetLoops(1, LoopType.Incremental);

        yield return null;
        
    }
}
