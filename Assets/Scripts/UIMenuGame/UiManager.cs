using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [Header("LevelSelection")]
    [SerializeField] private GameObject levelSelection1;
    [SerializeField] private GameObject levelSelection2;
    
    private void Awake()
    {
        if (levelSelection1 == null || levelSelection2 == null)
        {
            Debug.LogError("Level selection GameObjects are not assigned in the inspector!");
            return;
        }
        levelSelection1.SetActive(false);
        levelSelection2.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (levelSelection1.activeInHierarchy || levelSelection2.activeInHierarchy)
            {
                SelectionLevel(false);
            }
            else
            {
                SelectionLevel(true);
            }
        }
    }

    public void SelectionLevel(bool status)
    {
        
        levelSelection1.SetActive(status);
        if (!status)
        {
            levelSelection2.SetActive(false);
        }
    }

    public void ShowLevelSelection1()
    {
        levelSelection1.SetActive(true);
        levelSelection2.SetActive(false);
    }

    public void ShowLevelSelection2()
    {
        levelSelection1.SetActive(false);
        levelSelection2.SetActive(true);
    }

    public void NextLevelSelection()
    {
        if (levelSelection1.activeInHierarchy)
        {
            ShowLevelSelection2();
        }
    }

    public void PreviousLevelSelection()
    {
        if (levelSelection2.activeInHierarchy)
        {
            ShowLevelSelection1();
        }
    }
}
