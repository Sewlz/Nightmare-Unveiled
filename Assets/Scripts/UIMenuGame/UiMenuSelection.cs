using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiMenuSelection : MonoBehaviour
{
    [Header("LevelSelection")]
    [SerializeField] private GameObject levelselection;

    private void Awake()
    {
        levelselection.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (levelselection.activeInHierarchy)
            {
                SelectionLevel(false);
            }
        }
    }


    #region Selection
    public void SelectionLevel(bool status)
    {
        //If status == true pause | if status == false unpause
        levelselection.SetActive(status);

    }

    public void BackToMainMenu()
    {
        SelectionLevel(false);
        // Add any additional logic to transition back to the main menu if necessary
    }
    #endregion
}
