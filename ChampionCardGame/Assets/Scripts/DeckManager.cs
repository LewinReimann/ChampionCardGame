using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public List<Card> deck;
    public GameObject cardPrefab;
    public Transform cardParent;

    void Start()
    {
        foreach (Card card in deck)
        {
            GameObject cardObj = Instantiate(cardPrefab, cardParent);
            CardDisplay cardDisplay = cardObj.GetComponent<CardDisplay>();
            cardDisplay.card = card;
        }
    }
}
