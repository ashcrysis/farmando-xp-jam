using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonfire : MonoBehaviour
{
    public int ID;
    public Vector2 position;

    private SaveManager saveManager;
    public Transform bonfirePoint;
    public AudioSource bonfireSound;
    private float cooldownTime = 5f;
    private float nextInteractTime = 0f;
    void Start()
    {
        position = transform.position;
        saveManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<SaveManager>();
    }
 void Update()
    {
       
            if (GetComponent<Interavel>().canInteract && Input.GetKeyDown(KeyCode.C) && GetComponentInParent<DialoguePlayerRandom>().isPlaying)
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

                        GameObject.FindGameObjectWithTag("Timer").GetComponent<CountdownTimer>().timeRemaining += 10f;
                    }
                }
                
                saveManager.lastBonfireID = ID;
                saveManager.endgame = PlayerPrefs.GetInt("endgame") == 1 ? true:false;
                saveManager.lastBonfirePosition = bonfirePoint.position;
                saveManager.remainingTime = GameObject.FindGameObjectWithTag("Timer").GetComponent<CountdownTimer>().timeRemaining;
                GameObject.FindGameObjectWithTag("Player").GetComponent<Stamina>().stamina = GameObject.FindGameObjectWithTag("Player").GetComponent<Stamina>().MaxStamina;
                if (ID == 2)
                {
                    PlayerPrefs.SetInt("hasRest", 1);
                }
                saveManager.Save();
                 if (Time.time >= nextInteractTime)
                 {
                    bonfireSound.Play();
                }
                nextInteractTime = Time.time + cooldownTime;
            }
        
    }
}
