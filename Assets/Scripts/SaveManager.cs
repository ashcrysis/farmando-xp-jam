using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public int lastBonfireID;
    public Vector2 lastBonfirePosition;
    public bool endgame = false;
    public float remainingTime;

    void Start()
    {
        Read();
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
        PlayerPrefs.SetInt("LastBonfireID", lastBonfireID);
        PlayerPrefs.SetFloat("LastBonfirePositionX", lastBonfirePosition.x);
        PlayerPrefs.SetFloat("LastBonfirePositionY", lastBonfirePosition.y);
        PlayerPrefs.SetFloat("remainingTime", remainingTime);
        PlayerPrefs.SetInt("Endgame", endgame ? 1 : 0);

        PlayerPrefs.Save();
    }

    public void Read()
    {
        if (PlayerPrefs.HasKey("LastBonfireID"))
        {
            lastBonfireID = PlayerPrefs.GetInt("LastBonfireID");
            lastBonfirePosition = new Vector2(
                PlayerPrefs.GetFloat("LastBonfirePositionX"),
                PlayerPrefs.GetFloat("LastBonfirePositionY")
            );
            endgame = PlayerPrefs.GetInt("Endgame") == 1;
        }
        else
        {
            Debug.LogWarning("No save data found in PlayerPrefs!");
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
