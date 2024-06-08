using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class autoDialogue : MonoBehaviour
{
    [SerializeField] private GameObject dialogo;
    [SerializeField]private bool touching = false;
    [SerializeField]private bool touchedOnce = false;
    void Update()
    {
         if (touching){
       
                 if (!touchedOnce){
                    //dialogo.GetComponent<dialogue>().index = 0;
                    touchedOnce = true;
                    
                    dialogo.SetActive(true);
                 }
        }
    }


  void OnTriggerEnter2D(Collider2D col)
  {
 
        if (col.gameObject.CompareTag("Player"))
        {
               touching = true;
  }
}

  void OnTriggerExit2D(Collider2D col)
  {
 
        if (col.gameObject.CompareTag("Player"))
        {
               touching = false;
        }       

}
}
