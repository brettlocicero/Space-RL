using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    void OnTriggerEnter2D (Collider2D col)
    {
        if (col.CompareTag("Player")) 
        {
            Destroy(gameObject);
        }
    }
}
