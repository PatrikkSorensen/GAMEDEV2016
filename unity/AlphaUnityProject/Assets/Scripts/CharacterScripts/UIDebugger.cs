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

	void Update () {
        PlayerStatusScript test = B4.GetComponent<PlayerStatusScript>(); 
        UpdateGUI();
    }

    void UpdateGUI()
    {
        UpdateMiMiGUI();
        UpdateB4GUI(); 
    }

    void UpdateMiMiGUI()
    {
        GUIMiMi.text = "MiMI: " + "\nisBonded: " + MiMiStatus.getBondStatus() +
       "\nspeed: " + MiMiStatus.getSpeed() +
       "\n canEmpower: " + MiMiStatus.getCanEmpowerStatus() +
        "\nempowered: " + MiMiStatus.getEmpowerStatus() + 
        "\ncan slingshot: " + MiMiStatus.getCanSlingShot();
    }

    void UpdateB4GUI()
    {
        GUIB4.text = "B4: " + "\nisBonded: " + B4Status.getBondStatus() +
        "\nspeed: " + B4Status.getSpeed() +
        "\n canEmpower: " + B4Status.getCanEmpowerStatus() +
        "\nempowered: " + B4Status.getEmpowerStatus() +
        "\ncan slingshot: " + B4Status.getCanSlingShot();
    }
}
