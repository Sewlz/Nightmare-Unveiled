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
            DontDestroyOnLoad(gameObject); // �?m b?o instance kh�ng b? x�a
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetSpeed(float value)
    {
        sensitivity = value;
        // C?p nh?t t?c �? chu?t ? ��y
        // V� d?: N?u b?n s? d?ng l?p �i?u khi?n camera, truy?n gi� tr? sensitivity v�o ��
        UpdateMouseSensitivity();
    }

    private void UpdateMouseSensitivity()
    {
        // V� d?: �i?u ch?nh t?c �? chu?t trong l?p �i?u khi?n camera
        // ��y ch? l� m?t v� d?, �i?u ch?nh theo c�ch b?n �ang s? d?ng t?c �? chu?t
        Camera.main.GetComponent<MouseLook>().sensitivity = sensitivity;
    }
}
