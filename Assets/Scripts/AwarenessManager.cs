using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AwarenessManager : MonoBehaviour
{
    public Slider awarenessSlider;
    public float maxAwareness = 100f;
    public float currentAwareness;
    public float awarenessDecreaseRate = 5f; // T?c ð? gi?m thanh t?nh táo

    public float warningThreshold = 20f; // Ngý?ng c?nh báo, có th? ch?nh s?a t? Inspector
    public Image warningImage; // H?nh ?nh m? màn h?nh
    public Image noiseImage; // H?nh ?nh nhi?u màn h?nh
    public AudioSource warningAudio; // Âm thanh c?nh báo

    private bool isWarning = false;

    void Start()
    {
        currentAwareness = maxAwareness;
        awarenessSlider.maxValue = maxAwareness;
        awarenessSlider.value = currentAwareness;

        // Ð?t alpha c?a warningImage v? 0
        if (warningImage != null)
        {
            warningImage.color = new Color(warningImage.color.r, warningImage.color.g, warningImage.color.b, 0);
        }

        // Ð?t alpha c?a noiseImage v? 0
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
        SceneManager.LoadScene("DeathSCenes");
    }

    void StartWarning()
    {
        // B?t ð?u phát âm thanh c?nh báo
        if (warningAudio != null)
        {
            warningAudio.Play();
        }
    }

    void StopWarning()
    {
        // D?ng phát âm thanh c?nh báo
        if (warningAudio != null)
        {
            warningAudio.Stop();
        }

        // Ð?t l?i h?nh ?nh c?nh báo
        if (warningImage != null)
        {
            warningImage.color = new Color(warningImage.color.r, warningImage.color.g, warningImage.color.b, 0);
        }

        // Ð?t l?i h?nh ?nh nhi?u
        if (noiseImage != null)
        {
            noiseImage.color = new Color(noiseImage.color.r, noiseImage.color.g, noiseImage.color.b, 0);
        }
    }

    void UpdateWarningEffect()
    {
        // M? màn h?nh d?n
        if (warningImage != null)
        {
            float alpha = 1.0f - (currentAwareness / (maxAwareness * (warningThreshold / 100f)));
            warningImage.color = new Color(warningImage.color.r, warningImage.color.g, warningImage.color.b, alpha);
        }

        // Hi?u ?ng nhi?u màn h?nh
        if (noiseImage != null)
        {
            float noiseAlpha = 1.0f - (currentAwareness / (maxAwareness * (warningThreshold / 100f)));
            noiseImage.color = new Color(noiseImage.color.r, noiseImage.color.g, noiseImage.color.b, noiseAlpha);
        }
    }
}
