using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropshipAnim : MonoBehaviour
{
    [SerializeField] Transform spawnPos;
    [SerializeField] Transform player;

    public void Dropship ()
    {
        player.gameObject.SetActive(true);
        player.position = spawnPos.position;
    }
}
