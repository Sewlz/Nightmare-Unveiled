using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject controlPanel;
    [Header("Look Script")]
    [SerializeField] private Lookscript lookScript;
    [SerializeField] private Toolbar toolbar;
    private void Start()
    {
        // Ensure all panels are initially deactivated
        pausePanel.SetActive(false);
        optionsPanel.SetActive(false);
        controlPanel.SetActive(false);

        if (lookScript == null)
        {
            lookScript = FindObjectOfType<Lookscript>();
        }
    }

    // Method to activate the pause panel
    public void ShowPausePanel()
    {
        pausePanel.SetActive(true);
        optionsPanel.SetActive(false);
        controlPanel.SetActive(false);
        Time.timeScale = 0; // Pause the game
        if (lookScript != null)
        {
            toolbar.enabled = false;
            lookScript.enabled = false; // Disable look script
            Cursor.lockState = CursorLockMode.None; // Unlock the cursor
            Cursor.visible = true; // Show cursor
        }
    }

    // Method to activate the options panel
    public void ShowOptionsPanel()
    {
        pausePanel.SetActive(false);
        optionsPanel.SetActive(true);
        controlPanel.SetActive(false);
    }

    // Method to activate the control panel
    public void ShowControlPanel()
    {
        pausePanel.SetActive(false);
        optionsPanel.SetActive(false);
        controlPanel.SetActive(true);
    }

    // Method to go back to the pause panel from options or control panel
    public void GoBackToPausePanel()
    {
        pausePanel.SetActive(true);
        optionsPanel.SetActive(false);
        controlPanel.SetActive(false);
    }
    public void GoBackToOptionsPanel()
    {
        pausePanel.SetActive(false);
        optionsPanel.SetActive(true);
        controlPanel.SetActive(false);
    }
    // Method to resume the game from the pause panel
    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        optionsPanel.SetActive(false);
        controlPanel.SetActive(false);
        Time.timeScale = 1; // Resume the game
        if (lookScript != null)
        {
            lookScript.enabled = true; // Enable look script
            toolbar.enabled = true;
            Cursor.lockState = CursorLockMode.Locked; // Lock the cursor
            Cursor.visible = false; // Hide cursor
        }
    }
}
