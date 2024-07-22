using UnityEngine;

public class Flashflight : MonoBehaviour
{
    public GameObject flashLight;
    public AudioSource turnOn;
    public AudioSource turnOff;

    public bool isOn = false;

    void Start()
    {
        flashLight.SetActive(false);
        if (isOn)
        {
            flashLight.SetActive(true);
        }
    }

    public void ToggleFlashlight()
    {
        isOn = !isOn;
        flashLight.SetActive(isOn);

        if (isOn)
        {
            turnOn.Play();
        }
        else
        {
            turnOff.Play();
        }
    }
}
