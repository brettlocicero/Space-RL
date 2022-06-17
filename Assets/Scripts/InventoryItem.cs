using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/InventoryItem")]
public class InventoryItem : ScriptableObject
{
    public string inventoryItemName;
    public Sprite pic;
    public int id;
}
