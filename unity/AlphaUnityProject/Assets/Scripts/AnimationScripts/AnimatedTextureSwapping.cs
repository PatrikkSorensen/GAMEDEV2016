using UnityEngine;
using System.Collections;

public class AnimatedTextureSwapping : MonoBehaviour {

    public GameObject rightEye, leftEye; 
    public Animator anim; 
    public Texture2D[] frames;
    private float framesPerSecond = 10.0f;
    public float timeBetweenFames = 0.05f; 

    private Material leftEyeMat, rightEyeMat; 

    void Update()
    {
        if (Input.GetKey(KeyCode.C))
        {
            int index = (int)Mathf.Round(Time.time * framesPerSecond);
            index = index % frames.Length;
            GetComponent<Renderer>().material.mainTexture = frames[index];
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
}
