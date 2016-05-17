using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class ivl2_introEvent : MonoBehaviour {

    public AnimationCurve fadeCurve;
    public float fadeTime;
    public GameObject UIImage; 

    private GameObject B4, MiMi;
    private Camera cam;
    private Image image;

    void Start()
    {
        image = UIImage.GetComponent<Image>();
        B4 = GameObject.FindGameObjectWithTag("B4");
        MiMi = GameObject.FindGameObjectWithTag("MiMi");
        cam = Camera.main;
        FadeFromBlack();
        StartCoroutine(createTheBond());
    }

    void FadeFromBlack()
    {
        DOTween.To(() => image.color, x => image.color = x, Color.clear, fadeTime).SetEase(fadeCurve);
    }

    // Create players and bond them
    IEnumerator createTheBond()
    {
        yield return new WaitForSeconds(0.5f);
        B4.GetComponent<EstablishBond>().CreateBond();
    }
}
