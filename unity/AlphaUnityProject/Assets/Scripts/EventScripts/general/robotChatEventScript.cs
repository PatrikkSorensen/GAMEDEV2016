using UnityEngine;
using System.Collections;

public class robotChatEventScript : MonoBehaviour {

    private RobotChatScript robotChat;

	// Use this for initialization
	void Start () {
        robotChat = GameObject.FindGameObjectWithTag("B4").GetComponent<RobotChatScript>();
        if (robotChat == null)
        {
            robotChat = GameObject.FindGameObjectWithTag("MiMi").GetComponent<RobotChatScript>();
        }
	
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "B4" || other.gameObject.tag == "MiMi")
        {
            robotChat.startChat();
            gameObject.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
