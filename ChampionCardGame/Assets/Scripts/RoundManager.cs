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
    public ChampionDropZone championDropZone;

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
        StartCoroutine(DrawCardsWithDelay(7, 3f, 0.2f));
        // For example, to draw 7 cards with an initial delay of 3 seconds and a delay of 0.2 seconds between draws, you could use:

        drawPhaseActive = true;
        Invoke("DrawPhase", 4f);
        Invoke(roundText.text = "Draw Phase", 3f);
    }

    IEnumerator DrawCardsWithDelay(int numberOfCards, float initialDelay, float delayBetweenDraws)
    {
        yield return new WaitForSeconds(initialDelay); // Wait for the initial delay

        // Draw the specified number of cards
        for (int i = 0; i < numberOfCards; i++)
        {
            cardManager.DrawCard();
            yield return new WaitForSeconds(delayBetweenDraws); // wait for the delay between draws
        }
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
        StartCoroutine(gameManager.ExecuteBattlePhase());
    }

    public void EndPhase()
    {
        // Clear the Champion Drop Zone
        if (championDropZone.cardInChampionZone != null)
        {
            CardBehaviour card = championDropZone.cardInChampionZone.GetComponent<CardBehaviour>();
        }
    }

    public void ChampionDiedRoundSwitch()
    {

    }
}
