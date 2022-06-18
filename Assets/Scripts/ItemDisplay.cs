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
    public int itemAmt = 1;

    public void UpdateItemDisplay (InventoryItem invItem)
    {
        invenItem = invItem;
        itemImage.sprite = invItem.pic;
        itemNameText.text = invItem.inventoryItemName;
        UpdateItemCount();
    }

    public void UpdateItemCount () 
    {
        itemQuantityText.text = itemAmt.ToString();
    }
}
