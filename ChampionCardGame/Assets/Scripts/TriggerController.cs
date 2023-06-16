using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerController : MonoBehaviour
{
    private GameManager gameManager;
    private CardManager cardManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        cardManager = FindObjectOfType<CardManager>();
    }

    public void TriggerWheneverCardDrawn()
    {

    }

    public void TriggerWheneverCardPlayed()
    {

    }

    public void TriggerCardPlayed()
    {

    }

    public void TriggerWhenDamageReceived()
    {
        
    }

    public void TriggerWhenHealingReceived()
    {

    }

    public void TriggerWhenDestroyed()
    {

    }

    public void TriggerWhenTurnEnd()
    {

    }

    public void TriggerWhenBattlePhaseEnd()
    {

    }

    public void TriggerWhenSpellCasted()
    {

    }

}
