using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;
using System.Collections.Generic;

public class DeathManager : MonoBehaviour
{
    public GameObject player;
    public SaveManager saveManager;
    public Camera playerCamera;
    public Transform closestEnemy;
    public List<Transform> enemyTransforms = new List<Transform>();
    public CanvasGroup fadeGroup;
    public Image background;
    public TMP_Text youDiedText;
    public Button playAgainButton;
    public Button returnToMenuButton;
    public float fadeDuration = 2f;
    public float rotateDuration = 2f;

    void Start()
    {
        FindAllEnemies();
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
        closestEnemy = FindClosestEnemy(playerTransform);
        if (closestEnemy != null)
        {
            StartCoroutine(DeathSequence(playerTransform));
        }
    }

    IEnumerator DeathSequence(Transform playerTransform)
    {
        background.gameObject.SetActive(true);
        fadeGroup.gameObject.SetActive(true);
        
        Quaternion initialRotation = playerTransform.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(closestEnemy.position - playerTransform.position);

        float t = 0;
        while (t < rotateDuration)
        {
            t += Time.deltaTime;
            playerCamera.transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, t / rotateDuration);
            fadeGroup.alpha = Mathf.Lerp(0, 1, t / rotateDuration); // Fade in the background
            yield return null;
        }

        player.SetActive(false);
        fadeGroup.alpha = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    
        youDiedText.gameObject.SetActive(true);
        playAgainButton.gameObject.SetActive(true);
        returnToMenuButton.gameObject.SetActive(true);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        if (saveManager != null){
            saveManager.ReducePlayerLives();
        } 
    }

    void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void FindAllEnemies()
    {
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemyObject in enemyObjects)
        {
            enemyTransforms.Add(enemyObject.transform);
        }
    }

    Transform FindClosestEnemy(Transform playerTransform)
    {
        Transform closest = null;
        float minDistance = Mathf.Infinity;
        Vector3 playerPosition = playerTransform.position;

        foreach (Transform enemy in enemyTransforms)
        {
            float distance = Vector3.Distance(enemy.position, playerPosition);
            if (distance < minDistance)
            {
                closest = enemy;
                minDistance = distance;
            }
        }

        return closest;
    }
}
