using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonfire : MonoBehaviour
{
    public int ID;
    public Vector2 position;

    private SaveManager saveManager;
    public Transform bonfirePoint;

    void Start()
    {
        position = transform.position;
        saveManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<SaveManager>();
    }

    void Update()
    {
        if (GetComponent<Interavel>().canInteract && Input.GetKeyDown(KeyCode.C) && GetComponentInParent<DialoguePlayer>().isPlaying)
        {
            if (!PlayerPrefs.HasKey("BonfireIDs"))
            {
                List<int> bonfireIDs = new List<int>();
                bonfireIDs.Add(ID);
                PlayerPrefs.SetString("BonfireIDs", string.Join(",", bonfireIDs));
            }
            else
            {
                string bonfireIDsString = PlayerPrefs.GetString("BonfireIDs");
                List<int> bonfireIDs = new List<int>(System.Array.ConvertAll(bonfireIDsString.Split(','), int.Parse));
                Debug.Log(bonfireIDs);
                if (!bonfireIDs.Contains(ID))
                {
                    bonfireIDs.Add(ID);
                    PlayerPrefs.SetString("BonfireIDs", string.Join(",", bonfireIDs));

                    // Increment timeRemaining by 30 seconds
                    GameObject.FindGameObjectWithTag("Timer").GetComponent<CountdownTimer>().timeRemaining += 30f;
                }
            }

            saveManager.lastBonfireID = ID;
            saveManager.lastBonfirePosition = bonfirePoint.position;
            saveManager.remainingTime = GameObject.FindGameObjectWithTag("Timer").GetComponent<CountdownTimer>().timeRemaining;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Stamina>().stamina = GameObject.FindGameObjectWithTag("Player").GetComponent<Stamina>().MaxStamina;
            saveManager.Save();
        }
    }
}
