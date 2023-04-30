using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Public Information we Feed into this script
    public Player player1;
    public Player player2;

    public Round round;

    public int player1ChampionHealth = 5;
    public int player1ChampionAttackPower = 1;
    public int player2ChampionHealth = 5;
    public int player2ChampionAttackPower = 1;

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

        public int RollDice() // This Is our standard Dice Roll of 1-6 to see what gets rolled.
    {
        return Random.Range(1, 7);
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
            }
        }

       
    }

    public void Attack() // We Roll the RollDice we defined earlier to see who gets damaged with what Value. 
    {
        int player1Roll = RollDice();
        int player2Roll = RollDice();

        if (player1Roll > player2Roll)
        {
            player2ChampionHealth -= player1ChampionAttackPower;
        }
        else if (player2Roll > player1Roll)
        {
            player1ChampionHealth -= player2ChampionAttackPower;
        }
        else // rolls are the same
        {
            // no harm done
        }
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
