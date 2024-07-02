using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public Slider volumeSlider;
    public Slider mouseSpeedSlider;
    public Slider brightnessSlider;
    public Button setDefaultButton;

    private float defaultVolume = 1.0f;
    private float defaultMouseSpeed = 1.0f;
    private float defaultBrightness = 1.0f;

    void Start()
    {
        // Thi?t l?p giá tr? m?c ð?nh
        volumeSlider.value = defaultVolume;
        mouseSpeedSlider.value = defaultMouseSpeed;
        brightnessSlider.value = defaultBrightness;

        // Ðãng k? s? ki?n OnValueChanged cho các slider
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        mouseSpeedSlider.onValueChanged.AddListener(OnMouseSpeedChanged);
        brightnessSlider.onValueChanged.AddListener(OnBrightnessChanged);

        setDefaultButton.onClick.AddListener(OnSetDefaultButtonClicked);
    }

    void OnVolumeChanged(float value)
    {
        // Thay ð?i âm lý?ng
        AudioListener.volume = value;
    }

    void OnMouseSpeedChanged(float value)
    {
        // Thay ð?i t?c ð? chu?t (gi? ð?nh b?n có h? th?ng ð? ði?u ch?nh t?c ð? chu?t)
        // Example: MouseSensitivity.Instance.SetSpeed(value);
    }

    void OnBrightnessChanged(float value)
    {
        // Thay ð?i ð? sáng (gi? ð?nh b?n có h? th?ng ð? ði?u ch?nh ð? sáng)
        // Example: BrightnessController.Instance.SetBrightness(value);
    }


    void OnSetDefaultButtonClicked()
    {
        // Ð?t l?i giá tr? m?c ð?nh
        volumeSlider.value = defaultVolume;
        mouseSpeedSlider.value = defaultMouseSpeed;
        brightnessSlider.value = defaultBrightness;
    }
}

