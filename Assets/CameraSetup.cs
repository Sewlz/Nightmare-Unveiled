using UnityEngine;

public class CameraSetup : MonoBehaviour
{
    void Start()
    {
        // Check if there is a camera in the scene
        Camera mainCamera = Camera.main;

        if (mainCamera == null)
        {
            // Create a new camera if none exists
            GameObject cameraObject = new GameObject("Main Camera");
            mainCamera = cameraObject.AddComponent<Camera>();
        }

        // Ensure the camera is enabled
        mainCamera.enabled = true;

        // Set camera properties (customize as needed)
        mainCamera.clearFlags = CameraClearFlags.Skybox;
        mainCamera.cullingMask = LayerMask.GetMask("Default");

        // Position the camera (customize as needed)
        mainCamera.transform.position = new Vector3(0, 1, -10);
        mainCamera.transform.rotation = Quaternion.Euler(0, 0, 0);

        Debug.Log("Camera is set up and ready to render.");
    }
}

