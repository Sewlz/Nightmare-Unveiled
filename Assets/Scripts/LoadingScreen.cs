using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoadingScreen : MonoBehaviour
{
    public Slider progressBar;
    public TextMeshProUGUI loadingText;
    public TextMeshProUGUI mapNameText;
    public TextMeshProUGUI tipsText;
    public Image backgroundImage; // Th�m �?i t�?ng Image cho background
    public float progressSpeed = 0.5f; // T?c �? t�ng c?a thanh ti?n tr?nh

    private string nextScene;

    private void Start()
    {
        nextScene = PlayerPrefs.GetString("NextScene");
        string mapName = MapInfo.GetMapName(nextScene);
        string tips = MapInfo.GetMapTips(nextScene);
        string backgroundPath = MapInfo.GetMapBackground(nextScene);

        mapNameText.text = mapName;
        tipsText.text = tips;

        // Load v� g�n background image
        if (!string.IsNullOrEmpty(backgroundPath))
        {
            Sprite backgroundSprite = Resources.Load<Sprite>(backgroundPath);
            if (backgroundSprite != null)
            {
                backgroundImage.sprite = backgroundSprite;
            }
        }

        StartCoroutine(LoadAsyncScene());
    }

    IEnumerator LoadAsyncScene()
    {
        Debug.Log("Loading next scene: " + nextScene);

        AsyncOperation operation = SceneManager.LoadSceneAsync(nextScene);
        operation.allowSceneActivation = false; // Ng�n ch?n t? �?ng k�ch ho?t scene m?i khi t?i xong

        float targetProgress = 0f; // Ti?n tr?nh m?c ti�u
        float displayedProgress = 0f; // Ti?n tr?nh hi?n th? hi?n t?i

        while (!operation.isDone)
        {
            targetProgress = Mathf.Clamp01(operation.progress / 0.9f);
            displayedProgress = Mathf.Lerp(displayedProgress, targetProgress, progressSpeed * Time.deltaTime);

            progressBar.value = displayedProgress;
            loadingText.text = "Loading... " + (displayedProgress * 100).ToString("F0") + "%";

            // K�ch ho?t scene m?i khi t?i xong
            if (displayedProgress >= 0.99f)
            {
                loadingText.text = "Loading... 100%";
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
