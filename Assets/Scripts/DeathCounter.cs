using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCounter : MonoBehaviour
{
    public int deathCounter = 0;
    void Start()
    {
        deathCounter = PlayerPrefs.GetInt("deathCounter");
    }
    public void Died()
    {
        deathCounter += 1;
        PlayerPrefs.SetInt("deathCounter",deathCounter);
    }
}
