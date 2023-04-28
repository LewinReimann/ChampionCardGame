using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public string cardName;

    public Sprite cardArtwork;

    public string championEffect;
    public string secondaryEffect;

    public int health;

    public bool inPlay { get; set; } = false; // Indicates whether the card is in play or not
    public CardLocation location { get; set; } = CardLocation.Deck;

}

public enum CardLocation
{
    Deck,
    Hand,
    Champion,
    Secondary,
    Graveyard
}
