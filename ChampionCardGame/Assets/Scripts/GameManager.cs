using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.MultiplayerModels;
using TMPro;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{
    public TMP_Text playerRollText;
    public TMP_Text opponentRollText;
    public TMP_Text playersLifeText;
    public TMP_Text opponentLifeText;
    public int playerRoll = 0;
    public int opponentRoll = 0;
    public int playersLife = 5;
    public int opponentLife = 5;
    public bool isPlayerTurn = false;

    new private PhotonView photonView;

    void Start()
    {
        photonView = GetComponent<PhotonView>();

        // Determine which player goes first
        if (PhotonNetwork.IsMasterClient)
        {
            isPlayerTurn = Random.Range(0, 2) == 0;
        }

        playersLifeText.text = playersLife.ToString();
    }

    public void RollDice()
    {
        if (!isPlayerTurn || playerRoll != 0) // if its not the players turn or already has rolled down allow rolling
        {
            return;
        }

        int roll = Random.Range(1, 7); // generate a random number
        playerRoll = roll;
        playerRollText.text = roll.ToString();

        // Send the dice roll to the other player
        photonView.RPC("ReceiveDiceRoll", RpcTarget.Others, roll);
    }

    [PunRPC]
    public void ReceiveDiceRoll(int roll)
    {
        opponentRoll = roll;
        opponentRollText.text = roll.ToString();

        // If both players have rolled, determine the winner and reset for the next round
        if (playerRoll != 0)
        {
            DetermineWinner();
            ResetRound();
        }
    }

    public void DetermineWinner()
    {
        if (playerRoll < opponentRoll)
        {
            Debug.Log("You win the round!");
        }
        else if (playerRoll > opponentRoll)
        {
            Debug.Log("You lose the round!");
            playersLife--;
            playersLifeText.text = playersLife.ToString();
            photonView.RPC("UpdateOpponentLife", RpcTarget.Others, playersLife);
        }
        else
        {
            Debug.Log("The round is a draw!");
        }
    }

    [PunRPC]
    public void UpdateOpponentLife(int life)
    {
        opponentLife = life;
        opponentLifeText.text = opponentLife.ToString();
    }

    public void ResetRound()
    {
        playerRoll = 0;
        opponentRoll = 0;
        playerRollText.text = "";
        opponentRollText.text = "";
        isPlayerTurn = !isPlayerTurn; // switch turns
    }
}
