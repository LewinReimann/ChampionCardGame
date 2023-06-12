using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
[System.Serializable]
public class Card : ScriptableObject
{
    public GameObject GameObject;

    public CardTrigger trigger;
    public CardEffect effect;

    // Enum to define card types
    public enum CardType
    {
        Champion,
        Spell,
        Event,
        Other
    }

    public enum CardLocation
    {
        Deck,
        Hand,
        Field,
        Graveyard,
        Banished
    }

    public enum CardFaction
    {
        None,
        Ancient,
        Elve
    }

    public CardType type;
    public CardFaction faction;

    public string cardName;
    public Sprite cardArtwork;
    public string championEffect;
    public string secondaryEffect;

    public int health;
    public CardLocation location { get; set; } = CardLocation.Deck;

    // Event Logics

    public List<ICardTrigger> triggers = new List<ICardTrigger>();
    public List<ICardEffect> effects = new List<ICardEffect>();
}
