using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICardTrigger
{
    bool IsTriggered(GameManager gameManager);
}
