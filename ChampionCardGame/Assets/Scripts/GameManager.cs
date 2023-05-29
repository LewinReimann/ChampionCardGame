using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.MultiplayerModels;
using TMPro;

public class GameManager : MonoBehaviour
{
    public PlayFabManager playFabManager;
    public TMP_Text playerRollText;

    public void RollDice()
    {
        int roll = Random.Range(1, 7); // generate a random number
        playerRollText.text = roll.ToString();
        playFabManager.SendRoll(roll);
        playFabManager.FetchOpponentRoll(); // Fetch the opponents roll after you have rolled
    }
}
