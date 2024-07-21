using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiMenuSelection2 : MonoBehaviour
{
    public GameObject mainMenuPanel; 
    public GameObject LevelSelectionPanel;  

    void Start()
    {
        LevelSelectionPanel.SetActive(false);
    }

    public void OnControlButtonClicked()
    {
        mainMenuPanel.SetActive(false);
        LevelSelectionPanel.SetActive(true);
    }

    public void OnBackButtonClicked()
    {
        mainMenuPanel.SetActive(true);
        LevelSelectionPanel.SetActive(false);
    }
}
