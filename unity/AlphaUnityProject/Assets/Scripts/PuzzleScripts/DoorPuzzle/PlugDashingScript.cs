using UnityEngine;
using System.Collections;
using DG.Tweening;

public class PlugDashingScript : MonoBehaviour {

    public float dashMassOfPlug;
    public AudioClip scrapeSound;
    public float distanceFromObjectWhereDashWorks;

    private GameObject b4, mimi;
    private Rigidbody rb;
    private float originalMass, originalVolume;
    private bool light = false;
    private AudioSource sfxSource;
    private float distance1, distance2;

	// Use this for initialization
	void Start () {
        b4 = GameObject.FindGameObjectWithTag("B4");
        mimi = GameObject.FindGameObjectWithTag("MiMi");
        rb = gameObject.GetComponent<Rigidbody>();
        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.clip = scrapeSound;
        sfxSource.loop = false;
        sfxSource.playOnAwake = false;
        sfxSource.pitch = 1.4f;

        originalMass = rb.mass;
        originalVolume = sfxSource.volume;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        distance1 = Vector3.Distance (gameObject.transform.position, b4.transform.position);
        distance2 = Vector3.Distance (gameObject.transform.position, mimi.transform.position);

        //Debug.Log(distance1 + " " + distance2);
        if ( Input.GetButtonDown("B4Dash"))
        {
            if (!light)
            {
                Debug.Log("Distance from B4 to " + gameObject.name + ": " + distance1);
                if (distance1 < distanceFromObjectWhereDashWorks)
                StartCoroutine(SetMass(1.0f));
            }
        }
        if (Input.GetButtonDown("MiMiDash"))
        {
            if (!light)
            {
                Debug.Log("Distance from MiMi to " + gameObject.name + ": " + distance2);
                if (distance2 < distanceFromObjectWhereDashWorks)
                    StartCoroutine(SetMass(1.0f));
            }
        }
	
	}

    IEnumerator SetMass(float duration)
    {
        light = true;
        sfxSource.Play();
        sfxSource.DOFade(0, duration);
        rb.mass = dashMassOfPlug;
        yield return new WaitForSeconds(duration);
        sfxSource.Stop();
        rb.mass = originalMass;
        sfxSource.volume = originalVolume;
        light = false;
    }
}
