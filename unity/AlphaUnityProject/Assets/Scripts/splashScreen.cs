using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using DG.Tweening; 

public class splashScreen : MonoBehaviour {

    void Start()
    {
        //Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked; 
    }

    public void ChangeLevel(int level)
    {

        StartCoroutine(StartChangeLevel(level)); 
        
    }

    IEnumerator StartChangeLevel(int level)
    {
        GetComponent<AudioSource>().Play();
        GetComponent<AudioSource>().DOFade(0.0f, 3.0f);

        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(level);
    }

    public void Test()
    {
        Debug.Log("Testing"); 
    }
}
