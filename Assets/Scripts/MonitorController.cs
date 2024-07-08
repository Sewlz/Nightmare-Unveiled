using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class MonitorController : MonoBehaviour
{
    public GameObject canvas;  // Kéo th? Canvas c?a b?n vào ðây
    public Text displayText;  // Kéo th? Text component c?a b?n vào ðây
    public VideoPlayer videoPlayer;  // Kéo th? VideoPlayer c?a b?n vào ðây (n?u s? d?ng video)

    private bool isActive = false;
    private string message = "";
    private bool isInteracting = false;  // Bi?n ð? ki?m tra li?u có ðang týõng tác v?i màn h?nh hay không

    void Start()
    {
        canvas.SetActive(false);  // ?n canvas khi b?t ð?u tr? chõi
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
            displayText.text = "";  // Xóa text khi t?t canvas
        }

        if (videoPlayer != null)
        {
            if (!isInteracting)
            {
                if (isActive)
                {
                    videoPlayer.Stop();  // D?ng video khi màn h?nh ðý?c b?t và không týõng tác
                }
                else
                {
                    videoPlayer.Play();  // Ch?y video khi màn h?nh ðý?c t?t và không týõng tác
                }
            }
        }
    }

    public void SetMessage(string newMessage)
    {
        message = newMessage;  // Cài ð?t thông ði?p ð? hi?n th?
    }

    public void SetInteracting(bool value)
    {
        isInteracting = value;  // Cài ð?t tr?ng thái týõng tác
    }
}
