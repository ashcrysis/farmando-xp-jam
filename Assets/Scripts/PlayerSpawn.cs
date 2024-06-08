using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject starterPoint;

    void Start()
    {
        SpawnPlayerAtLastBonfire();
    }

    void SpawnPlayerAtLastBonfire()
    {
        if (PlayerPrefs.HasKey("LastBonfirePositionX") && PlayerPrefs.HasKey("LastBonfirePositionY"))
        {
            float spawnPositionX = PlayerPrefs.GetFloat("LastBonfirePositionX");
            float spawnPositionY = PlayerPrefs.GetFloat("LastBonfirePositionY");

            Vector2 spawnPosition = new Vector2(spawnPositionX, spawnPositionY);
            Instantiate(playerPrefab, new Vector3(spawnPosition.x, spawnPosition.y, 0), Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("No saved position found! Spawning player at default position.");
            Instantiate(playerPrefab, starterPoint.transform.position, Quaternion.identity);
        }
    }
}
