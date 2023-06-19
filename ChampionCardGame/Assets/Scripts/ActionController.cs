using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : MonoBehaviour
{
    public GameManager gameManager;
    public EventManager eventManager;

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
        // Subscribe to all events
        foreach (Card.TriggerTypes triggerType in System.Enum.GetValues(typeof(Card.TriggerTypes)))
        {
            SubscribeToTrigger(triggerType);
            Debug.Log("Subscribed to all events uwu");
        }
    }

    public void ActionDrawCard()
    {
        
    }

    public void ActionScoutCard()
    {

    }

    public void ActionDealDamage(int playerIndex, int damageAmount)
    {
        gameManager.ChampionDealDamage(playerIndex, damageAmount);
    }

    public void ActionHeal(int playerIndex, int healAmount)
    {
        gameManager.ChampionHeal(playerIndex, healAmount);
    }

    public void ActionRollPlus()
    {

    }

    public void ActionSummon()
    {

    }

    public void ActionSearchFor()
    {

    }

    public void ActionCastCard()
    {

    }

    public void ActionDestroySecondary()
    {

    }

    public void ActionDestroyTopDeck()
    {

    }

    public void ActionRevive()
    {

    }

    public void ActionSpellshield()
    {

    }

    // LOGIQ AND CARD HANDLING LISTS EVENTS QUEUE AND SO ON

    public void HandleEvent(Card.TriggerTypes triggerType)
    {
        // Check championCards, spellCards, and eventCards for matching triggertypes
        foreach (var spellCardInfo in spellCards)
        {
            if (spellCardInfo.TriggerType == triggerType)
            {
                ExecuteEffects();
            }
            foreach (var championCardInfo in championCards)
            {
                if (championCardInfo.TriggerType == triggerType)
                {
                    ExecuteEffects();
                }
            }
            foreach (var eventCardInfo in eventCards)
            {
                if (eventCardInfo.TriggerType == triggerType)
                {
                    ExecuteEffects();
                }
            }
        }
    }

    public void ExecuteEffects()
    {
        Debug.Log("A Trigger was right and we fire an effect");
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
