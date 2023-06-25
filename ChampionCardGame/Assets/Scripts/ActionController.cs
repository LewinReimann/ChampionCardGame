using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class ActionController : MonoBehaviour
{
    public GameManager gameManager;
    public EventManager eventManager;
    public CardManager playerCardManager;
    public CardManager opponentCardManager;

    public static ActionController instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

    }

    private void SubscribeToTrigger(Card.TriggerTypes triggerType)
    {
        EventManager.Instance.Subscribe(triggerType, () => HandleEvent(triggerType));
    }

    private void Start()
    {
        // Initialize effect handlers dictionary
        InitializeEffectHandlers();

        // Subscribe to all events
        foreach (Card.TriggerTypes triggerType in System.Enum.GetValues(typeof(Card.TriggerTypes)))
        {
            SubscribeToTrigger(triggerType);
            Debug.Log("Subscribed to all events uwu");
        }
    }

    public void ActionDrawCard(int playerIndex, int drawAmount)
    {
        // Determine the appropriate card manager based on the playerIndex
        CardManager appropriateCardManager = (playerIndex == 0) ? playerCardManager : opponentCardManager;

        // Call the DrawCard method drawAmount times
        for (int i = 0; i < drawAmount; i++)
        {
            appropriateCardManager.DrawCard();
        }
    }

    public void ActionScoutCard(int playerIndex, int damageAmount)
    {
        Debug.Log("Scout Effect");
    }

    public void ActionDealDamage(int playerIndex, int damageAmount)
    {
        gameManager.ChampionDealDamage(playerIndex, damageAmount);
    }

    public void ActionHeal(int playerIndex, int healAmount)
    {
        gameManager.ChampionHeal(playerIndex, healAmount);
    }

    public void ActionRollPlus(int playerIndex, int plusAmount)
    {
        Debug.Log("RollPlus Effect");
    }

    public void ActionSummon(int playerIndex, int summonAmount)
    {
        Debug.Log("Summon Effect");
    }

    public void ActionSearchFor(int playerIndex, int searchAmount)
    {
        Debug.Log("SearchFor Effect");
    }

    public void ActionCastCard(int playerIndex, int castAmount)
    {
        Debug.Log("Cast Effect");
    }

    public void ActionDestroySecondary(int playerIndex, int destroyedAmount)
    {
        Debug.Log("DestroySecondary Effect");
    }

    public void ActionDestroyTopDeck(int playerIndex, int destroyedAmount)
    {
        Debug.Log("TopDeck Effect");
    }

    public void ActionRevive(int playerIndex, int revivedAmount)
    {
        Debug.Log("Revive Effect");
    }

    public void ActionSpellshield(int playerIndex, int spellshieldAmount)
    {
        Debug.Log("SpellShield Effect");
    }

    // LOGIQ AND CARD HANDLING LISTS EVENTS QUEUE AND SO ON

    private Dictionary<Card.EffectTypes, Action<int, int>> effectHandlers;

    private void InitializeEffectHandlers()
    {
        effectHandlers = new Dictionary<Card.EffectTypes, Action<int, int>>
        {
            { Card.EffectTypes.DrawCard, (playerIndex, value) => ActionDrawCard(playerIndex, value) },
            { Card.EffectTypes.DealDamage, ActionDealDamage },
            { Card.EffectTypes.Heal, ActionHeal },
            { Card.EffectTypes.RollPlus, ActionRollPlus },
            { Card.EffectTypes.Summon, ActionSummon },
            { Card.EffectTypes.SearchFor, ActionSearchFor },
            { Card.EffectTypes.CastCard, ActionCastCard },
            { Card.EffectTypes.DestroySecondary, ActionDestroySecondary },
            { Card.EffectTypes.DestroyTopDeck, ActionDestroyTopDeck },
            { Card.EffectTypes.Revive, ActionRevive },
            { Card.EffectTypes.Spellshield, ActionSpellshield }
        };
    }

    public void HandleEvent(Card.TriggerTypes triggerType)
    {
        // Check championCards, spellCards, and eventCards for matching triggertypes
        foreach (var playedCard in championCards.Concat(spellCards).Concat(eventCards))
        {
            if (playedCard.TriggerType == triggerType)
            {
                ExecuteEffect(playedCard.Effect, playedCard.PlayerIndex, playedCard.EffectValue);
            }
        }
    }

    public void ExecuteEffect(Card.EffectTypes effectType, int playerIndex, int value)
    {
        // Execute the appropriate effect handler
        if (effectHandlers.ContainsKey(effectType))
        {
            effectHandlers[effectType].Invoke(playerIndex, value);
        }
        else
        {
            Debug.Log("Effect handler not found for effect type: " + effectType.ToString());
        }
    }

    public List<PlayedCardInfo> championCards = new List<PlayedCardInfo>();
    public List<PlayedCardInfo> spellCards = new List<PlayedCardInfo>();
    public List<PlayedCardInfo> eventCards = new List<PlayedCardInfo>();

    public void RegisterPlayedCard(Card.EffectTypes effectTypes, int playerIndex, Card.TriggerTypes triggerType, Card.CardType cardType, int effectValue)
    {
        PlayedCardInfo playedCardInfo = new PlayedCardInfo(effectTypes, playerIndex, triggerType, effectValue);

        switch (cardType)
        {
            case Card.CardType.Champion:
                championCards.Add(playedCardInfo);
                break;
            case Card.CardType.Spell:
                spellCards.Add(playedCardInfo);
                break;
            case Card.CardType.Event:
                eventCards.Add(playedCardInfo);
                break;
        }
    }
}