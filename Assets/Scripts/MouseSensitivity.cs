using UnityEngine;

public class MouseSensitivity : MonoBehaviour
{
    public static MouseSensitivity Instance;

    public float sensitivity = 1.0f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Ð?m b?o instance không b? xóa
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetSpeed(float value)
    {
        sensitivity = value;
        // C?p nh?t t?c ð? chu?t ? ðây
        // Ví d?: N?u b?n s? d?ng l?p ði?u khi?n camera, truy?n giá tr? sensitivity vào ðó
        UpdateMouseSensitivity();
    }

    private void UpdateMouseSensitivity()
    {
        // Ví d?: Ði?u ch?nh t?c ð? chu?t trong l?p ði?u khi?n camera
        // Ðây ch? là m?t ví d?, ði?u ch?nh theo cách b?n ðang s? d?ng t?c ð? chu?t
        Camera.main.GetComponent<MouseLook>().sensitivity = sensitivity;
    }
}
