using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    public CameraTypes type;
    public GameObject player, playerTwo;

    public enum CameraTypes
    {
        THIRD_PERSON_CAMERA,
        SINGLE_PERSON_CAMERA
    }
    
    private ThirdPersonCameraScript thirdPersonCamera;
    private CameraFollow singlePersonCamera;

    void Awake()
    {
        thirdPersonCamera = gameObject.GetComponent<ThirdPersonCameraScript>();
        singlePersonCamera = gameObject.GetComponent<CameraFollow>();

        if (!player)
        {
            Debug.LogWarning("Please assign your players in inspector. Setting B4 from script.");
            player = GameObject.FindGameObjectWithTag("B4");
        }

        singlePersonCamera.player = player.transform;
        changeCameraType(type); 
    }

    public void changeCameraType(CameraTypes type)
    {
        if(type == CameraTypes.SINGLE_PERSON_CAMERA)
            transitionToSinglePersonCamera(); 
            
        if(type == CameraTypes.THIRD_PERSON_CAMERA)
            transitionToThirdPersonCamera();

   }

    private void transitionToSinglePersonCamera()
    {
        thirdPersonCamera.enabled = false;
        singlePersonCamera.enabled = true;

        // Set variables for camera type 

    }

    private void transitionToThirdPersonCamera()
    {
        thirdPersonCamera.enabled = true;
        singlePersonCamera.enabled = false;

        // Set variables for camera type
    }
}
