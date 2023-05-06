using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour
{
    public GameObject dicePrefab;
    public Transform spawnPoint;

    public void RollDice()
    {
        GameObject diceInstance = Instantiate(dicePrefab, spawnPoint.position, Quaternion.identity);
        diceInstance.transform.SetParent(spawnPoint); // Set the dice as a child of the spawn Point
        Dice dice = diceInstance.GetComponent<Dice>();
        dice.OnRollCompleted += HandleRollCompleted;
        dice.Roll();
    }

    private void HandleRollCompleted(int result)
    {
        int finalResult = result; // This is where I can apply modifiers

        // Log the final result
        Debug.Log("Dice roll result: " + finalResult);

        // DO something to the final result
    }
}
