using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeckBuilder : MonoBehaviour
{

    // All of our cards we have should go into this List or rather if they are available to the player? 
    public List<Card> availableCards;
    public GameObject cardLibraryItemPrefab;
    public GameObject cardLibraryContent;

    public GameObject deckContent;

    public Deck currentDeck;

    public TextMeshProUGUI deckSizeText;

    public static DeckBuilder Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        CreateNewDeck();
        PopulateCardLibrary();
        UpdateDeckSizeText();
    }

    private void CreateNewDeck()
    {
        currentDeck = ScriptableObject.CreateInstance<Deck>();
        currentDeck.cards = new List<Card>();
        currentDeck.deckName = "New Deck";
    }

    private void PopulateCardLibrary()
    {

        foreach (Card card in availableCards)
        {
            // Instantiate a CardLibraryItem prefab for each available card
            GameObject cardItem = Instantiate(cardLibraryItemPrefab, cardLibraryContent.transform);

            // Get the CardDisplayUI Component from the instantiated item
            CardDisplayUI cardDisplay = cardItem.GetComponent<CardDisplayUI>();

            // Set the card data for this item
            cardDisplay.card = card;

            // Get the Button component from the instantiated item
            Button button = cardItem.GetComponent<Button>();

            // Set the OnClick event for the button component to call AddCardToDeck
            button.onClick.AddListener(() => AddCardToDeck(card));
        }
    }

    public void AddCardToDeck(Card card)
    {
        Debug.Log("Adding card to deck: " + card.cardName);
        currentDeck.cards.Add(card);

        // Instantiate a new card item for the deck
        GameObject cardItem = Instantiate(cardLibraryItemPrefab, deckContent.transform);

        // Get the CardDisplayUI Component from the instantiated item
        CardDisplayUI cardDisplay = cardItem.GetComponent<CardDisplayUI>();

        // Set the card data for this item
        cardDisplay.card = card;

        //Update Decksize Text
        UpdateDeckSizeText();
    }

    public void SaveDeck()
    {
        DeckController.Instance.customDeck = currentDeck;
    }

    private void UpdateDeckSizeText()
    {
        deckSizeText.text = currentDeck.cards.Count.ToString() + "/30";
    }
}
