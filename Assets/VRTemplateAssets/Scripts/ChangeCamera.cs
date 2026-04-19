using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.InputSystem;

public class ChestCameraSwitch : MonoBehaviour
{
    [Header("Cameras")]
    public Camera mainVRCamera;
    public Camera puzzleCamera;
    public Collider cubeCollider;
    private bool isOnPuzzleCamera = false;
    private bool playerNearChest = false;

    public GameObject xrRayInteractorLeft;
    public GameObject xrRayInteractorRight;

    void Update()
    {
        if (Keyboard.current.tKey.wasPressedThisFrame && playerNearChest)
        {
            if (!isOnPuzzleCamera)
                SwitchToPuzzleCamera();
            else
                SwitchToMainCamera();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearChest = true;
            Debug.Log("Player near chest");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearChest = false;
            Debug.Log("Player left chest");
        }
    }
    
    public void SwitchToPuzzleCamera()
    {
        mainVRCamera.tag = "Untagged";
        puzzleCamera.tag = "MainCamera";
        mainVRCamera.enabled = false;
        puzzleCamera.enabled = true;
        cubeCollider.enabled = false;
        xrRayInteractorLeft.GetComponent<NearFarInteractor>().enabled = false;
        xrRayInteractorRight.GetComponent<NearFarInteractor>().enabled = false;
        isOnPuzzleCamera = true;
        Debug.Log("Switched to puzzle camera");
    }

    public void SwitchToMainCamera()
    {
        puzzleCamera.tag = "Untagged";
        mainVRCamera.tag = "MainCamera";
        puzzleCamera.enabled = false;
        mainVRCamera.enabled = true;
        cubeCollider.enabled = true;
        xrRayInteractorLeft.GetComponent<NearFarInteractor>().enabled = true;
        xrRayInteractorRight.GetComponent<NearFarInteractor>().enabled = true;
        isOnPuzzleCamera = false;
    }
}