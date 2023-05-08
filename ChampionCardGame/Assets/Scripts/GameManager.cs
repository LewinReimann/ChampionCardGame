using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    // Public Information we Feed into this script
    public Player player1;
    public Player player2;

    public Round round;
    public DiceManager diceManager;

    public int player1ChampionHealth = 5;
    public int player1ChampionAttackPower = 1;
    public int player2ChampionHealth = 5;
    public int player2ChampionAttackPower = 1;

    public event Action<int, int> OnRollsCompleted;

    public static GameManager instance;





    // Private Information we Feed into this script


    public void Awake()
    {
        
        // Check if an instance of the GameManager already exists
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // Set this instance as the single instance
        instance = this;
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
        
            // Calculate the damage
            if (diceManager.player1Roll > diceManager.player2Roll)
            {
                player2ChampionHealth -= player1ChampionAttackPower;
                Debug.Log("Is this Health changing?");
            }
            else if (diceManager.player2Roll > diceManager.player1Roll)
            {
                player1ChampionHealth -= player2ChampionAttackPower;
                Debug.Log("Is this Health changing? Player2");
            }

            else
            {
                // nothing happens
            }
            // If the rolls are equal, no damage is dealt
        
    }

    private void StartGame()
    {
        // Initialize player and opponent health
        player1.health = 3;
        player2.health = 3;

        // Initialize champion health and attack Power
        player1ChampionAttackPower = 1;
        player2ChampionAttackPower = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Starts the round
        round.StartRound();
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
        if (player1.health <= 0)
        {
            Debug.Log("Player1 has lost the game.");
            StartGame();
            // End game logic goes here
        }
        else if (player2.health <= 0)
        {
            Debug.Log("Player2 has lost the game.");
            StartGame();
            // End game logic goes here
        }

    }
}
