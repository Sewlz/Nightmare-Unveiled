using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseGame : MonoBehaviour
{
    [Header("Pause")]
    [SerializeField] private GameObject Pausegame;
    [Header("Look Script")]
    [SerializeField] private Lookscript lookScript;
    [Header("Menu Manager")]
    [SerializeField] private MenuManager menuManager;
    [Header("Settings Panel")]
    [SerializeField] private GameObject settingsPanel;
    [Header("Control Panel")]
    [SerializeField] private GameObject controlPanel;

    private bool isPaused = false;

    private void Awake()
    {
        Pausegame.SetActive(false);
        if (lookScript == null)
        {
            lookScript = FindObjectOfType<Lookscript>();
        }
        if (menuManager == null)
        {
            menuManager = FindObjectOfType<MenuManager>();
        }
        if (settingsPanel == null)
        {
            Debug.LogError("Settings Panel not assigned.");
        }
        if (controlPanel == null)
        {
            Debug.LogError("Control Panel not assigned.");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingsPanel.activeInHierarchy || controlPanel.activeInHierarchy)
            {
                // N?u b?t k? menu con n�o �ang m?, ��ng menu con v� ti?p t?c game
                if (settingsPanel.activeInHierarchy)
                {
                    settingsPanel.SetActive(false);
                }
                if (controlPanel.activeInHierarchy)
                {
                    controlPanel.SetActive(false);
                }

                // Ti?p t?c game n?u �ang ? tr?ng th�i t?m d?ng
                if (isPaused)
                {
                    ResumeGame();
                }
            }
            else if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGameu();
            }
        }
    }

    private void PauseGameu()
    {
        Pausegame.SetActive(true);
        Time.timeScale = 0f;
        AudioListener.pause = true; // T?m d?ng t?t c? �m thanh
        lookScript.enabled = false; // V� hi?u h�a Lookscript n?u c?n
        Cursor.visible = true; // Hi?n th? con tr? chu?t
        Cursor.lockState = CursorLockMode.None; // Kh�ng kh�a con tr? chu?t
        isPaused = true;
    }

    public void ResumeGame()
    {
        Pausegame.SetActive(false);
        Time.timeScale = 1f;
        AudioListener.pause = false; // B?t l?i t?t c? �m thanh
        lookScript.enabled = true; // B?t l?i Lookscript n?u c?n
        Cursor.visible = false; // ?n con tr? chu?t
        Cursor.lockState = CursorLockMode.Locked; // Kh�a con tr? chu?t v�o gi?a m�n h?nh
        isPaused = false;
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void OpenControlPanel()
    {
        controlPanel.SetActive(true);
    }
}
