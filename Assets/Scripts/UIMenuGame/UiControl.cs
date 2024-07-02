using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiControl : MonoBehaviour
{
    public GameObject OptionPanel;
    public GameObject controlPanel;
    void Start()
    {

        controlPanel.SetActive(false);
    }

    public void OnControlsButtonClicked()
    {

        OptionPanel.SetActive(false);
        controlPanel.SetActive(true);
    }

    public void OnBackButtonClicked()
    {
        OptionPanel.SetActive(true);
        controlPanel.SetActive(false);   
    }
}
