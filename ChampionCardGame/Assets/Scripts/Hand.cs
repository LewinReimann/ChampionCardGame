using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Hand : MonoBehaviour
{
    public GameObject cardPrefab;
    [SerializeField]
    public Transform cardParent;
    public Transform[] cardSlots; // The fice slots in the hand
    public HandLayout handLayout;
    public Round round;
    public CardManager cardManager;

    public List<GameObject> cards = new List<GameObject>(); // The Cards in the Hand
    

    public void AddCard(Card card)
    {

        // handLayout.ChildAdded();
            handLayout.AddCardSlot();
        

        // Set the initial location o the card to Hand
        card.location = CardLocation.Hand;


        // Instantiate a new GameObject for the card and set its CardDisplay Component
        // to display the given card.
        GameObject cardObj = Instantiate(cardPrefab, cardParent);
        CardDisplay cardDisplay = cardObj.GetComponent<CardDisplay>();
        cardDisplay.card = card;

        // Set the cards parent to one of the card slots in the hand.
        int index = cards.Count;
        cardObj.transform.SetParent(transform.GetChild(cards.Count), false);

        // Add the card to the list of cards in the hand
        cards.Add(cardObj);
        cardManager.AddCardToHand(card);

        // Update the hand layout
        handLayout.UpdateLayout();
    }


    public void PlayCard(Card card)
    {
        // Remove all empty slots from our Hand GameObject List
        cards.RemoveAll(item => item == null);

        cardManager.RemoveCardFromHand(card);
        cardManager.AddCardToPlay(card);

        // Find the card object before removing it
        GameObject foundCardObj = cards.Find(c => c.GetComponent<CardDisplay>().card == card);
        cards.Remove(foundCardObj);

        handLayout.RemoveEmptyCardSlot();

        // Add Card to a cardsInPlay List
        if (foundCardObj != null)
        {
            CardDisplay cardDisplayInstance = foundCardObj.GetComponent<CardDisplay>();
            cardManager.cardsInPlay.Add(cardDisplayInstance);
        }
        else
        {
            Debug.LogError("Card not found in Hand.");
        }


    }
    public List<Card> GetCards()
    {
        return cards.Select(c => c.GetComponent<CardDisplay>().card).ToList();
    }

   

    public void Clear()
    {
        // Remove and destroy all cards in the hand.
        foreach (GameObject cardObj in cards)
        {
            Destroy(cardObj);
        }
        cards.Clear();
    }

    public void AddCardToHandDisplay(Card card, int index)
    {
        // This will to the spacing depending on how many cards are in the hands
        // float spacing = Mathf.Clamp(2f / (cards.Count + 1), 0.3f, 1f);
        // Vector3 position = Vector3.right * (index - (cards.Count - 1) / 2f) * spacing;

       // Debug.Log("Adding card " + card.cardName + " to hand at index " + index + " with spacing " + spacing + " and position " + position);

        // Display the cards by giving them the Prefab to feed.
        GameObject cardObj = Instantiate(cardPrefab, cardParent);
        cardObj.transform.SetParent(transform.GetChild(index), false);
        CardDisplay cardDisplay = cardObj.GetComponent<CardDisplay>();
        cardDisplay.card = card;
    }
}
