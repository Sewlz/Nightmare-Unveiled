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
        // Thi?t l?p gi� tr? m?c �?nh
        volumeSlider.value = defaultVolume;
        mouseSpeedSlider.value = defaultMouseSpeed;
        brightnessSlider.value = defaultBrightness;

        // ��ng k? s? ki?n OnValueChanged cho c�c slider
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        mouseSpeedSlider.onValueChanged.AddListener(OnMouseSpeedChanged);
        brightnessSlider.onValueChanged.AddListener(OnBrightnessChanged);

        setDefaultButton.onClick.AddListener(OnSetDefaultButtonClicked);
    }

    void OnVolumeChanged(float value)
    {
        // Thay �?i �m l�?ng
        AudioListener.volume = value;
    }

    void OnMouseSpeedChanged(float value)
    {
        // Thay �?i t?c �? chu?t (gi? �?nh b?n c� h? th?ng �? �i?u ch?nh t?c �? chu?t)
        // Example: MouseSensitivity.Instance.SetSpeed(value);
    }

    void OnBrightnessChanged(float value)
    {
        // Thay �?i �? s�ng (gi? �?nh b?n c� h? th?ng �? �i?u ch?nh �? s�ng)
        // Example: BrightnessController.Instance.SetBrightness(value);
    }


    void OnSetDefaultButtonClicked()
    {
        // �?t l?i gi� tr? m?c �?nh
        volumeSlider.value = defaultVolume;
        mouseSpeedSlider.value = defaultMouseSpeed;
        brightnessSlider.value = defaultBrightness;
    }
}

