using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public DiceManager diceManager;

    public List<Card> allCards = new List<Card>();
    public List<Card> onHand = new List<Card>();
    public List<Card> onBoard = new List<Card>();
    public List<Card> graveyard = new List<Card>();

    public List<CardDisplay> cardsInPlay = new List<CardDisplay>();

    
    public void AddCardToHand(Card card)
    {
        onHand.Add(card);
    }

    public void RemoveCardFromHand(Card card)
    {
        onHand.Remove(card);
    }

    public void AddCardToPlay(Card card)
    {
        onBoard.Add(card);
    }

    public void RemoveCardFromPlay(Card card)
    {
        onBoard.Remove(card);
    }

    public void AddCardToGraveyard(Card card)
    {
        graveyard.Add(card);
    }

    public void RemoveCardFromGraveyard(Card card)
    {
        graveyard.Remove(card);
    }

    public void ClearBoard()
    {
        // Find all CardDisplay components in the scene
        CardDisplay[] cardDisplays = FindObjectsOfType<CardDisplay>();

        // Loop through all the CardDisplays components
        foreach (CardDisplay cardDisplay in cardDisplays)
        {
            // Check if the card is in the onBoard list
            if (onBoard.Contains(cardDisplay.card))
            {
                // Destroy the cards GameObject and remove it from the onBoard List
                Destroy(cardDisplay.gameObject);
                onBoard.Remove(cardDisplay.card);
            }
        }
        // Find all GameObjects in the scene that have a Slot component
        Slot[] slots = FindObjectsOfType<Slot>();

        // Loop through all the slots and set isOccupied to false
        foreach (Slot slot in slots)
        {
            slot.isOccupied = false;
        }
    }

    public void ApplyEffectsOnDamage(DamageType damageType, ref int targetChampionHealth)
    {
      if (damageType == DamageType.EffectDamage)
        {
            foreach (Card card in onBoard)
            {
                if (card.effectCondition == EffectCondition.OnReceiveEffectDamage)
                {
                    // Apply the corresponding action
                    switch (card.effectAction)
                    {
                        case EffectAction.Heal1:
                            targetChampionHealth += 1;
                            break;

                        // Handle more actions as needed

                        default:
                            break;
                    }
                }
            }
        }
    }

    public void ApplyEffectsOnDraw()
    {

    }

    public void ApplyEffectsOnRoll(int player1Roll, int player2Roll, ref int player1ChampionHealth, ref int player2ChampionHealth)
    {
        foreach (Card card in onBoard)
        {
            // Check if the cards condition is met
            bool conditionMet = false;
            switch (card.effectCondition)
            {
                case EffectCondition.OnRoll5:
                    if (player1Roll == 5)
                    {
                        conditionMet = true;
                    }
                    break;

                // Handle more condition as needed

                default:
                    break;
            }

            // if the condition is met, apply the corresponding action
            if (conditionMet)
            {
                switch (card.effectAction)
                {
                    case EffectAction.Deal1DamageToOpponent:
                        // Apply 1 damage to the opponent
                        DealDamage(ref player2ChampionHealth, 1, DamageType.EffectDamage);
                        Debug.Log("Effect has been triggerd correctly at roll 5");
                        break;

                    // Handle more actions as needed

                    default:
                        break;
                }
            }
        }
    }

    public void DealDamage(ref int targetChampionHealth, int damageAmount, DamageType damageType)
    {
        targetChampionHealth -= damageAmount;
        ApplyEffectsOnDamage(damageType, ref targetChampionHealth);
    }

}

public enum DamageType
{
    BattleDamage,
    EffectDamage
}
