using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerSpawn : MonoBehaviour
{
    public GameObject playerPrefab;

    private string saveFilePath;
    public GameObject starterPoint;
    void Start()
    {
        saveFilePath = Path.Combine(Application.persistentDataPath, "saveData.json");
        SpawnPlayerAtLastBonfire();
    }

    void SpawnPlayerAtLastBonfire()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            SaveManager.SaveData data = JsonUtility.FromJson<SaveManager.SaveData>(json);

            Vector2 spawnPosition = data.lastBonfirePosition;
            Instantiate(playerPrefab, new Vector3(spawnPosition.x, spawnPosition.y, 0), Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Save file not found! Spawning player at default position.");
            Instantiate(playerPrefab, starterPoint.transform.position, Quaternion.identity);
        }
    }

}
