using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;

public class SaveManager : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public List<Transform> enemyTransforms = new List<Transform>();
    public List<GameObject> items = new List<GameObject>();
    public Vector3 startPosition;
    public Quaternion startRotation;
    public Camera playerCamera;
    public Toolbar toolbar;

    private string currentSceneName;
    private GameData gameData;

    void Start()
    {
        startPosition = transform.position;
        startRotation = playerCamera.transform.rotation;
        currentSceneName = SceneManager.GetActiveScene().name;
        FindAllEnemies();
        FindAllItems();
        LoadGame();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            SaveGame();
        }
        if (Input.GetKeyDown(KeyCode.F9))
        {
            LoadGame();
        }
    }

    void FindAllEnemies()
    {
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemyObject in enemyObjects)
        {
            enemyTransforms.Add(enemyObject.transform);
        }
    }

    void FindAllItems()
    {
        GameObject[] itemObjects = GameObject.FindGameObjectsWithTag("Item");
        foreach (GameObject itemObject in itemObjects)
        {
            items.Add(itemObject);
        }
    }

    public void SaveGame()
    {
        List<Vector3> enemyPositions = new List<Vector3>();
        List<ItemStateData> itemStates = new List<ItemStateData>();

        foreach (var enemyTransform in enemyTransforms)
        {
            if (enemyTransform != null)
            {
                enemyPositions.Add(enemyTransform.position);
                Debug.Log($"Saving enemy position: {enemyTransform.position}");
            }
            else
            {
                Debug.LogWarning("Null enemy transform found while saving positions.");
            }
        }

        foreach (var item in items)
        {
            if (item != null)
            {
                itemStates.Add(new ItemStateData 
                { 
                    position = new float[] { item.transform.position.x, item.transform.position.y, item.transform.position.z }, 
                    isActive = item.activeSelf 
                });
                Debug.Log($"Saving item position: {item.transform.position}, active: {item.activeSelf}");
            }
        }

        SceneData sceneData = new SceneData
        {
            sceneName = currentSceneName,
            enemyPositions = new List<EnemyPosData>(),
            itemStates = itemStates
        };

        foreach (var pos in enemyPositions)
        {
            EnemyPosData enemyPosData = new EnemyPosData
            {
                pos = new float[] { pos.x, pos.y, pos.z }
            };
            sceneData.enemyPositions.Add(enemyPosData);
        }

        if (gameData == null)
        {
            gameData = new GameData
            {
                scenes = new List<SceneData>()
            };
        }

        // Remove existing data for the current scene if it exists
        gameData.scenes.RemoveAll(scene => scene.sceneName == currentSceneName);

        gameData.scenes.Add(sceneData);
        gameData.inventory = new List<ItemData>();
        gameData.playerPosition = new float[] { transform.position.x, transform.position.y, transform.position.z };
        gameData.playerRotation = new float[] { playerCamera.transform.rotation.x, playerCamera.transform.rotation.y, playerCamera.transform.rotation.z, playerCamera.transform.rotation.w };
        gameData.currentScene = currentSceneName;

        int i = 0;
        foreach (var item in playerInventory.inventory)
        {
            ItemData itemData = new ItemData
            {
                itemName = item.itemName,
                isFlashlight = item.isFlashlight,
                isNote = item.isNote,
                paragraph = item.paragraph,
                isEnDrink = item.isEnDrink,
                isMultiple = item.isMultiple,
                itemIcon = item.itemIcon,
                quantity = Int32.Parse(toolbar.slotsQuantity[i].text)
            };
            gameData.inventory.Add(itemData);
            i++;
        }

        SaveSystem.SaveGame(gameData);
        Debug.Log($"Game saved successfully. Player position: {transform.position}, rotation: {playerCamera.transform.rotation}, scene: {currentSceneName}");
    }

    public void LoadGame()
    {
        gameData = SaveSystem.LoadGame();
        if (gameData != null)
        {
            if (gameData.currentScene != currentSceneName)
            {
                // If the current scene does not match the saved scene, clear data
                ClearData();
                Debug.LogWarning("Scene mismatch: Cleared data.");
                return;
            }

            playerInventory.inventory.Clear();
            foreach (var itemData in gameData.inventory)
            {
                Item item = ScriptableObject.CreateInstance<Item>();
                item.itemName = itemData.itemName;
                item.isFlashlight = itemData.isFlashlight;
                item.isNote = itemData.isNote;
                item.paragraph = itemData.paragraph;
                item.isMultiple = itemData.isMultiple;
                item.isEnDrink = itemData.isEnDrink;
                item.itemIcon = itemData.itemIcon;
                item.quantity = itemData.quantity;               
                playerInventory.inventory.Add(item);
            }

            Vector3 position = new Vector3(gameData.playerPosition[0], gameData.playerPosition[1], gameData.playerPosition[2]);
            transform.position = position;

            Quaternion rotation = new Quaternion(gameData.playerRotation[0], gameData.playerRotation[1], gameData.playerRotation[2], gameData.playerRotation[3]);
            playerCamera.transform.rotation = rotation;
            Debug.Log($"Loaded player position: {position}, rotation: {rotation}, scene: {gameData.currentScene}");

            SceneData currentSceneData = gameData.scenes.Find(scene => scene.sceneName == currentSceneName);

            if (currentSceneData != null)
            {
                // Update enemy positions
                for (int i = 0; i < enemyTransforms.Count && i < currentSceneData.enemyPositions.Count; i++)
                {
                    Vector3 enemyPosition = new Vector3(currentSceneData.enemyPositions[i].pos[0], currentSceneData.enemyPositions[i].pos[1], currentSceneData.enemyPositions[i].pos[2]);
                    enemyTransforms[i].position = enemyPosition;
                }

                // Update item positions and states
                foreach (var itemState in currentSceneData.itemStates)
                {
                    foreach (var item in items)
                    {
                        if (item != null && Vector3.Distance(item.transform.position, new Vector3(itemState.position[0], itemState.position[1], itemState.position[2])) < 0.1f)
                        {
                            Debug.Log($"Loading item state: position = {item.transform.position}, isActive = {itemState.isActive}");
                            item.SetActive(itemState.isActive);
                            item.transform.position = new Vector3(itemState.position[0], itemState.position[1], itemState.position[2]);
                        }
                    }
                }
            }
            else
            {
                Debug.LogWarning($"No saved data found for scene: {currentSceneName}");
            }
        }
    }
    public void ClearData()
    {
        playerInventory.inventory.Clear();
        enemyTransforms.Clear();
        items.Clear();
        FindAllEnemies();
        FindAllItems();
        transform.position = startPosition;
        playerCamera.transform.rotation = startRotation;
    }
}
