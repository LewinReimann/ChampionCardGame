using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public List<Card> allCards = new List<Card>();
    public List<Card> onHand = new List<Card>();
    public List<Card> onBoard = new List<Card>();
    public List<Card> graveyard = new List<Card>();

    private List<CardDisplay> cardsInPlay = new List<CardDisplay>();

    
    public void AddCardToHand(Card card)
    {
        onHand.Add(card);
    }

    public void RemoveCardFromHand(Card card)
    {
        onHand.Remove(card);
    }

    public void AddCardToPlay(Card card)
    {
        onBoard.Add(card);
    }

    public void RemoveCardFromPlay(Card card)
    {
        onBoard.Remove(card);
    }

    public void AddCardToGraveyard(Card card)
    {
        graveyard.Add(card);
    }

    public void RemoveCardFromGraveyard(Card card)
    {
        graveyard.Remove(card);
    }

    public void ClearBoard()
    {
        // Find all GameObjects in the scene that have a CardDisplay component
        CardDisplay[] cardDisplay = FindObjectsOfType<CardDisplay>();

        // Loop through all the CardDisplays and add those that have isInPlay = true and add them to a List
        foreach (CardDisplay cd in cardDisplay)
        {
            if (cd.isInPlay)
            {
                cardsInPlay.Add(cd);
            }
        }

        // Loop through all the CardDisplays that have isInPlay = true and destroy them
        foreach(CardDisplay cd in cardsInPlay)
        {
            Destroy(cd.gameObject);
        }

        // Clear the cardsInPlay list
        cardsInPlay.Clear();

        // Find all GameObjects in the scene that have a Slot component
        Slot[] slots = FindObjectsOfType<Slot>();

        // Loop through all the slots and set isOccupied to false
        foreach (Slot slot in slots)
        {
            slot.isOccupied = false;
        }
    }
}
