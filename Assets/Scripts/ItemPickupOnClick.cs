using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickupOnClick : MonoBehaviour
{
    [SerializeField] InventoryItem item;
    [SerializeField] Vector2 amtRange;

    void OnMouseDown ()
    {
        int amt = (int)Random.Range(amtRange.x, amtRange.y);
        Inventory.instance.AddItemToInventory(item, amt);

        Destroy(gameObject);
    }
}
