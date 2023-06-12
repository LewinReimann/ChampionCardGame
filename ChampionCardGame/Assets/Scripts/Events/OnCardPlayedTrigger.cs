using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New OnCardPlayedTrigger", menuName = "CardTriggers/OnCardPlayedTrigger")]
public class OnCardPlayedTrigger : CardTrigger
{
    
    public override bool IsTriggered(GameManager gameManager)
    {
        // This will be handled by GameManager when a card is played
        return true;
    }
}
