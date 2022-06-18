using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{    
    public static Inventory instance;
    [SerializeField] InventoryItem[] allItems;

    [Header("")]
    [SerializeField] Animator inven;
    [SerializeField] Transform invenItemsParent;
    bool open;
    [SerializeField] ItemDisplay baseItemDisplay;

    [Header("")]
    [SerializeField] List<ItemDisplay> items;

    [Header("")]
    [SerializeField] Transform notifParent;
    [SerializeField] Transform itemNotif;

    void Awake () 
    {
        instance = this;
    }

    void Start () 
    {
        //TestInventory();
        //TestInventory();
    }

    void TestInventory () 
    {
        // simple test that adds every item of game to inventory
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

    public void AddItemToInventory (InventoryItem item, int amt = 1) 
    {
        for (int x = 0; x < amt; x++) 
        {
            bool found = false;
            foreach (ItemDisplay i in items) 
            {
                if (i.invenItem == item) 
                {
                    i.itemAmt += 1;
                    i.UpdateItemCount();

                    found = true;
                }
            }

            if (found) continue;

            Transform itemObj = Instantiate(baseItemDisplay.transform, Vector3.zero, Quaternion.identity);
            itemObj.transform.SetParent(invenItemsParent);
            itemObj.localScale = Vector3.one;

            ItemDisplay itemDisplay = itemObj.GetComponent<ItemDisplay>();
            items.Add(itemDisplay);
            itemDisplay.UpdateItemDisplay(item);
        }

        SpawnNotification(item, amt);
    }

    void SpawnNotification (InventoryItem item, int amt = 1) 
    {
        Transform notifObj = Instantiate(itemNotif, Vector3.zero, Quaternion.identity);
        notifObj.transform.SetParent(notifParent);
        notifObj.localScale = Vector3.one;
        notifObj.GetComponent<ItemNotification>().DisplayItemNotification(item.pic, item.name, amt);

        Destroy(notifObj.gameObject, 8f);
    }
}