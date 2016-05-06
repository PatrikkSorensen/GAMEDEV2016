using UnityEngine;
using System.Collections;

public class AnimatedTextureSwapping : MonoBehaviour {

    public GameObject rightEye, leftEye; 
    public Animator anim; 
    public Texture2D[] frames;
    public float timeBetweenFames = 0.05f;
    public Texture loveEyes, surprisedEyes, happyEyes; 
  

    private float framesPerSecond = 10.0f;
    private Material leftEyeMat, rightEyeMat; 

    void Update()
    {
        if (Input.GetKey(KeyCode.C))
        {
            StartCoroutine(ShowLoveEyes()); 
        }

        if (Input.GetKey(KeyCode.X))
            StartCoroutine(Blink()); 
    }

    IEnumerator Blink()
    {
        //TODO: Make more generic
        int[] sequence = { 0, 1, 2, 3, 2, 1, 0 };
        Renderer l_rend = leftEye.GetComponent<Renderer>(); 
        Renderer R_rend = rightEye.GetComponent<Renderer>();

        foreach (int index in sequence)
        {
            l_rend.material.mainTexture = frames[index];
            R_rend.material.mainTexture = frames[index];
            yield return new WaitForSeconds(timeBetweenFames);
        }

        anim.SetBool("blinking", false);
        anim.SetInteger("blink_int", 1);
        anim.SetBool("idle", true);

        yield return null; 
    }

    IEnumerator ShowLoveEyes()
    {
        //TODO: Make more generic
        int[] sequence = { 0, 1, 2, 3, 2, 1, 0 };
        Renderer l_rend = leftEye.GetComponent<Renderer>();
        Renderer R_rend = rightEye.GetComponent<Renderer>();

        for(int i = 0; i < 4; i++)
        {
            yield return new WaitForSeconds(1.0f);
        }
    }

    IEnumerator SurprisedEyes()
    {
        //TODO: Make more generic
        int[] sequence = { 0, 1, 2, 3, 2, 1, 0 };
        Renderer l_rend = leftEye.GetComponent<Renderer>();
        Renderer R_rend = rightEye.GetComponent<Renderer>();

        foreach (int index in sequence)
        {
            l_rend.material.mainTexture = frames[index];
            R_rend.material.mainTexture = frames[index];
            yield return new WaitForSeconds(timeBetweenFames);
        }

        anim.SetBool("blinking", false);
        anim.SetInteger("blink_int", 1);
        anim.SetBool("idle", true);

        yield return null;
    }

}
