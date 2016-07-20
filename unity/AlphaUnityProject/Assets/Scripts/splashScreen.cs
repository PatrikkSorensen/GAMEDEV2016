using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class splashScreen : MonoBehaviour {

    public void ChangeLevel(int level)
    {
        SceneManager.LoadScene(level); 
    }

    public void Test()
    {
        Debug.Log("Testing"); 
    }
}
