using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Public Information we Feed into this script
    public int player1Life = 2;
    public int player2Life = 2;

    public Round round;

    public int player1ChampionHealth = 5;
    public int player1ChampionAttackPower = 1;

    // Private Information we Feed into this script

    public int RollDice() // This Is our standard Dice Roll of 1-6 to see what gets rolled.
    {
        return Random.Range(1, 7);
    }

    // Start is called before the first frame update
    void Start()
    {
        // Starts the round
        round.StartRound();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
