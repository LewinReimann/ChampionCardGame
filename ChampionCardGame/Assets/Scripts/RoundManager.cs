using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoundManager : MonoBehaviour
{
    public bool drawPhaseActive;
    public bool championPhaseActive;
    public bool secondaryPhaseActive;
    public bool revealPhaseActive;
    public bool battlePhaseActive;
    public bool endPhaseActive;

    public int RoundCount = 0;

    public TMP_Text roundText;

    public GameManager gameManager;
    public CardManager cardManager;

    public void Start()
    {
        drawPhaseActive = false;
        championPhaseActive = false;
        secondaryPhaseActive = false;
        revealPhaseActive = false;
        battlePhaseActive = false;
        endPhaseActive = false;

        GameStarts();
    }

    public void GameStarts()
    {
        Invoke("cardManager.DrawCard", 0.1f);
        Invoke("cardManager.DrawCard", 0.2f);
        Invoke("cardManager.DrawCard", 0.3f);
        Invoke("cardManager.DrawCard", 0.4f);
        Invoke("cardManager.DrawCard", 0.5f);
        Invoke("cardManager.DrawCard", 0.6f);
        Invoke("cardManager.DrawCard", 0.7f);


        drawPhaseActive = true;
        Invoke("DrawPhase", 3f);
        Invoke(roundText.text = "Draw Phase", 3f);
    }

    public void CanPlayCards()
    {
        if (championPhaseActive == true || secondaryPhaseActive == true)
        {
            // enable deblock draggable and scripts that allow play
        }
        else
        {
            // Block draggable scripts
        }
    }

    public void SwitchPhase()
    {
        if (drawPhaseActive == true)
        {
            drawPhaseActive = false;
            championPhaseActive = true;
            ChampionPhase();

            roundText.text = "Champion Phase";
        }
        else if (championPhaseActive == true)
        {
            championPhaseActive = false;
            secondaryPhaseActive = true;
            SecondaryPhase();

            roundText.text = "Secondary Phase";
        }
        else if (secondaryPhaseActive == true)
        {
            secondaryPhaseActive = false;
            revealPhaseActive = true;
            RevealPhase();

            roundText.text = "Reveal Phase";
        }
        else if (revealPhaseActive == true)
        {
            revealPhaseActive = false;
            battlePhaseActive = true;
            BattlePhase();

            roundText.text = "Battle Phase";
        }
        else if (battlePhaseActive == true)
        {
            battlePhaseActive = false;
            endPhaseActive = true;
            EndPhase();

            roundText.text = "End Phase";
        }
        else if (endPhaseActive == true)
        {
            endPhaseActive = false;
            drawPhaseActive = true;
            DrawPhase();

            roundText.text = "Draw Phase";
        }
        else
        {
            Debug.Log("No active Phase found need fixing");
        }
    }

    public void DrawPhase()
    {
        cardManager.DrawCard();

        Invoke("SwitchPhase", 1f);
    }

    public void ChampionPhase()
    {
        
    }

    public void SecondaryPhase()
    {
        
    }

    public void RevealPhase()
    {
        
    }

    public void BattlePhase()
    {
        
    }

    public void EndPhase()
    {
        
    }
}
