using UnityEngine;
using System.Collections;
using DG.Tweening;

public class DOTWeenExamples : MonoBehaviour {
    public AnimationCurve curve;
    public float seconds;
    void Update()
    {
        if (Input.GetKey(KeyCode.X))
            transform.DOMoveX(4, 1).SetEase(curve);

        if (Input.GetKey(KeyCode.C))
            transform.DOMoveX(0, 1).SetEase(curve);
    }
}
