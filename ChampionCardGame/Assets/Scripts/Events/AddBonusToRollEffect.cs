using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBonusToRollEffect : ICardEffect
{
    private int bonus;

    public AddBonusToRollEffect(int bonus)
    {
        this.bonus = bonus;
    }

    public void ApplyEffect(GameManager gameManager)
    {
        // Add bonus to the dice roll.
        gameManager.playerRoll += bonus;
    }
}
