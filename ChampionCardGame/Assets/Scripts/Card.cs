using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
[System.Serializable]
public class Card : ScriptableObject
{
    public GameObject GameObject;

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

    public enum TriggerTypes
    {
        WheneverCardDrawn,
        WheneverCardPlayed,
        CardPlayed,
        WhenDamageReceived,
        WhenHealingReceived,
        WhenDestroyed,
        WhenTurnEnd,
        WhenBattlePhaseEnd,
        WhenSpellCasted
    }

    public enum EffectTypes
    {
        DrawCard,
        ScoutCard,
        DealDamage,
        Heal,
        RollPlus,
        Summon,
        SearchFor,
        CastCard,
        DestroySecondary,
        DestroyTopDeck,
        Revive,
        Spellshield
    }

    public CardType type;
    public CardFaction faction;
    public TriggerTypes trigger;
    public EffectTypes effect;
    public int effectValue;

    public string cardName;
    public Sprite cardArtwork;
    public string championEffect;
    public string secondaryEffect;

    public int health;
    public CardLocation location { get; set; } = CardLocation.Deck;

}
