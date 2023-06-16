using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card Effects/Create New Effect")]
public class CardEffect : ScriptableObject
{
    public ActionController actionController;

    public void Initialize(Card associatedCard)
    {
        if (actionController == null)
        {
            actionController = Object.FindObjectOfType<ActionController>();
            Debug.Log("Is this CardEffect initialized correctly?");
        }
    }

    public enum ActionType
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

    public ActionType actionType;

    // Additional fields for configuring the effect
    // (e.g. amount of damage, number of cards to draw, etc.)
    public int value;

    public void ExecuteEffect()
    {
        Debug.Log("Executing effect of action type: " + actionType);
        switch (this.actionType)
        {
            case CardEffect.ActionType.DrawCard:
                actionController.ActionDrawCard();
                break;
            case CardEffect.ActionType.ScoutCard:
                actionController.ActionScoutCard();
                break;
            case CardEffect.ActionType.DealDamage:
                actionController.ActionDealDamage(this.value);
                break;
            case CardEffect.ActionType.Heal:
                actionController.ActionHeal(this.value);
                break;
            case CardEffect.ActionType.RollPlus:
                actionController.ActionRollPlus();
                break;
            case CardEffect.ActionType.Summon:
                actionController.ActionSummon();
                break;
            case CardEffect.ActionType.SearchFor:
                actionController.ActionSearchFor();
                break;
            case CardEffect.ActionType.CastCard:
                actionController.ActionCastCard();
                break;
            case CardEffect.ActionType.DestroySecondary:
                actionController.ActionDestroySecondary();
                break;
            case CardEffect.ActionType.DestroyTopDeck:
                actionController.ActionDestroyTopDeck();
                break;
            case CardEffect.ActionType.Revive:
                actionController.ActionRevive();
                break;
            case CardEffect.ActionType.Spellshield:
                actionController.ActionSpellshield();
                break;
        }
    }
}
