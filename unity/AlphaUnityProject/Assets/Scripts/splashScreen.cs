using UnityEngine;
using System.Collections;

public class splashScreen : MonoBehaviour {
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Joystick1Button1))
            Application.LoadLevel(1); 
	}
}
