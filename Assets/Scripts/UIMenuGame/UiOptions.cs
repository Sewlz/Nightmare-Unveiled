using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiOptions : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject OptionPanel;
    void Start()
    {

        OptionPanel.SetActive(false);
    }

    public void OnOptionsButtonClicked()
    {

        mainMenuPanel.SetActive(false);
        OptionPanel.SetActive(true);
    }

    public void OnBackButtonClicked()
    {

        mainMenuPanel.SetActive(true);
        OptionPanel.SetActive(false);
    }
}
