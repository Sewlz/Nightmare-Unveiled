using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float sensitivity = 1.0f;

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // C?p nh?t góc nh?n d?a trên chu?t
        // Ví d?: transform.Rotate(-mouseY, mouseX, 0);
    }
}
