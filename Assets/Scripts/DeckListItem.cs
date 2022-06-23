using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckListItem : MonoBehaviour
{
    [SerializeField] Text cardName;
    [SerializeField] Image cardPic;

    public void UpdateSelf (Card c)
    {
        cardName.text = c.cardName;
        cardPic.sprite = c.pic;
    }
}
