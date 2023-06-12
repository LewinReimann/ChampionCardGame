using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDiceRollTrigger : ICardTrigger
{
    public bool IsTriggered(GameManager gameManager)
    {
        // return true if you detect a dice roll event
        // you can add more logic here
        return true;
    }
}
