using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : MonoBehaviour
{
    private GameManager gameManager;
    private CardManager cardManager;


    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        cardManager = FindObjectOfType<CardManager>();
    }

    public void ActionDrawCard()
    {
        cardManager.DrawCard();
    }

    public void ActionScoutCard()
    {

    }

    public void ActionDealDamage(int damageAmount)
    {
        gameManager.ChampionDealDamage(damageAmount);
    }

    public void ActionHeal(int healAmount)
    {
        gameManager.ChampionHeal(healAmount);
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
}
