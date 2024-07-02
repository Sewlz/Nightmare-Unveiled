using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiMenuControl : MonoBehaviour
{
    [Header("Control")]
    [SerializeField] private GameObject PanelControler;

    private void Awake()
    {
        PanelControler.SetActive(false);
    }

    private void Update()
    {
            if (PanelControler.activeInHierarchy)
            {
                SelectionLevel(false);
            }

    }


    #region Selection
    public void SelectionLevel(bool status)
    {
        //If status == true pause | if status == false unpause
        PanelControler.SetActive(status);

    }

    public void BackToMainMenu()
    {
        SelectionLevel(false);
        // Add any additional logic to transition back to the main menu if necessary
    }
    #endregion
}
