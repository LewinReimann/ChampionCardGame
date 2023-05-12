using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
[System.Serializable]
public class Card : ScriptableObject
{

    public EffectCondition effectCondition;
    public EffectAction effectAction;

    public string cardName;

    public Sprite cardArtwork;

    public string championEffect;
    public string secondaryEffect;

    public int health;
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

public enum EffectCondition
{
    None,
    OnRoll5, 
    OnReceiveEffectDamage,
        // Add more conditions here
}

public enum EffectAction
{
    None,
    Deal1DamageToOpponent,
    Heal1
        // Add more actions here
}
