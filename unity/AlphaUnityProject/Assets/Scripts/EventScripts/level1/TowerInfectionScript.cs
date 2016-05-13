using UnityEngine;
using System.Collections;
using DG.Tweening;

public class TowerInfectionScript : MonoBehaviour {

    public GameObject sphere; 
    public Color endcolor;
    public AnimationCurve curve; 
    public float duration = 5.0f;
    public float pulseDuration = 1.0f;
    public float originalRange;

    [HideInInspector]
    public bool shouldPingPong, shouldPlayLightScene = false; 
    private CameraShake camShake; 
    private Light halo;

    void Start()
    {
        camShake = Camera.main.GetComponent<CameraShake>();
        halo = sphere.GetComponent<Light>();
        originalRange = halo.range;
    }

    void Update()
    {
        if (shouldPlayLightScene)
            StartCoroutine(PlayLightScene());


        if (shouldPingPong)
        {
            float amplitude = Mathf.PingPong(Time.time, duration);
            amplitude = amplitude / duration * 0.5F + 0.5F;
            halo.range = originalRange * amplitude;
        }
        
    }

    IEnumerator PlayLightScene()
    {
        camShake.Shake();
        halo.DOColor(endcolor, 3.0f).SetEase(curve); 
        yield return new WaitForSeconds(1.0f);

        pulseDuration = 1.0f;
    }
}
