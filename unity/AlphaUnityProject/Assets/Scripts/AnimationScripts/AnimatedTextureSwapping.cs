using UnityEngine;
using System.Collections;

public class AnimatedTextureSwapping : MonoBehaviour {

    public Texture2D[] frames;
    float framesPerSecond = 10.0f;

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
        Renderer r = GetComponent<Renderer>(); 

        foreach(int index in sequence)
        {
            r.material.mainTexture = frames[index]; 
            yield return new WaitForSeconds(0.1f);
        }

        yield return null; 
    }
}
