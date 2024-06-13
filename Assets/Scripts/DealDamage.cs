using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DealDamage : MonoBehaviour
{
    private bool isColliding = false;
    private Collider2D collidingObject;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("enemy"))
        {
            isColliding = true;
            collidingObject = other;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("enemy"))
        {
            isColliding = false;
            collidingObject = null;
        }
    }

    private void Update()
    {
        if (isColliding && collidingObject != null)
        {
            if (collidingObject.CompareTag("Player"))
            {
                if (!collidingObject.gameObject.GetComponent<Dash>().invincible)
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<DeathCounter>().Die();
                }
            }

            if (gameObject.name == "SDamage")
            {
                if (collidingObject.CompareTag("enemy"))
                {
                    Destroy(collidingObject.gameObject);
                }
            }
        }
    }
}
