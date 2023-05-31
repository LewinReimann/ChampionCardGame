using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public bool drawPhaseActive;
    public bool championPhaseActive;
    public bool secondaryPhaseActive;
    public bool revealPhaseActive;
    public bool battlePhaseActive;
    public bool endPhaseActive;

    public int RoundCount = 0;

    public void SwitchPhase()
    {
        if (drawPhaseActive == true)
        {
            drawPhaseActive = false;
            championPhaseActive = true;
            ChampionPhase();
        }
        else if (championPhaseActive == true)
        {
            championPhaseActive = false;
            secondaryPhaseActive = true;
            SecondaryPhase();
        }
        else if (secondaryPhaseActive == true)
        {
            secondaryPhaseActive = false;
            revealPhaseActive = true;
            RevealPhase();
        }
        else if (revealPhaseActive == true)
        {
            revealPhaseActive = false;
            battlePhaseActive = true;
            BattlePhase();
        }
        else if (battlePhaseActive == true)
        {
            battlePhaseActive = false;
            endPhaseActive = true;
            EndPhase();
        }
        else if (endPhaseActive == true)
        {
            endPhaseActive = false;
            drawPhaseActive = true;
            DrawPhase();
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
        
    }

    public void EndPhase()
    {
        
    }
}
