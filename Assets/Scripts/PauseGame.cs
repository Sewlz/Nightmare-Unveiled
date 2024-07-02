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
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Pausegame.activeInHierarchy)
            {
                menuManager.ResumeGame();
            }
            else
            {
                menuManager.ShowPausePanel();
            }
        }
    }
}
