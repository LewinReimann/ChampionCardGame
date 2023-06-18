using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card Triggers/Create New Card Trigger")]
public class CardTrigger : ScriptableObject
{

    private TriggerController triggerController;
    public AnimationController animationController;
    private Card associatedCard; 

    public TriggerTypes triggerType;
    public CardEffect cardEffect;

    public void Initialize(Card associatedCard)
    {
        this.associatedCard = associatedCard;
        triggerController = Object.FindObjectOfType<TriggerController>();
        animationController = Object.FindObjectOfType<AnimationController>();
        
        if (associatedCard != null)
        {
            cardEffect = associatedCard.cardEffect;
        }
    }

    public void SubscribeToEvents()
    {
        string eventName = triggerType.ToString();
        EventManager.Instance.Subscribe(eventName, OnEventFired);
    }

    private void OnEventFired()
    {
        // This method gets called when the corresponding event is fired
        if (cardEffect != null)
        {
            

            // queue the animation
            animationController.QueueAnimation(associatedCard.cardArtwork, () =>
            {
                // Execute the effect after the animation completes
                cardEffect.ExecuteEffect();
            });
        }
        else
        {
            Debug.Log("No card effect has been slotted");
        }
    }

    public void UnsubscribeFromEvents()
    {
        string eventName = triggerType.ToString();
        EventManager.Instance.Unsubscribe(eventName, OnEventFired);
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

    public void OnTriggerFired()
    {
        if (cardEffect != null)
        {
            cardEffect.ExecuteEffect(); 
        }
    }
}
