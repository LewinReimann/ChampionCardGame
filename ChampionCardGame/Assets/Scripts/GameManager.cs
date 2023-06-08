using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using System;

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

    public int playerChampionHealth;
    public int opponentChampionHealth;

    public bool isPlayerTurn = false;
    public bool isBattleActive;

    public RoundManager roundManager;

    new private PhotonView photonView;

    public CardManager cardManager;

    // Dice Stuff

    public GameObject dicePrefabPlayer;
    public GameObject dicePrefabOpponent;
    public Transform spawnPointPlayer;
    public Transform spawnPointOpponent;

    public int rollCount = 0;
    public int player1Roll;
    public int player2Roll;

    public void SpawnAndRollDice(GameObject dicePrefab, Transform spawnPoint)
    {
        // Instantiate the dice at the specified spawn point
        GameObject diceInstance = Instantiate(dicePrefab, spawnPoint.position, Quaternion.identity);

        // SEt the dice as a child of the spawnpoint
        diceInstance.transform.SetParent(spawnPoint);

        // Get the Dice component and call the roll method
        Dice dice = diceInstance.GetComponent<Dice>();
        dice.Roll();
    }

    public void RollDice()
    {
        SpawnAndRollDice(dicePrefabPlayer, spawnPointPlayer);
        SpawnAndRollDice(dicePrefabOpponent, spawnPointOpponent);
    }

    void Start()
    {
        photonView = GetComponent<PhotonView>();

        // Determine which player goes first
        if (PhotonNetwork.IsMasterClient)
        {
            isPlayerTurn = UnityEngine.Random.Range(0, 2) == 0;
        }

        playersLifeText.text = playersLife.ToString();

        cardManager.InitializeDeck();
    }

    public IEnumerator ExecuteBattlePhase()
    {
        isBattleActive = true;

        while (isBattleActive)
        {
            int playerDiceRoll = UnityEngine.Random.Range(1, 7);
            int opponentDiceRoll = UnityEngine.Random.Range(1, 7);

            if (playerDiceRoll < opponentDiceRoll)
            {
                opponentChampionHealth--;
            }
            else if (playerDiceRoll > opponentDiceRoll)
            {
                playerChampionHealth--;
            }

            yield return new WaitForSeconds(1f);
        }
    }

    private void Update()
    {
        if (playerChampionHealth <= 0)
        {
            isBattleActive = false;
            playersLife--;
            roundManager.SwitchPhase();
        }

        if (opponentChampionHealth <= 0)
        {
            isBattleActive = false;
            opponentLife--;
            roundManager.SwitchPhase();
        }

        if (playersLife <= 0)
        {
            Debug.Log("Player 2 wins the game!");
        }
        else if (opponentLife <= 0)
        {
            Debug.Log("Player 1 wins the game!");
        }
    }
}
