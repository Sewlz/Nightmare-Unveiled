using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AwarenessManager : MonoBehaviour
{
    public Slider awarenessSlider;
    public Transform player;
    public GameObject playerObject;
    public float maxAwareness = 100f;
    public float currentAwareness;
    public float awarenessDecreaseRate = 5f; // T?c �? gi?m thanh t?nh t�o

    public float warningThreshold = 20f; // Ng�?ng c?nh b�o, c� th? ch?nh s?a t? Inspector
    public Image warningImage; // H?nh ?nh m? m�n h?nh
    public Image noiseImage; // H?nh ?nh nhi?u m�n h?nh
    public AudioSource warningAudio; // �m thanh c?nh b�o

    private bool isWarning = false;
    public DeathManager deathManager;
    void Start()
    {
        currentAwareness = maxAwareness;
        awarenessSlider.maxValue = maxAwareness;
        awarenessSlider.value = currentAwareness;
        deathManager = FindObjectOfType<DeathManager>();
        // �?t alpha c?a warningImage v? 0
        if (warningImage != null)
        {
            warningImage.color = new Color(warningImage.color.r, warningImage.color.g, warningImage.color.b, 0);
        }

        // �?t alpha c?a noiseImage v? 0
        if (noiseImage != null)
        {
            noiseImage.color = new Color(noiseImage.color.r, noiseImage.color.g, noiseImage.color.b, 0);
        }
    }

    void Update()
    {
        currentAwareness -= awarenessDecreaseRate * Time.deltaTime;
        awarenessSlider.value = currentAwareness;

        if (currentAwareness <= 0)
        {
            GameOver();
        }
        else if (currentAwareness <= maxAwareness * (warningThreshold / 100f))
        {
            if (!isWarning)
            {
                isWarning = true;
                StartWarning();
            }
            UpdateWarningEffect();
        }
        else
        {
            if (isWarning)
            {
                isWarning = false;
                StopWarning();
            }
        }
    }

    public void IncreaseAwareness(float amount)
    {
        currentAwareness += amount;
        if (currentAwareness > maxAwareness)
        {
            currentAwareness = maxAwareness;
        }
        awarenessSlider.value = currentAwareness;
    }

    void GameOver()
    {
        playerObject.SetActive(false);
        deathManager.TriggerDeath(player);
    }

    void StartWarning()
    {
        // B?t �?u ph�t �m thanh c?nh b�o
        if (warningAudio != null)
        {
            warningAudio.Play();
        }
    }

    void StopWarning()
    {
        // D?ng ph�t �m thanh c?nh b�o
        if (warningAudio != null)
        {
            warningAudio.Stop();
        }

        // �?t l?i h?nh ?nh c?nh b�o
        if (warningImage != null)
        {
            warningImage.color = new Color(warningImage.color.r, warningImage.color.g, warningImage.color.b, 0);
        }

        // �?t l?i h?nh ?nh nhi?u
        if (noiseImage != null)
        {
            noiseImage.color = new Color(noiseImage.color.r, noiseImage.color.g, noiseImage.color.b, 0);
        }
    }

    void UpdateWarningEffect()
    {
        // M? m�n h?nh d?n
        if (warningImage != null)
        {
            float alpha = 1.0f - (currentAwareness / (maxAwareness * (warningThreshold / 100f)));
            warningImage.color = new Color(warningImage.color.r, warningImage.color.g, warningImage.color.b, alpha);
        }

        // Hi?u ?ng nhi?u m�n h?nh
        if (noiseImage != null)
        {
            float noiseAlpha = 1.0f - (currentAwareness / (maxAwareness * (warningThreshold / 100f)));
            noiseImage.color = new Color(noiseImage.color.r, noiseImage.color.g, noiseImage.color.b, noiseAlpha);
        }
    }
}
