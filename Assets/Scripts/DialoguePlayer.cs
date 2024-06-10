using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguePlayer : MonoBehaviour
{
    [SerializeField] public GameObject dialogo;
    private bool touching = false;
    public bool resetLineIndex = false;
    void Update()
    {
         if (GetComponentInChildren<Interavel>().canInteract){
       if (Input.GetKeyDown(KeyCode.C))
            {
              if (dialogo.GetComponent<dialogue>() != null){
                 if (dialogo.GetComponent<dialogue>().index ==  dialogo.GetComponent<dialogue>().lines.Length-1){
                      resetLineIndex = true;
                    }
                    dialogo.SetActive(true);
                }else{
                    if (dialogo.GetComponent<dialogue_with_portrait>().index ==  dialogo.GetComponent<dialogue_with_portrait>().lines.Length-1){
                      resetLineIndex = true;
                    }
                    dialogo.SetActive(true);
                }
        }
        }
    }


  void OnCollisionEnter2D(Collision2D col)
  {

        if (col.gameObject.CompareTag("Player"))
        {
               touching = true;
  }
}

  void OnCollisionExit2D(Collision2D col)
  {

        if (col.gameObject.CompareTag("Player"))
        {
               touching = false;
        }

}
}
