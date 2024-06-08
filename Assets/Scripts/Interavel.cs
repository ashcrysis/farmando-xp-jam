using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interavel : MonoBehaviour
{
    [SerializeField]private GameObject botao;
    [SerializeField]private float minDistance;
    public bool canInteract;
    [SerializeField] private string neededItem = "";
    [SerializeField] private int neededItemAmount = 0;

    void Update()
    {

        canInteract = Vector2.Distance(transform.parent.position,GameObject.FindGameObjectWithTag("Player").transform.position) < minDistance;
        try{
        if (neededItem == "")
        {
                botao.SetActive(canInteract);
        }
        if (neededItem != "")
        {
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<Inventario>().itemCounts[neededItem] >= neededItemAmount)
            {
                botao.SetActive(canInteract);
            }
        }
        }catch (Exception) { }
    }
    public void SetBotao(bool situation)
    {
        this.botao.SetActive(situation);
    }
}
