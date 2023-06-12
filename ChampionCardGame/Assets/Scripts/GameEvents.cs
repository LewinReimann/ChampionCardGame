using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    // Events
    public static event Action<Card> OnCardPlayed;
    public static event Action<int> OnDiceRolled;
    public static event Action OnTurnEnded;
    public static event Action<int> ModifyDiceRoll;

    // Additional events go here . . .

    // Methods to raise events
    public static void RaiseOnCardPlayed(Card card)
    {
        OnCardPlayed?.Invoke(card);
    }

    public static void RaiseOnDiceRolled(int value)
    {
        OnDiceRolled?.Invoke(value);
    }

    public static void RaiseOnTurnEnded()
    {
        OnTurnEnded?.Invoke();
    }

    public static int RaiseModifyDiceRoll(int initialResult)
    {
        int modifiedResult = initialResult;
        ModifyDiceRoll?.Invoke(modifiedResult);
        return modifiedResult;
    }

    // Additional raise methods go here . . .

    public abstract class CardEffect
    {
        public abstract void ActivateEffect(Card card);
    }

    public class DrawCardEffect : CardEffect
    {
        public override void ActivateEffect(Card card)
        {
            // Code to draw a card
        }
    }

    public class ModifyRollEffect : CardEffect
    {
        public int Modifier { get; private set; }

        public ModifyRollEffect(int modifier)
        {
            Modifier = modifier;
        }

        public override void ActivateEffect(Card card)
        {
            // Code to modify roll value
        }
    }

    // More effects here . . .
}
