using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
[System.Serializable]
public class Card : ScriptableObject
{
    // Enum to define card types
    public enum CardType
    {
        Champion,
        Spell,
        Event
    }

    public enum CardLocation
    {
        Deck,
        Hand,
        Field,
        Graveyard,
        Banished
    }

    public CardType type;

    public string cardName;
    public Sprite cardArtwork;
    public string championEffect;
    public string secondaryEffect;

    public int health;
    public CardLocation location { get; set; } = CardLocation.Deck;

}
