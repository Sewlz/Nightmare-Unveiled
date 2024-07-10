using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class MonitorController : MonoBehaviour
{
    public GameObject canvas;  // K�o th? Canvas c?a b?n v�o ��y
    public Text displayText;  // K�o th? Text component c?a b?n v�o ��y
    public VideoPlayer videoPlayer;  // K�o th? VideoPlayer c?a b?n v�o ��y (n?u s? d?ng video)

    private bool isActive = false;
    private string message = "";
    private bool isInteracting = false;  // Bi?n �? ki?m tra li?u c� �ang t��ng t�c v?i m�n h?nh hay kh�ng

    void Start()
    {
        canvas.SetActive(false);  // ?n canvas khi b?t �?u tr? ch�i
    }

    public void ToggleScreen()
    {
        isActive = !isActive;  // B?t ho?c t?t canvas
        canvas.SetActive(isActive);

        if (isActive)
        {
            displayText.text = message;  // C?p nh?t text khi b?t canvas
        }
        else
        {
            displayText.text = "";  // X�a text khi t?t canvas
        }

        if (videoPlayer != null)
        {
            if (!isInteracting)
            {
                if (isActive)
                {
                    videoPlayer.Stop();  // D?ng video khi m�n h?nh ��?c b?t v� kh�ng t��ng t�c
                }
                else
                {
                    videoPlayer.Play();  // Ch?y video khi m�n h?nh ��?c t?t v� kh�ng t��ng t�c
                }
            }
        }
    }

    public void SetMessage(string newMessage)
    {
        message = newMessage;  // C�i �?t th�ng �i?p �? hi?n th?
    }

    public void SetInteracting(bool value)
    {
        isInteracting = value;  // C�i �?t tr?ng th�i t��ng t�c
    }
}
