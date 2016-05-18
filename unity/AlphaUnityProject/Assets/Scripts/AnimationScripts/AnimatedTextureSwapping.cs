using UnityEngine;
using System.Collections;
using System;

public class AnimatedTextureSwapping : MonoBehaviour {

    public GameObject rightEye, leftEye; 

    public Texture2D[] frames;
    public float timeBetweenBlinkFrames = 0.05f;
    public float timeBetweenLoveFrames = 0.7f; 
    public Texture loveEyes, surprisedEyes, happyEyes;

    private int[] blinkSequence = { 0, 1, 2, 3, 2, 1, 0 };
    private Renderer l_rend;
    private Renderer r_rend;
    private Animator anim;

    private float framesPerSecond = 10.0f;
    private Material leftEyeMat, rightEyeMat; 

    void Start()
    {
        anim = GetComponent<Animator>(); 
        l_rend = leftEye.GetComponent<Renderer>();
        r_rend = rightEye.GetComponent<Renderer>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.C))
            anim.SetBool("blinking", true);

        if (Input.GetKey(KeyCode.X))
            anim.SetBool("love", true);

        DetermineIdleAnimation();
    }

    private void DetermineIdleAnimation()
    {
        if (anim.GetCurrentAnimatorStateInfo(1).IsName("mimi_eye1") && anim.GetInteger("idleint") > 0)
            return;

         
        Debug.Log("Determing idle animation");
        int animNumber = UnityEngine.Random.Range(0, 3);
        anim.SetInteger("idleint", animNumber); 
         
    }

    IEnumerator Blink()
    {
        foreach (int index in blinkSequence)
        {
            l_rend.material.mainTexture = frames[index];
            r_rend.material.mainTexture = frames[index];
            yield return new WaitForSeconds(timeBetweenBlinkFrames);
        }

        anim.SetBool("blinking", false);

        yield return null; 
    }

    IEnumerator ShowLoveEyes()
    {
        for (int i = 0; i < 5; i++)
        {
            if(i % 2 == 0)
            {
                l_rend.material.SetColor("_Color", Color.white);
                r_rend.material.SetColor("_Color", Color.white);
                l_rend.material.mainTexture = loveEyes;
                r_rend.material.mainTexture = loveEyes;
            } else
            {
                l_rend.material.SetColor("_Color", Color.black);
                r_rend.material.SetColor("_Color", Color.black);
            }

            yield return new WaitForSeconds(timeBetweenLoveFrames);
        }

        l_rend.material.SetColor("_Color", Color.white);
        r_rend.material.SetColor("_Color", Color.white);
        l_rend.material.mainTexture = frames[0];
        r_rend.material.mainTexture = frames[0];

        anim.SetBool("love", false);
    }

    IEnumerator SurprisedEyes()
    {

        l_rend.material.mainTexture = surprisedEyes;
        r_rend.material.mainTexture = surprisedEyes; 
        yield return null;
    }

}
