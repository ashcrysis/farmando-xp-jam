using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deleteOnContactToLayer : MonoBehaviour
{
    public LayerMask destroyOnTouchingLayer;


  void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & destroyOnTouchingLayer) != 0)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
