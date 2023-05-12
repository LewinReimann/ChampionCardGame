using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInfoText : MonoBehaviour
{
   
    public TextMeshProUGUI player1HealthText;
    public TextMeshProUGUI player2HealthText;
    public TextMeshProUGUI rollResultText;
    public TextMeshProUGUI PhaseText;
    public TextMeshProUGUI roundCounterText;

    private string GetPhaseName(int phaseIndex)
    {
        switch (phaseIndex)
        {
            case 1:
                return "Draw Phase";
            case 2 :
                return "Champion Phase";
            case 3:
                return "Secondary Phase";
            case 4:
                return "BattlePhase";
            case 5:
                return "End Phase";
            default:
                return "UnknownPhase";
        }
    }

    private void Update()
    {
        // Make sure gameManager and its properties are not null
        if (GameManager.Instance != null && GameManager.Instance.player1Data != null && GameManager.Instance.player2Data != null && GameManager.Instance.round != null)
        {
            // Update player health text
            player1HealthText.text = GameManager.Instance.player1Data.playerName + "\n" + GameManager.Instance.player1Data.Health.ToString();
            player2HealthText.text = GameManager.Instance.player2Data.playerName + "\n" + GameManager.Instance.player2Data.Health.ToString();

            // Update Phase and round text
            PhaseText.text = GetPhaseName(GameManager.Instance.round.currentPhaseIndex);
            roundCounterText.text = GameManager.Instance.round.RoundCounter.ToString();
        }
    }

}
