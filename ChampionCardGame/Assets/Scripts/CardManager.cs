using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public List<Card> allCards = new List<Card>();
    public List<Card> onHand = new List<Card>();
    public List<Card> onBoard = new List<Card>();
    public List<Card> graveyard = new List<Card>();

    public List<CardDisplay> cardsInPlay = new List<CardDisplay>();

    
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
        // Find all CardDisplay components in the scene
        CardDisplay[] cardDisplays = FindObjectsOfType<CardDisplay>();

        // Loop through all the CardDisplays components
        foreach (CardDisplay cardDisplay in cardDisplays)
        {
            // Check if the card is in the onBoard list
            if (onBoard.Contains(cardDisplay.card))
            {
                // Destroy the cards GameObject and remove it from the onBoard List
                Destroy(cardDisplay.gameObject);
                onBoard.Remove(cardDisplay.card);
            }
        }
        // Find all GameObjects in the scene that have a Slot component
        Slot[] slots = FindObjectsOfType<Slot>();

        // Loop through all the slots and set isOccupied to false
        foreach (Slot slot in slots)
        {
            slot.isOccupied = false;
        }
    }
}
