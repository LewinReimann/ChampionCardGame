using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardTrigger : ScriptableObject
{
    public abstract bool IsTriggered(GameManager gameManager);
}
