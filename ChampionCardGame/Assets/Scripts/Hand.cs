using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    private List<Card> cards = new List<Card>();

    public void AddCard(Card card)
    {
        cards.Add(card);
    }

    public void RemoveCard(Card card)
    {
        cards.Remove(card);
    }

    public List<Card> GetCards()
    {
        return cards;
    }

    public void Clear()
    {
        cards.Clear();
    }
}
