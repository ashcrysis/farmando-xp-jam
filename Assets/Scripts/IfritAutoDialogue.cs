using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfritAutoDialogue : MonoBehaviour
{
    private DeathCounter counter;
    [SerializeField] private GameObject[] dialogos;
    [SerializeField] private int[] deathNeeded;
    
void Start()
{
    counter = GetComponentInParent<DeathCounter>();

    for (int i = 0; i < dialogos.Length; i++)
    {
        if (PlayerPrefs.HasKey("Dialogue_" + i))
        {
            if (PlayerPrefs.GetInt("Dialogue_" + i) == 1)
            {
                dialogos[i].SetActive(false);
            }
        }
    }

    int index = -1;
    for (int i = 0; i < deathNeeded.Length; i++)
    {
        if (counter.deathCounter >= deathNeeded[i])
        {
            index = i;
        }
    }

    if (index >= 0 && index < dialogos.Length)
    {
        if (!PlayerPrefs.HasKey("Dialogue_" + index))
        {
            dialogos[index].SetActive(true);
            PlayerPrefs.SetInt("Dialogue_" + index, 1);
        }
    }
}

}
