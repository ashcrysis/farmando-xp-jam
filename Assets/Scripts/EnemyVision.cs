using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    public bool foundPlayer = false;
  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.CompareTag("Player")){
        foundPlayer = true;
    }
  }
  void OnTriggerExit2D(Collider2D other)
  {
    if (other.gameObject.CompareTag("Player")){
        foundPlayer = false;
    }
  }
}
