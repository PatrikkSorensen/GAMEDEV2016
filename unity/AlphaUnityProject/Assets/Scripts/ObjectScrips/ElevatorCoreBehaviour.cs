using UnityEngine;
using System.Collections;
using DG.Tweening; 

public class ElevatorCoreBehaviour : MonoBehaviour {

    public ActivateCoreBehaviour powerCore;
    public float targetIntensity = 1.0f;
    public float duration;
    public Color color;
    public KeyCode debugKey;

    private bool m_isActive = false;

    public bool IsActive
    {
        get { return m_isActive; }
        set { m_isActive = value; }
    } 
    private Light m_halolight;

    void Start()
    {
        m_halolight = gameObject.GetComponentInChildren<Light>();
        m_halolight.color = color;
    }

    void Update()
    { 
        if (powerCore.IsActive && !IsActive)
            ActivateCore(); 
    }

    void ActivateCore()
    {
        Debug.Log("Activating core...");
        m_halolight.DOIntensity(targetIntensity, duration);
        Debug.Log(m_halolight.name);
        IsActive = true; 
    }
}
