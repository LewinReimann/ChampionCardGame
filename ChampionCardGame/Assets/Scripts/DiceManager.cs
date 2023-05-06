using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DiceManager : MonoBehaviour
{
    public GameObject dicePrefabPlayer1;
    public GameObject dicePrefabPlayer2;
    public Transform spawnPointPlayer1;
    public Transform spawnPointPlayer2;

    private int rollCount = 0;
    public int player1Roll;
    public int player2Roll;

    public (int, int) GetPlayerRolls()
    {
        return (player1Roll, player2Roll);
    }

    public void RollDice(GameObject dicePrefab, Transform spawnPoint, System.Action<int, bool> onRollCompleted)
    {
        GameObject diceInstance = Instantiate(dicePrefab, spawnPoint.position, Quaternion.identity);
        diceInstance.transform.SetParent(spawnPoint); // Set the dice as a child of the spawn Point
        Dice dice = diceInstance.GetComponent<Dice>();
        bool isPlayer1 = (dicePrefab == dicePrefabPlayer1);
        dice.OnRollCompleted += (result) => onRollCompleted(result, isPlayer1);
        dice.Roll();

    }

    private void HandleRollCompleted(int result, bool isPlayer1)
    {
        rollCount++;

        GameObject diceInstance = GameObject.FindGameObjectWithTag("Dice");

        if (isPlayer1)
        {
            player1Roll = result;
        }
        else
        {
            player2Roll = result;
        }

        Dice dice = FindObjectOfType<Dice>();
        dice.OnRollCompleted -= (r) => HandleRollCompleted(r, isPlayer1);

        int finalResult = result;

        Debug.Log("Dice Roll result: " + finalResult);

        Destroy(diceInstance, 1f);

        if (player1Roll != 0 && player2Roll != 0)
        {
            GameManager.instance.OnRollsCompleted?.Invoke(player1Roll, player2Roll);
            player1Roll = 0;
            player2Roll = 0;
        }

    }

    public void RollDiceForBothPlayers()
    {
        RollDice(dicePrefabPlayer1, spawnPointPlayer1, HandleRollCompleted);
        RollDice(dicePrefabPlayer2, spawnPointPlayer2, HandleRollCompleted);
    }


}
