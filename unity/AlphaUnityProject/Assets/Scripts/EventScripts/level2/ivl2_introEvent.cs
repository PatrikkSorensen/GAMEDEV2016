using UnityEngine;
using System.Collections;

public class ivl2_introEvent : MonoBehaviour {

    public AnimationCurve fadeInUIImageCurve;
    public float fadeUIImageTime;

    private GameObject B4, MiMi;

    void Start()
    {
        B4 = GameObject.FindGameObjectWithTag("B4");
        MiMi = GameObject.FindGameObjectWithTag("MiMi");

        StartCoroutine(createTheBond());
    }

    void Awake()
    {

    }

    // Fade in image 
    void FadeInUIImage()
    {
        //DOTween.To(() => image.color, x => image.color = x, Color.white, fadeUIImageTime).SetEase(fadeOutUIImageCurve);
    }

    // Shake camera

    // Create players and bond them
    IEnumerator createTheBond()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Creating the bond");
        Debug.Log("Creating the bond");
        Debug.Log("Creating the bond");
        Debug.Log("Creating the bond");
        Debug.Log("Creating the bond");
        Debug.Log("Creating the bond");
        Debug.Log("Creating the bond");
        Debug.Log("Creating the bond");
        Debug.Log("Creating the bond");
        Debug.Log("Creating the bond");
        Debug.Log("Creating the bond");
        Debug.Log("Creating the bond");
        Debug.Log("Creating the bond");
        Debug.Log("Creating the bond");
        Debug.Log("Creating the bond");
        B4.GetComponent<EstablishBond>().CreateBond();
    }
}
