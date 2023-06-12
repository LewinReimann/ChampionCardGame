using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DrawCardEffect", menuName = "CardEffects/DrawCardEffect")]
public class DrawCardEffect : CardEffect
{
    public override void ApplyEffect(GameManager gameManager)
    {
        // call the DrawCard method in GameManager
        gameManager.cardManager.DrawCard();
    }
}
