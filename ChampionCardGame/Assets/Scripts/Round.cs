using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Round : MonoBehaviour
{
    
    private bool roundActive = false;
    public int currentPhaseIndex = 0;
    public int RoundCounter = 0;
    public GameManager gameManager;
    public DeckManager deckManager;

    private bool drawPhaseActive = false;
    // private bool championPhaseActive = false;
    // private bool secondaryPhaseActive = false;
    private bool battlePhaseActive = false;
    // private bool endPhaseActive = false;

    public void StartRound()
    {
        gameManager = FindObjectOfType<GameManager>(); // Pass reference to GameManager to Round script
        roundActive = true;
        SwitchTurn();
    }

    public void SwitchTurn()
    {
        
        
        if (currentPhaseIndex == 0)
        {
            DrawPhase();
        }
        else if (currentPhaseIndex == 1)
        {
            
            ChampionPhase();
        }
        else if (currentPhaseIndex == 2)
        {
            SecondaryPhase();
        }
        else if (currentPhaseIndex == 3)
        {
           
            BattlePhase();
        }
        else if (currentPhaseIndex == 4)
        {
            
            EndPhase();
            RoundCounter++;
            Debug.Log("Round has increased by" + RoundCounter);
        }

        if (currentPhaseIndex >= 5)
        {
            currentPhaseIndex = 0;
        }

    }
    
    public void DrawPhase()
    {
        Debug.Log("Draw Phase has begun");
        drawPhaseActive = true;
        deckManager.DrawCard();

        drawPhaseActive = false;
        currentPhaseIndex++;
    }

    private void ChampionPhase()
{
        Debug.Log("Champion Phase has begun");
        currentPhaseIndex++;
        // TODO Implement logic for the champion phase
    }

private void SecondaryPhase()
{
        Debug.Log("Secondary Phase has begun");
        currentPhaseIndex++;
}

private void BattlePhase()
    {
        Debug.Log("End Phase has begun");
        battlePhaseActive = true;
        StartCoroutine(ExecuteBattlePhase()); //Co-Routine is for something to Cooperate in a "Routine" of course like two small codes run at the same time and not one after another
    }

    public void EndBattlePhase() // Call this function to end the BattleRound
    {
        battlePhaseActive = false;

        // Set Champion Health PLACEHOLDER FOR TESTIN
        gameManager.player1ChampionHealth = Random.Range(3, 6);
        gameManager.player2ChampionHealth = Random.Range(3, 6);

        currentPhaseIndex++;
    }

    private IEnumerator ExecuteBattlePhase() //IEnumerator are  something that will call actions in a sequence like go again and again like here: Do Attack then check if someones Health is below 0 and then wait one second and execute again from top)
    {
        while (battlePhaseActive)
        {
            gameManager.Attack();

            if (gameManager.player1ChampionHealth <= 0)
            {
                gameManager.player1.health--;
                EndBattlePhase();
            }
            else if (gameManager.player2ChampionHealth <= 0)
            {
                gameManager.player2.health--;
                EndBattlePhase();
            }

            yield return new WaitForSeconds(1f); // Wait for 1 second before executing the next attack
        }
    }

    private void EndPhase()
{
        Debug.Log("End Phase has begun");
        currentPhaseIndex++;
        // TODO implement logic for the EndPhase
    }

}
