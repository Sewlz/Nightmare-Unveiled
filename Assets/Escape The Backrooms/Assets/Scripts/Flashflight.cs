using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Flashflight : MonoBehaviour
{
    public GameObject flashLight;

    public AudioSource turnOn;
    public AudioSource turnOff;

    public bool on;
    public bool off;
    // Start is called before the first frame update
    void Start()
    {
        off = true;
        flashLight.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (off && Input.GetKeyDown(KeyCode.T))
        {
            flashLight.SetActive(true);
            turnOn.Play();
            off = false;
            on = true;
        }
        else if (on && Input.GetKeyDown(KeyCode.T))
        {
            flashLight.SetActive(false);
            turnOff.Play();
            off = true;
            on = false;

        }

    }
}
