﻿using UnityEngine;
using System.Collections;

public class ActivateCoreBehaviour : MonoBehaviour {

    public CurcuitChanneller[] curcuitChannellers; 
    public KeyCode debugKey;
    public GameObject ElevatorSphere; 

    private bool m_isActive;
    private AudioSource m_source; 

    void Start()
    {
        m_source = GetComponent<AudioSource>(); 
    }
    void Update()
    {
        if (Input.GetKey(debugKey) && !m_isActive)
            StartCore(); 

        if (!CheckEnergySources())
            return;

        if (!m_isActive)
            StartCore();
    } 

    void StartCore()
    {
        Debug.Log("Starting core...");
        m_isActive = true; 
        m_source.Play();
        GetComponent<Animator>().SetBool("hasPower", true);
    }

    void ActivateElevatorSphere()
    {

    }

    bool CheckEnergySources()
    {
        foreach (CurcuitChanneller c in curcuitChannellers)
            if (!c.HasChannelled)
            {
                Debug.Log("Returning false" + c.HasChannelled); 
                return false;
            }


        return true; 
    }
}
