using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Public Information we Feed into this script
    
    public Photon.Realtime.Player player1;
    public Photon.Realtime.Player player2;

    public PlayerData player1Data;
    public PlayerData player2Data;

    public Round round;
    public DiceManager diceManager;
    public CardManager cardManager;

    public int player1ChampionHealth = 5;
    public int player1ChampionAttackPower = 1;
    public int player2ChampionHealth = 5;
    public int player2ChampionAttackPower = 1;

    public event Action<int, int> OnRollsCompleted;

    public static GameManager Instance { get; private set; }

    public int playerID = 0;

    // Private Information we Feed into this script

    public void SetPlayer1(Photon.Realtime.Player player)
    {
        player1 = player;
    }

    public void SetPlayer2(Photon.Realtime.Player player)
    {
        player2 = player;
    }

    public int GetPlayer1ActorNumber()
    {
        return player1.ActorNumber;
    }

    public int GetPlayer2ActorNumber()
    {
        return player2.ActorNumber;
    }

    public void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        
        // Check if an instance of the GameManager already exists
        if (Instance != null)
        {
            Debug.LogError("More t han one GameManager instance found!");
            Destroy(this.gameObject);
            return;
        }

        Debug.Log("GameManager Awake called. Instance Set.");

        // Set this instance as the single instance
        Instance = this;

        // Initialize players health
        player1Data = new PlayerData(3);
        player2Data = new PlayerData(3);

        // Dont destroy this object when changing scenes
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        round = FindObjectOfType<Round>();
        diceManager = FindObjectOfType<DiceManager>();
        cardManager = FindObjectOfType<CardManager>();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }



    public void SetChampionHealth()
    {
        GameObject slotPlayer1 = GameObject.Find("SCSlotPlayer1");
        if (slotPlayer1 != null)
        {
            ChampionCard championCard = slotPlayer1.GetComponentInChildren<ChampionCard>();
            if (championCard != null)
            {
                int health = championCard.health;
                player1ChampionHealth = health;

                Debug.Log($"Player1 Champion set. Health: {health}");
            }
        }

        GameObject slotPlayer2 = GameObject.Find("SCSlotPlayer2");
        if (slotPlayer2 != null)
        {
            ChampionCard championCard = slotPlayer2.GetComponentInChildren<ChampionCard>();
            if (championCard != null)
            {
                int health = championCard.health;
                player2ChampionHealth = health;

                Debug.Log($"Player2 Champion set. Health: {health}");
            }
        }

    }

    public IEnumerator Attack()
    {
        diceManager.RollDiceForBothPlayers();

        yield return new WaitUntil(() => diceManager.rollCount == 2);

        cardManager.ApplyEffectsOnRoll(diceManager.player1Roll, diceManager.player2Roll, ref player1ChampionHealth, ref player2ChampionHealth);

            // Calculate the damage
            if (diceManager.player1Roll > diceManager.player2Roll)
            {
                cardManager.DealDamage(ref player2ChampionHealth, player1ChampionAttackPower, DamageType.BattleDamage);
            }
            else if (diceManager.player2Roll > diceManager.player1Roll)
            {
                cardManager.DealDamage(ref player1ChampionHealth, player2ChampionAttackPower, DamageType.BattleDamage);
            }

            else
            {
                // nothing happens
            }
            // If the rolls are equal, no damage is dealt
        
    }

    private void StartGame()
    {

       
        // Initialize champion health and attack Power
        player1ChampionAttackPower = 1;
        player2ChampionAttackPower = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Starts the round
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if EnGame should be called
        EndGame();
    }

    private void EndGame()
    {
        if (player1Data.Health <= 0)
        {
            Debug.Log("Player1 has lost the game.");
            StartGame();
            // End game logic goes here
        }
        else if (player2Data.Health <= 0)
        {
            Debug.Log("Player2 has lost the game.");
            StartGame();
            // End game logic goes here
        }

    }
}
