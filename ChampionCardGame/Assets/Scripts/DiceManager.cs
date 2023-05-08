using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DiceManager : MonoBehaviour
{
    public CardManager cardManager;

    public GameObject dicePrefabPlayer1;
    public GameObject dicePrefabPlayer2;
    public Transform spawnPointPlayer1;
    public Transform spawnPointPlayer2;

    public int rollCount = 0;
    public int player1Roll;
    public int player2Roll;

    public event Action<int, int> OnRollsCompleted;

    public (int, int) GetPlayerRolls()
    {
        return (player1Roll, player2Roll);
    }

    public void RollDice(GameObject dicePrefab, Transform spawnPoint, System.Action<int, bool, Dice> onRollCompleted)
    {
        GameObject diceInstance = Instantiate(dicePrefab, spawnPoint.position, Quaternion.identity);
        diceInstance.transform.SetParent(spawnPoint); // Set the dice as a child of the spawn Point
        Dice dice = diceInstance.GetComponent<Dice>();
        bool isPlayer1 = (dicePrefab == dicePrefabPlayer1);
        dice.Roll();

        dice.OnRollCompleted += (result) => onRollCompleted(result, isPlayer1, dice);


    }

    private void HandleRollCompleted(int result, bool isPlayer1, Dice dice)
    {
        rollCount++;

        GameObject diceInstance = dice.gameObject;

        if (isPlayer1)
        {
            player1Roll = result;
        }
        else
        {
            player2Roll = result;
        }

        int finalResult = result;


        Destroy(diceInstance, 1f);

        // Check if both dice rolls have been completed
        if (rollCount == 2)
        {
            // Trigger the OnRollsCompleted event
            OnRollsCompleted?.Invoke(player1Roll, player2Roll);
        }
    }

    public void RollDiceForBothPlayers()
    {
        rollCount = 0;

        RollDice(dicePrefabPlayer1, spawnPointPlayer1, (result, player1Roll, dice) => HandleRollCompleted(result, true, dice));
        RollDice(dicePrefabPlayer2, spawnPointPlayer2, (result, player2Roll, dice) => HandleRollCompleted(result, false, dice));
    }


}
