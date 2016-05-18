using UnityEngine;
using System.Collections;

public class CameraLocationChangeEvent : MonoBehaviour {

    public Camera cam;
    public float upToUse;
    public float awayToUse;

    private float oldUp;
    private float oldAway;
    private ThirdPersonCameraScript cameraScript;
    private float lastChange;
	// Use this for initialization
	void Start () {
        cameraScript = cam.GetComponent<ThirdPersonCameraScript>();
        lastChange = Time.time;
	}

    void getOldPosition()
    {
        var oldPos = cameraScript.getPosition();
        oldUp = oldPos.x;
        oldAway = oldPos.y;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "B4")
        {
            getOldPosition();
            var timeDifference = Time.time - lastChange;
            //Debug.Log("Time difference = " + timeDifference);
            if (timeDifference > 0.05f)
            {
                //Debug.Log("oldUp = " + oldPos.x + " upToUse = " + upToUse + " oldAway = " + oldPos.y + " awayToUse = " + awayToUse);
                //if (oldUp != upToUse && oldAway != awayToUse)
                if (new Vector2(oldUp, oldAway) != new Vector2(upToUse, awayToUse))
                {
                    cameraScript.setPosition(awayToUse, upToUse);
                    lastChange = Time.time;
                }
                else
                {
                    cameraScript.setPosition(oldAway, oldUp);
                    lastChange = Time.time;
                }
            }
        }
    }
}
