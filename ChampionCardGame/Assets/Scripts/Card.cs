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
        WhenSpecialDamageReceived,
        WhenNormalDamageReceived,
        WhenSpecialDamageDealt,
        WhenNormalDamageDealt,
        WhenUnitSummoned,
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
    public List<int> summonCardIndices;

    public TriggerTypes secondaryTrigger;
    public EffectTypes secondaryEffectType;
    public int secondaryEffectValue;
    public List<int> secondarySummonCardIndices;

    public string cardName;
    public Sprite cardArtwork;
    public string championEffectText;
    public string secondaryEffectText;

    public int health;
    public CardLocation location { get; set; } = CardLocation.Deck;

    public void ChangeToSecondary()
    {
        trigger = secondaryTrigger;
        effect = secondaryEffectType;
        effectValue = secondaryEffectValue;
        summonCardIndices = secondarySummonCardIndices;
    }
}
