using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deck : MonoBehaviour
{
    public List<Card> deck;
    public List<Transform> hand;
    public List<Card> cardBase;
    public int startingHandSize = 5;
    [SerializeField] int maxHandSize = 10;
    [SerializeField] Transform cardObj;
    [SerializeField] Transform handUI;
    [SerializeField] Text deckText;

    [Header("Deck List Feedback")]
    [SerializeField] GameObject deckList;
    [SerializeField] DeckListItem[] deckListItems;

    GameController gc;

    void Awake () 
    {
        gc = GetComponent<GameController>();
        ShuffleDeck();
    }

    public void ShuffleDeck () 
    {
        deck.Clear();
        deck.AddRange(cardBase);
        UpdateDeckText();

        // deck shuffle
        int count = deck.Count;
        int last = count - 1;
        for (int i = 0; i < last; ++i) 
        {
            int r = UnityEngine.Random.Range(i, count);
            var tmp = deck[i];
            deck[i] = deck[r];
            deck[r] = tmp;
        }

        for (int i = 0; i < count; i++) 
        {
            deckListItems[i].gameObject.SetActive(true);
            deckListItems[i].UpdateSelf(deck[i]);
        }
    }

    void UpdateDeckText () => deckText.text = deck.Count.ToString();

//////////////////////// ACCESSING FUNCTIONS ////////////////////////

    public void ClearDeck (Card c)  => deck.Clear();
    public void AddCard (Card c)    => deck.Add(c);

    public void DrawCard ()
    {
        if (hand.Count >= maxHandSize) return;
        if (deck.Count < 1) ShuffleDeck();

        Card drawn = deck[0];
        deck.RemoveAt(0);
        UpdateDeckText();
        AddCardToHand(drawn);

        // first active GameObject is the next card to deactivate
        foreach (DeckListItem dli in deckListItems) 
        {
            if (dli.gameObject.activeSelf) 
            {
                dli.gameObject.SetActive(false);
                break;
            }
        }
    }
    
    public void AddCardToHand (Card c) 
    {
        Transform card = Instantiate(cardObj);
        card.SetParent(handUI, false);
        string updatedDesc = c.desc.Replace("DMG", c.damage.ToString());

        card.GetComponent<CardDisplay>().DisplayInfo(c, c.cost, c.cardName, c.pic, updatedDesc);
        hand.Add(card);
        gc.UpdateHandVisuals();
    }

    public void RemoveCertainCardFromDeck (int i) => deck.RemoveAt(i);
    public void RemoveCertainCardFromHand (int i) 
    {
        //print("remove at " + i);
        hand.RemoveAt(i);
    }

    public void DrawHand () 
    {
        for (int i = 0; i < startingHandSize; i++) DrawCard();
    }

    public void EmptyHand () 
    {
        int childCount = handUI.childCount;
        handUI.DetachChildren();
        foreach (Transform t in hand) Destroy(t.gameObject);

        hand.Clear();
    }

    public void DeckListToggle () 
    {
        deckList.SetActive(!deckList.activeSelf);
    }
}