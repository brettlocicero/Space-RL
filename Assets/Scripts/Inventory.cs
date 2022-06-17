using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] Animator inven;
    bool open;
    [SerializeField] InventoryItem[] allItems;

    [SerializeField] Dictionary<InventoryItem, int> items = new Dictionary<InventoryItem, int>();

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
        if (items.ContainsKey(item)) 
        {
            items[item] += 1;
        }

        else 
        {
            items[item] = 1;
        }

        print(items);
    }
}