using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    public int lastBonfireID;
    public Vector2 lastBonfirePosition;
    public bool endgame = false;
    private string saveFilePath;

    void Start()
    {
        saveFilePath = Path.Combine(Application.persistentDataPath, "saveData.json");
        StartCoroutine(SaveEveryFiveMinutes());
    }

    [System.Serializable]
    public class SaveData
    {
        public int lastBonfireID;
        public Vector2 lastBonfirePosition;
        public bool endgame;
    }

    public void Save()
    {
        SaveData data = new SaveData();
        data.lastBonfireID = lastBonfireID;
        data.lastBonfirePosition = lastBonfirePosition;
        data.endgame = endgame;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(saveFilePath, json);
    }

    public void Read()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            lastBonfireID = data.lastBonfireID;
            lastBonfirePosition = data.lastBonfirePosition;
            endgame = data.endgame;
        }
        else
        {
            Debug.LogWarning("Save file not found!");
        }
    }

    IEnumerator SaveEveryFiveMinutes()
    {
        while (true)
        {
            yield return new WaitForSeconds(300);
            Save();
        }
    }
}
