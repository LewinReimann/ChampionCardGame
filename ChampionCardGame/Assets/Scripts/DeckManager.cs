using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DeckManager : MonoBehaviour
{
    public Deck defaultDeck;
    public Deck customDeck;
    private Deck activeDeck;

    public Transform cardParent;
    public int startingHandSize;
    public Hand hand;

    private List<Card> drawPile = new List<Card>();
    private List<Card> discardPile = new List<Card>();
    

    void Awake()
    {
        if (DeckController.Instance != null && DeckController.Instance.customDeck != null && DeckController.Instance.customDeck.cards.Count > 0)
        {
            activeDeck = DeckController.Instance.customDeck;
        }
        else
        {
            activeDeck = defaultDeck;
        }

        // Create a new draw pile by adding all the cards from the deck
        drawPile.AddRange(activeDeck.cards);

    }

    void Start()
    {

     
        Shuffle();

        // Draw starting hand
        DrawStartingHand();

  
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

    public Card DrawCard()
    {
    

        // Draw the top card from the draw pile and add it to the hand
        Card drawnCard = drawPile[0]; // draws the first card of the List so the TopDeck
        drawPile.RemoveAt(0); // Removes that Card from the List so that it is no longer in the Deck.
        hand.AddCard(drawnCard);
        
        return drawnCard;
    }

}
