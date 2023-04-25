using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Round : MonoBehaviour
{
    
    private bool roundActive = false;
    public int currentPhaseIndex = 0;
    public int RoundCounter = 0;

    public void StartRound()
    {
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
    
    private void DrawPhase()
    {
        // TODO: Implement logic for the draw Phase
        Debug.Log("Draw Phase has begun");
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
        /*
        Debug.Log("Secondary Phase has begun");

        // Set up the conditions for the Secondary Phase to end
        bool secondaryPhaseEnded = false;
        PhaseButton phaseButton = FindObjectOfType<PhaseButton>();
        phaseButton.round = this;

        // Listen for button click to end the phase
        Button button = phaseButton.GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            secondaryPhaseEnded = true;
            roundActive = true;
            button.onClick.RemoveAllListeners();
        });

        while (!secondaryPhaseEnded)
        {
            // Do nothing until the secondary Phase ends
        }

        Debug.Log("Secondary Phase has ended");
        */
    }

private void BattlePhase()
{
        Debug.Log("Battle Phase has begun");
        currentPhaseIndex++;
        // TODO Implement logic for the battle phase
    }

private void EndPhase()
{
        Debug.Log("End Phase has begun");
        currentPhaseIndex++;
        // TODO implement logic for the EndPhase
    }

}
