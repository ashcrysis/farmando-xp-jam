using System.Collections;
using UnityEngine;

public class OnSecondTouchAfterDelay : MonoBehaviour
{
    private bool firstTouchDetected = false;
    private float timeSinceFirstTouch = 0f;
    private bool leftTouch;
    public float delay = 1.4f;

 void OnTriggerEnter2D(Collider2D other)
 {
     if (firstTouchDetected)
        {
           if (leftTouch){
                GetComponent<autoDialogue>().enabled = true;
           }
        }
        else
        {
            Debug.Log("First touch detected. Waiting for the second touch.");
            firstTouchDetected = true;
            timeSinceFirstTouch = Time.time;
        }
 }
  void OnTriggerExit2D(Collider2D other)
 {
    leftTouch = true;
 }

}
