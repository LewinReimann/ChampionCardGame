using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInfoText : MonoBehaviour
{
    public Player player1;
    public Player player2;
    public Round roundCounter;
    public Round currentPhaseIndex;

    public TextMeshProUGUI player1HealthText;
    public TextMeshProUGUI player2HealthText;
    public TextMeshProUGUI rollResultText;
    public TextMeshProUGUI PhaseText;
    public TextMeshProUGUI roundCounterText;

    private string GetPhaseName(int phaseIndex)
    {
        switch (phaseIndex)
        {
            case 0:
                return "Draw Phase";
            case 1 :
                return "Champion Phase";
            case 2:
                return "Secondary Phae";
            case 3:
                return "BattlePhase";
            case 4:
                return "End Phase";
            default:
                return "UnknownPhase";
        }
    }

    private void Update()
    {

        // Update player health text
        player1HealthText.text = player1.playerName + "\n" + player1.health.ToString();
        player2HealthText.text = player2.playerName + "\n" + player2.health.ToString();

        // Update roll results text

        // Update Phase and round text
        PhaseText.text = GetPhaseName(currentPhaseIndex.currentPhaseIndex);
        roundCounterText.text = roundCounter.RoundCounter.ToString();


    }

}
