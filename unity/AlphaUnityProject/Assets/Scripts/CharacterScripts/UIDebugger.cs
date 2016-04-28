using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIDebugger : MonoBehaviour {

    private Text GUIB4, GUIMiMi;

    private GameObject B4, MiMi;
    private PlayerStatusScript B4Status, MiMiStatus;

    void Start()
    {
        B4 = GameObject.FindGameObjectWithTag("B4");
        B4Status = B4.GetComponent<PlayerController>().playerStatus;
        GUIB4 = GameObject.FindGameObjectWithTag("GUIB4Status").GetComponent<Text>();

        MiMi = GameObject.FindGameObjectWithTag("MiMi");
        MiMiStatus = MiMi.GetComponent<PlayerController>().playerStatus;
        GUIMiMi = GameObject.FindGameObjectWithTag("GUIMiMiStatus").GetComponent<Text>();
    }

	// Update is called once per frame
	void Update () {
        UpdateGUI();
    }

    void UpdateGUI()
    {
        UpdateMiMiGUI();
        UpdateB4GUI(); 
    }

    void UpdateMiMiGUI()
    {
        GUIMiMi.text = gameObject.name + "\nisBonded: " + MiMiStatus.getBondStatus() +
       "\nspeed: " + MiMiStatus.getSpeed() +
        "\nchanneled" + MiMiStatus.getChannelStatus(); 
    }

    void UpdateB4GUI()
    {

    }
}
