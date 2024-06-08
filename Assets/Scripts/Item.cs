using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class Item : MonoBehaviour
{
   public string nome ;
   public Item(string nome)
   {
    this.nome = nome;
   }
}
