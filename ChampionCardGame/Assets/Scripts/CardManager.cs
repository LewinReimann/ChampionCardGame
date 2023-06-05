using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public GameObject cardPrefab;
    public Deck currentDeck;
    public PlayerHandLayout playerHandLayout;

    // These lists hold the actual card data.
    public List<Card> deck;
    public List<Card> hand = new List<Card>();
    public List<Card> field = new List<Card>();
    public List<Card> grave = new List<Card>();
    public List<Card> banished = new List<Card>();


    private void Start()
    {
        InitializeDeck();
    }

    public void InitializeDeck()
    {
        // Copy the list from the chosen Deck object
        deck = new List<Card>(currentDeck.cards);

        ShuffleDeck();
    }

    public void MoveCard(Card card, Card.CardLocation from, Card.CardLocation to)
    {
        // Find the appropriate lits for "from" and "to".
        List<Card> fromList = GetListFromLocation(from);
        List<Card> toList = GetListFromLocation(to);

        if (fromList.Contains(card))
        {
            // Remove the card from the "from" list.
            fromList.Remove(card);

            // Add the card to the "to" List.
            toList.Add(card);
        }
        else
        {
            Debug.LogError($"Card not found in {from}.");
        }
    }

    private List<Card> GetListFromLocation(Card.CardLocation location)
    {
        switch (location)
        {
            case Card.CardLocation.Deck: return deck;
            case Card.CardLocation.Hand: return hand;
            case Card.CardLocation.Field: return field;
            case Card.CardLocation.Graveyard: return grave;
            case Card.CardLocation.Banished: return banished;

            default:
                Debug.LogError($"Invalid location: {location}.");
                return null;
        }
    }

    public void ShuffleDeck()
    {
        System.Random rng = new System.Random();
        int n = deck.Count;

        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            Card value = deck[k];
            deck[k] = deck[n];
            deck[n] = value;
        }
    }

    public void DrawCard()
    {
        if (deck.Count > 0)
        {
            // Take the top card from the deck
            Card topCard = deck[0];

            // Instantiate the card prefab
            GameObject cardObject = Instantiate(cardPrefab);
            cardObject.transform.localScale = new Vector3(1f, 1f, 1f); // Set the cards scale

            // Get the CardDisplay component and set its card to the topCard
            CardDisplay cardDisplay = cardObject.GetComponent<CardDisplay>();
            cardDisplay.card = topCard;

            // Move the card data from the deck to the hand
            MoveCard(topCard, Card.CardLocation.Deck, Card.CardLocation.Hand);

            // Get the players hand layout and add a card slot
            playerHandLayout.AddCardToHand(cardObject);

            // Set the cards parent to the last slot in the hand
            // cardObject.transform.SetParent(playerHandLayout.cardSlots[playerHandLayout.cardSlots.Count - 1]);
        }
        else
        {
            Debug.LogWarning("Deck is empty. Cannot draw a card.");
        }
        
    }
}
