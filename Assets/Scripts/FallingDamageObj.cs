using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingDamageObj : MonoBehaviour
{
  public AudioSource fallingSource;
  private bool onlyPlayOnce = true;
  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Player"))
    {
        GetComponentInParent<Rigidbody2D>().gravityScale = 5;
        if (onlyPlayOnce){
        fallingSource.Play();
        onlyPlayOnce = false;  
      }
    }
  }
}
