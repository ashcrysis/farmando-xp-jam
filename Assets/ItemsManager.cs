using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    public GameObject keyImage;
    public GameObject maskImage;
    public bool isWithKey = true;
    void Start()
    {
        if (PlayerPrefs.GetInt("endgame") == 0 )
        {
            keyImage.SetActive(PlayerPrefs.GetInt("hasKey") == 1);
        }
            maskImage.SetActive(PlayerPrefs.GetInt("endgame") == 1);
        if (PlayerPrefs.GetInt("endgame") == 1)
        {
            keyImage.SetActive(false);
        }
    }

    void Update()
    {
        if (PlayerPrefs.GetInt("endgame") == 0 )
        {
            keyImage.SetActive(PlayerPrefs.GetInt("hasKey") == 1);
        }

        maskImage.SetActive(PlayerPrefs.GetInt("endgame") == 1);

        if (PlayerPrefs.GetInt("endgame") == 1 || !isWithKey)
        {
            keyImage.SetActive(false);
        }
    }
}
