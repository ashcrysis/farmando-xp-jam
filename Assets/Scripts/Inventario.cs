using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventario : MonoBehaviour
{
    [SerializeField] public List<Item> itensJogador;
    [SerializeField] public Dictionary<string, int> itemCounts = new Dictionary<string, int>();

    void Start()
    {
        StartCoroutine(delay(1f)); // wait for itens to initialize, otherwise it will print NULL
        CheckItemsTypesAndQuantities();
    }
 public void AddItem(Item item)
    {
        itensJogador.Add(item);
        CheckItemsTypesAndQuantities();
    }

 public void RemoveItem(string itemType, int quantity = 1)
    {
        int itemsRemoved = 0;

        for (int i = itensJogador.Count - 1; i >= 0; i--) // iterate backwards to avoid index issues
        {
            if (itensJogador[i].nome == itemType)
            {
                itensJogador.RemoveAt(i);
                itemsRemoved++;

                if (itemsRemoved == quantity)
                {
                    break;
                }
            }
        }

        CheckItemsTypesAndQuantities();

    }
public void CheckItemsTypesAndQuantities()
{
    itemCounts.Clear();
    for (int i = 0; i < itensJogador.Count; i++)
    {
        UpdateItemCount(itemCounts, itensJogador[i].nome);

    }
}



private void UpdateItemCount(Dictionary<string, int> itemCounts, string itemType)
{
    if (!itemCounts.ContainsKey(itemType))
    {
        itemCounts.Add(itemType, 0);
    }

    itemCounts[itemType]++;
}
       private IEnumerator delay(float delay)
    {
        yield return new WaitForSeconds(delay);
    }
}
