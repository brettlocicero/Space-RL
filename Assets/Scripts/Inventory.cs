using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] Animator inven;
    [SerializeField] Transform invenItemsParent;
    bool open;
    [SerializeField] InventoryItem[] allItems;
    [SerializeField] ItemDisplay baseItemDisplay;

    [SerializeField] List<ItemDisplay> items;
    HashSet<InventoryItem> seenItems = new HashSet<InventoryItem>();

    void Start () 
    {
        TestInventory();
        TestInventory();
    }

    void TestInventory () 
    {
        foreach (InventoryItem item in allItems) 
        {
            AddItemToInventory(item);
        }
    }

    void Update ()
    {
        // press tab and inventory is not open, OPEN
        if (Input.GetKeyDown(KeyCode.Tab) && !open) OpenInventory();

        // else press tab and open, CLOSE
        else if (Input.GetKeyDown(KeyCode.Tab)) CloseInventory();
    }

    void OpenInventory () 
    {
        open = true;
        inven.SetTrigger("Open");
    }

    void CloseInventory () 
    {
        open = false;
        inven.SetTrigger("Close");
    }

    void AddItemToInventory (InventoryItem item) 
    {
        if (seenItems.Contains(item)) 
        {
            foreach (ItemDisplay i in items) 
            {
                if (i.invenItem == item) 
                {
                    i.itemAmt += 1;
                    break;
                }
            }
        }

        else 
        {
            Transform itemD = Instantiate(baseItemDisplay.transform, Vector3.zero, Quaternion.identity);
            itemD.transform.SetParent(invenItemsParent);
            itemD.localScale = Vector3.one;

            seenItems.Add(item);
            items.Add(itemD.GetComponent<ItemDisplay>());
            itemD.GetComponent<ItemDisplay>().UpdateItemDisplay(item);
        }
    }
}