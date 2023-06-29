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
    public RoundManager roundManager;

    public Transform playerDropZone;
    public Transform opponentDropZone;

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

    public void ActionDrawCard(EffectContext context)
    {
        // Determine the appropriate card manager based on the playerIndex
        CardManager appropriateCardManager = (context.playerIndex == 0) ? playerCardManager : opponentCardManager;

        // Call the DrawCard method drawAmount times
        for (int i = 0; i < context.Value; i++)
        {
            appropriateCardManager.DrawCard();
        }
    }

    public void ActionScoutCard(EffectContext context)
    {
        Debug.Log("Scout Effect");
    }

    public void ActionDealDamage(EffectContext context)
    {
        gameManager.ChampionDealDamage(context.playerIndex, context.Value);
    }

    public void ActionHeal(EffectContext context)
    {
        gameManager.ChampionHeal(context.playerIndex, context.Value);
    }

    public void ActionRollPlus(EffectContext context)
    {
        Debug.Log("RollPlus Effect");
    }

    public void ActionSummon(EffectContext context)
    {
        // Get the appropriate CardManager based on playerIndex
        CardManager appropriateCardManager = (context.playerIndex == 0) ? playerCardManager : opponentCardManager;

        // Iterate through the list of cards indices and summon each card
        foreach (int cardIndex in context.CardIndices)
        {
            // Get the card to be summoned from the summonable cards pool based on cardIndex
            if (cardIndex < 0 || cardIndex >= appropriateCardManager.summonableCardsPool.Count)
            {
                Debug.LogError("Invalid card index for summon.");
                return;
            }
            Card cardToSummon = appropriateCardManager.summonableCardsPool[cardIndex];

            // Instantiate the card 
            GameObject cardObject = Instantiate(appropriateCardManager.cardPrefab);
            cardObject.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);

            // Get the CardDisplay component and set its card to cardToSummon
            CardDisplay cardDisplay = cardObject.GetComponent<CardDisplay>();
            cardDisplay.card = cardToSummon;

            CardBehaviour cardBehaviour = cardObject.GetComponent<CardBehaviour>();
            cardBehaviour.playerIndex = context.playerIndex;
            cardBehaviour.appropriateCardManager = appropriateCardManager;

            // SEt inPlay to true as the card is being summoned
            cardBehaviour.isInPlay = true;

            // Summon the card in the regular DropZone
            if (roundManager.secondaryPhaseActive && cardToSummon.type != Card.CardType.Event)
            {
                Transform originalParent = (context.playerIndex == 0) ? playerDropZone : opponentDropZone;
                Quaternion originalRotation = Quaternion.Euler(0, 0, 0);

                // set the parent of the dropZone
                cardObject.transform.SetParent(originalParent, true);
                cardObject.transform.localRotation = originalRotation;

                // Add the card to the field list directly
                appropriateCardManager.field.Add(cardToSummon);
            }
        }
    }

    public void ActionSearchFor(EffectContext context)
    {
        Debug.Log("SearchFor Effect");
    }

    public void ActionCastCard(EffectContext context)
    {
        Debug.Log("Cast Effect");
    }

    public void ActionDestroySecondary(EffectContext context)
    {
        Debug.Log("DestroySecondary Effect");
    }

    public void ActionDestroyTopDeck(EffectContext context)
    {
        Debug.Log("TopDeck Effect");
    }

    public void ActionRevive(EffectContext context)
    {
        Debug.Log("Revive Effect");
    }

    public void ActionSpellshield(EffectContext context)
    {
        Debug.Log("SpellShield Effect");
    }

    // LOGIQ AND CARD HANDLING LISTS EVENTS QUEUE AND SO ON

    private Dictionary<Card.EffectTypes, Action<EffectContext>> effectHandlers;

    private void InitializeEffectHandlers()
    {
        effectHandlers = new Dictionary<Card.EffectTypes, Action<EffectContext>>
        {
            { Card.EffectTypes.DrawCard, ActionDrawCard },
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
                // Create a new EffectContext for each played card that matches the trigger type
                EffectContext context = new EffectContext();

                // Populate the EffectContext with the necessary data from the played card
                context.playerIndex = playedCard.PlayerIndex;
                context.Value = playedCard.EffectValue;
                context.CardIndices = playedCard.SummonCardIndices;

                // Execute the effect with the populated context
                ExecuteEffect(playedCard.Effect, context);
            }
        }
    }

    public class EffectContext
    {
        public int playerIndex { get; set; }
        public int Value { get; set; }
        public List<int> CardIndices { get; set; }
    }

    public void ExecuteEffect(Card.EffectTypes effectType, EffectContext context)
    {
        if (effectHandlers.ContainsKey(effectType))
        {
            effectHandlers[effectType].Invoke(context);
        }
        else
        {
            Debug.Log("Effect handler not found for effect type: " + effectType.ToString());
        }
    }

    public List<PlayedCardInfo> championCards = new List<PlayedCardInfo>();
    public List<PlayedCardInfo> spellCards = new List<PlayedCardInfo>();
    public List<PlayedCardInfo> eventCards = new List<PlayedCardInfo>();

    public void RegisterPlayedCard(
        Card.EffectTypes effectTypes, Card.EffectTypes? secondaryEffectTypes,
        int playerIndex, 
        Card.TriggerTypes triggerType, Card.TriggerTypes? secondaryTriggerType,
        Card.CardType cardType, 
        int effectValue, int? secondaryEffectValue,
        List<int> summonCardIndices, List<int>? secondarySummonCardIndices,
        EffectContext context)
    {
        PlayedCardInfo playedCardInfo = new PlayedCardInfo(
            effectTypes, secondaryEffectTypes,
            playerIndex, 
            triggerType, secondaryTriggerType,
            effectValue, secondaryEffectValue,
            summonCardIndices, secondarySummonCardIndices,
            context);

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
