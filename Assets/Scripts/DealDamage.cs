using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DealDamage : MonoBehaviour
{
   private bool isColliding = false;
    private Collider2D collidingObject;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isColliding = true;
            collidingObject = other;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isColliding = false;
            collidingObject = null;
        }
    }

    private void Update()
    {
        if (isColliding && collidingObject != null && !collidingObject.gameObject.GetComponent<Dash>().invincible)
        {
            collidingObject.GetComponent<DeathCounter>().Died();
            SceneManager.LoadScene(0);
        }
    }
}
