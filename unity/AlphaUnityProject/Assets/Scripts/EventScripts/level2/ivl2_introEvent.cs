using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class ivl2_introEvent : MonoBehaviour {

    public KeyCode debugKey; 
    public AnimationCurve fadeCurve;
    public float fadeTime;
    public GameObject UIImage; 

    private GameObject B4, MiMi;
    private Camera cam;
    private Image image;
    private bool hasPlayed = false; 

    void Start()
    {
        image = UIImage.GetComponentInChildren<Image>();
        B4 = GameObject.FindGameObjectWithTag("B4");
        MiMi = GameObject.FindGameObjectWithTag("MiMi");
        cam = Camera.main;
        FadeFromBlack();
        StartCoroutine(createTheBond());
    }

    void Update()
    {
        if (Input.GetKey(debugKey) && !hasPlayed)
        {
            Debug.Log("Playing lvl 2 intro scene");
            FadeFromBlack();
            StartCoroutine(createTheBond());
            hasPlayed = true; 
        }
    }

    void FadeFromBlack()
    {
        Debug.Log(image.name);
        DOTween.To(() => image.color, x => image.color = x, Color.clear, fadeTime).SetEase(fadeCurve);
    }

    // Create players and bond them
    IEnumerator createTheBond()
    {
        yield return new WaitForSeconds(0.5f);
        B4.GetComponent<EstablishBond>().CreateBond();
    }
}
