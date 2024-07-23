using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        volumeSlider.value = defaultVolume;
        mouseSpeedSlider.value = defaultMouseSpeed;
        brightnessSlider.value = defaultBrightness;

        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        mouseSpeedSlider.onValueChanged.AddListener(OnMouseSpeedChanged);
        brightnessSlider.onValueChanged.AddListener(OnBrightnessChanged);

        setDefaultButton.onClick.AddListener(OnSetDefaultButtonClicked);
    }

    void OnVolumeChanged(float value)
    {
      
        AudioListener.volume = value;
    }

    void OnMouseSpeedChanged(float value)
    {
        MouseSensitivity.Instance.SetSpeed(value);
    }


    void OnBrightnessChanged(float value)
    {
     
        RenderSettings.ambientIntensity = value;
    }

    void OnSetDefaultButtonClicked()
    {
  
        volumeSlider.value = defaultVolume;
        mouseSpeedSlider.value = defaultMouseSpeed;
        brightnessSlider.value = defaultBrightness;
    }
}
