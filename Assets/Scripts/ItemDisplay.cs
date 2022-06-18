using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour
{
    [SerializeField] Text itemNameText;
    [SerializeField] Text itemQuantityText;
    [SerializeField] Image itemImage;

    [Header("")]
    public InventoryItem invenItem;
    public int itemAmt;

    public void UpdateItemDisplay (InventoryItem invItem)
    {
        invenItem = invItem;
        itemImage.sprite = invItem.pic;
        itemNameText.text = invItem.inventoryItemName;
    }
}
