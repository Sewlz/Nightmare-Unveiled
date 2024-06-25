using UnityEngine;

public class Flashflight : MonoBehaviour
{
    public GameObject flashLight;
    public AudioSource turnOn;
    public AudioSource turnOff;

    private bool isOn = false;

    void Start()
    {
        flashLight.SetActive(false);
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
