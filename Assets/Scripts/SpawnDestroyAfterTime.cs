using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDestroyAfterTime : MonoBehaviour
{
    [SerializeField] GameObject fx;
    [SerializeField] float delay = 2f;
    [SerializeField] GameObject[] objs;

    void Start ()
    {
        Invoke("Trigger", delay);
    }

    void Trigger () 
    {
        Instantiate(fx, transform.position, transform.rotation);
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        if (sr) sr.enabled = false;
        foreach(GameObject obj in objs) 
        {
            obj.SetActive(false);
        }
    }
}
