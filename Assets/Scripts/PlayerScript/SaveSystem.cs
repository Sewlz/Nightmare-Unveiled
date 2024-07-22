using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public static class SaveSystem
{
 public static void SaveGame(GameData gameData)
    {
        string json = JsonUtility.ToJson(gameData);
        string path = Application.persistentDataPath + "/savefile.json";
        File.WriteAllText(path, json);

        Debug.Log("Game saved to: " + path);
    }

    public static GameData LoadGame()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            GameData data = JsonUtility.FromJson<GameData>(json);
            Debug.Log("Game loaded from: " + path);
            return data;
        }
        else
        {
            Debug.LogWarning("No save file found at: " + path);
            return null;
        }
    }
}

[System.Serializable]
public class GameData
{
    public List<ItemData> inventory;
    public float[] playerPosition;
    public float[] playerRotation;
    public string currentScene;
    public int playerLives =3;
    public List<SceneData> scenes;
}

[System.Serializable]
public class SceneData
{
    public string sceneName;
    public List<EnemyPosData> enemyPositions;
    public List<ItemStateData> itemStates;
}

[System.Serializable]
public class ItemData
{
    public string itemName;
    public bool isFlashlight;
    public bool isNote;
    public bool isEnDrink;
    public bool isMultiple;
    public bool isFuse;
    public bool isRemote;
     public bool isAdded;
    public string paragraph;
    public Sprite itemIcon;
    public int quantity = 0;
}

[System.Serializable]
public class EnemyPosData
{
    public float[] pos;
}

[System.Serializable]
public class ItemStateData
{
    public float[] position;
    public bool isActive;
}

