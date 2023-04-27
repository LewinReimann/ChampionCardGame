using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public Deck deck;
    public GameObject cardPrefab;
    public Transform cardParent;
    public int startingHandSize;
    public Hand hand;

    private List<Card> drawPile = new List<Card>();
    private List<Card> discardPile = new List<Card>();
    

    void Start()
    {
        hand = GetComponentInChildren<Hand>();

        // Create a new draw pile by adding all the cards from the deck
        drawPile.AddRange(deck.cards);

        Shuffle();

        // Draw starting hand
        DrawStartingHand();

        foreach (Card card in deck.cards)
        {
            GameObject cardObj = Instantiate(cardPrefab, cardParent);
            CardDisplay cardDisplay = cardObj.GetComponent<CardDisplay>();
            cardDisplay.card = card;
        }
    }

    private void Shuffle()
    {
        System.Random rnd = new System.Random();

        // Iterate through all the cards in the draw pile and shuffle them
        for (int i = drawPile.Count - 1; i > 0 ; i--)
            { 
            int j = rnd.Next(i + 1);
            Card temp = drawPile[i];
            drawPile[i] = drawPile[j];
            drawPile[j] = temp;
            }
    }

    private void DrawStartingHand()
    {
        for (int i = 0; i < startingHandSize; i++)
        {
            DrawCard();
        }
    }

    private Card DrawCard()
    {
        // Check if the draw pile is empty and huffle the discarded pile back
        if (drawPile.Count == 0)
        {
            drawPile.AddRange(discardPile);
            discardPile.Clear();
            Shuffle();
        }

        // Draw the top card from the draw pile and add it to the hand
        Card drawnCard = drawPile[0]; // draws the first card of the List so the TopDeck
        drawPile.RemoveAt(0); // Removes that Card from the List so that it is no longer in the Deck.
        hand.AddCard(drawnCard);

        // Add the drawon card to the hands display
        int index = hand.GetCards().Count - 1;
        hand.AddCardToHandDisplay(drawnCard, index);

        return drawnCard;
    }

}
