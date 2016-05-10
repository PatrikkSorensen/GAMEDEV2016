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

    private AudioSource[] sfxSounds;
    private List<CurcuitLines> m_stationlines = new List<CurcuitLines>();
    private CurcuitLines m_templelines;
    private bool isScenePlaying, isSceneFinished = false;
	
	void Start () {
        m_stationlines.Add(lightstation1.GetComponentInChildren<CurcuitLines>());
        m_stationlines.Add(lightstation2.GetComponentInChildren<CurcuitLines>());

        m_templelines = GetComponent<CurcuitLines>(); 
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
        bool fireEvent = true; 
        foreach(CurcuitLines lines in m_stationlines)
        {
            if (!lines.HasChannelled)
                fireEvent = false; 
        }

        if (fireEvent && !isScenePlaying)
            sfxSounds[0].Play();
            sfxSounds[1].Play();
            StartCoroutine(PlayActivatitonScene());
            new WaitForSeconds(moveSpeed);
            sfxSounds[1].Stop();
    }


    IEnumerator PlayActivatitonScene()
    {
        Debug.Log("playing activation scene");
        isScenePlaying = true;

        m_templelines.ChanelLines(); 

        DOTween.Init(false, true, LogBehaviour.ErrorsOnly);
        doorToMove.transform.DOMove(DOTMove, moveSpeed).SetRelative().SetLoops(1, LoopType.Incremental);

        yield return null;
        
    }
}
