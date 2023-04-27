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

    private List<GameObject> cards = new List<GameObject>(); // The Cards in the Hand

    public void AddCard(Card card)
    {
        // Create anew GameObject for the card and set its CardDisplay Component
        // to display the given card.
        GameObject cardObj = Instantiate(cardPrefab, cardParent);
        CardDisplay cardDisplay = cardObj.GetComponent<CardDisplay>();
        cardDisplay.card = card;

        // Debug.Log statements to check the values of variables
        Debug.Log("Cards count before adding card: " + cards.Count);
        Debug.Log("Card slot index: " + cards.Count);

        // Set the cards parent to one of the card slots in the hand.
        cardObj.transform.SetParent(cardSlots[cards.Count], false);

        // Debug.Log statement to check if the card object is null
        Debug.Log("Card object: " + cardObj);

        // Add the card to the list of cards in the hand
        cards.Add(cardObj);

        // Debug.Log statement to check the number of cards in the hand after adding the new card
        Debug.Log("Cards count after adding card: " + cards.Count);
    }

    public void RemoveCard(Card card)
    {
        GameObject cardObj = cards.Find(c => c.GetComponent<CardDisplay>().card == card);
        cards.Remove(cardObj);
        Destroy(cardObj);
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
        float spacing = Mathf.Clamp(2f / (cards.Count + 1), 0.3f, 1f);
        Vector3 position = Vector3.right * (index - (cards.Count - 1) / 2f) * spacing;

        // Display the cards by giving them the Prefab to feed.
        GameObject cardObj = Instantiate(cardPrefab, cardParent);
        cardObj.transform.SetParent(transform.GetChild(index), false);
        CardDisplay cardDisplay = cardObj.GetComponent<CardDisplay>();
        cardDisplay.card = card;
    }
}
