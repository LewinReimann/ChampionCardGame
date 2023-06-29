using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardEffect
{
    public Card.EffectTypes Effect;
    public Card.TriggerTypes Trigger;
    public int Value;
    public List<int> SummonCardIndices;
}

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
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

    public CardEffect PrimaryEffect;
    public CardEffect SecondaryEffect;

    public CardType type;
    public CardFaction faction;
    
    public string cardName;
    public Sprite cardArtwork;
    public string championEffect;
    public string secondaryEffectText;

    public int health;
    public CardLocation location { get; set; } = CardLocation.Deck;

}
