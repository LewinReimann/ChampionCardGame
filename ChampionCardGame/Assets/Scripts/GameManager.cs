using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using System;

public class GameManager : MonoBehaviourPunCallbacks
{
    public List<GamePlayer> gamePlayers;
    public int currentGamePlayerIndex = 0;

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

    public CardManager playerCardManager;
    public CardManager opponentCardManager;

    public static GameManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Dice Stuff

    public GameObject dicePrefabPlayer;
    public GameObject dicePrefabOpponent;
    public Transform spawnPointPlayer;
    public Transform spawnPointOpponent;

    public int rollCount = 0;
    public int player1Roll;
    public int player2Roll;

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

        playerCardManager.InitializeDeck();
        opponentCardManager.InitializeDeck();

        // Initialize players
        gamePlayers = new List<GamePlayer>()
        {
            new GamePlayer { gamePlayerName = "Player 1", playerIndex = 0 },
            new GamePlayer { gamePlayerName = "Player 2 ", playerIndex = 1 }
        };

        // Set the first players turn
        currentGamePlayerIndex = 0;
    }

    

    public void ChampionDealDamage(int damageAmount)
    {
        opponentChampionHealth -= damageAmount;
    }

    public void ChampionHeal(int healAmount)
    {
        playerChampionHealth += healAmount;
    }

    public IEnumerator ExecuteBattlePhase()
    {
        isBattleActive = true;

        while (isBattleActive)
        {
            // Spawn the dice
            Dice playerDice = SpawnAndRollDice(dicePrefabPlayer, spawnPointPlayer).GetComponent<Dice>();
            Dice opponentDice = SpawnAndRollDice(dicePrefabOpponent, spawnPointOpponent).GetComponent<Dice>();

            int playerDiceRoll = 0;
            int opponentDiceRoll = 0;

            // Subscribe to the OnRollCompleted event
            playerDice.OnRollCompleted += (roll) => { playerDiceRoll = roll; };
            opponentDice.OnRollCompleted += (roll) => { opponentDiceRoll = roll; };

            // Wait until both dices have finished rolling
            yield return new WaitUntil(() => playerDiceRoll > 0 && opponentDiceRoll > 0);

            // Compare the dice rolls
            if (playerDiceRoll < opponentDiceRoll)
            {
                opponentChampionHealth--;
            }
            else if (playerDiceRoll > opponentDiceRoll)
            {
                playerChampionHealth--;
            }

            // Check if the battle should end
            if (playerChampionHealth <= 0 || opponentChampionHealth <= 0)
            {
                isBattleActive = false;
            }

            // Delay before next roll
            yield return new WaitForSeconds(1f);
        }

        // Proceed to the next phase
        roundManager.SwitchPhase();
        
    }

    public GameObject SpawnAndRollDice(GameObject dicePrefab, Transform spawnPoint)
    {
        // Instantiate the dice at the specifific spawn point
        GameObject diceInstance = Instantiate(dicePrefab, spawnPoint.position, Quaternion.identity);

        // Set the dice as a child of the spawn point
        diceInstance.transform.SetParent(spawnPoint);

        // Get the Dice component and call the Roll method
        Dice dice = diceInstance.GetComponent<Dice>();
        dice.Roll();

        return diceInstance;
    }

    private void Update()
    {
        // Check for a button press or some other trigger to switch turns
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwitchTurn();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchActivePlayer();
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

    public void SwitchTurn()
    {
        // Increment player index, if its the last player wrap around
        currentGamePlayerIndex = (currentGamePlayerIndex + 1) % gamePlayers.Count;
        Debug.Log(gamePlayers[currentGamePlayerIndex].gamePlayerName + "'s turn");
    }

    public void SwitchActivePlayer()
    {
        // Toggle between 0 and 1
        currentGamePlayerIndex = (currentGamePlayerIndex == 0) ? 1 : 0;
    }
}
