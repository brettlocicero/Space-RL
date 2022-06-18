using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemNotification : MonoBehaviour
{
    [SerializeField] Text notifText;
    [SerializeField] Image img;

    public void DisplayItemNotification (Sprite pic, string name, int amt = 1)
    {
        img.sprite = pic;
        notifText.text = amt + " - " + name;
    }
}
