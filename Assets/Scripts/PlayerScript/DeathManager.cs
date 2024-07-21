using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class DeathManager : MonoBehaviour
{
    public SaveManager saveManager;
    public Camera playerCamera;
    public Transform enemy;
    public CanvasGroup fadeGroup;
    public Image background;
    public TMP_Text youDiedText;
    public Button playAgainButton;
    public Button returnToMenuButton;
    public float fadeDuration = 2f;
    public float rotateDuration = 2f;

    void Start()
    {
        background.gameObject.SetActive(false);
        fadeGroup.gameObject.SetActive(false);
        youDiedText.gameObject.SetActive(false);
        playAgainButton.gameObject.SetActive(false);
        returnToMenuButton.gameObject.SetActive(false);
        fadeGroup.alpha = 0.5f;

        playAgainButton.onClick.AddListener(PlayAgain);
        returnToMenuButton.onClick.AddListener(ReturnToMenu);
    }


    public void TriggerDeath(Transform playerTransform)
    {
        StartCoroutine(DeathSequence(playerTransform));
    }

    IEnumerator DeathSequence(Transform playerTransform)
    {
        fadeGroup.gameObject.SetActive(true);
        // Rotate the player's camera to face the enemy
        Quaternion initialRotation = playerTransform.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(enemy.position - playerTransform.position);

        float t = 0;
        while (t < rotateDuration)
        {
            t += Time.deltaTime;
            playerTransform.rotation = Quaternion.Slerp(initialRotation, targetRotation, t / rotateDuration);
            yield return null;
        }

        // Fade to transparent
        t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            fadeGroup.alpha = Mathf.Lerp(0, 1, t / fadeDuration);
            yield return null;
        }

        // Enable cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Show UI elements
        youDiedText.gameObject.SetActive(true);
        playAgainButton.gameObject.SetActive(true);
        returnToMenuButton.gameObject.SetActive(true);
        background.gameObject.SetActive(true);
    }

    void PlayAgain()
    {
        // Reload the current scene
        saveManager.ClearData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void ReturnToMenu()
    {
        // Load the main menu scene (replace "MainMenu" with your scene name)
        saveManager.ClearData();
        SceneManager.LoadScene("MainMenu");
    }
}
