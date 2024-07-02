using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : MonoBehaviour
{

    [SerializeField] int MaxPage;
    int currentPage;
    Vector3 targetPos;
    [SerializeField] Vector3 pageSetup;
    [SerializeField] RectTransform levelPagesRect;

    [SerializeField] float tweenTime;
    [SerializeField] LeanTweenType tweenType;
    private UiMenuSelection uiManager;

    private void Awake()
    {
        currentPage = 1;
        targetPos = levelPagesRect.localPosition;
        uiManager = FindObjectOfType<UiMenuSelection>(); // Find the UiMenuSelection component
    }

    public void Next()
    {
        if (currentPage < MaxPage)
        {
            currentPage++;
            targetPos += pageSetup;
            MovePage();
        }
    }

    public void Previous()
    {
        if(currentPage > 1)
        {
            currentPage--;
            targetPos -= pageSetup;
            MovePage();
        }
    }

    void MovePage()
    {
        levelPagesRect.LeanMoveLocal(targetPos, tweenTime).setEase(tweenType);
    }

   public void Back()
    {
        if (uiManager != null)
        {
            uiManager.BackToMainMenu();
        }
    }
}
