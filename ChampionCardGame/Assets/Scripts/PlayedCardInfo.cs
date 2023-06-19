using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayedCardInfo
{
    public Card.EffectTypes Effect { get; }
    public int PlayerIndex { get; }
    public Card.TriggerTypes TriggerType { get; }
    public int EffectValue { get; }

    public PlayedCardInfo(Card.EffectTypes effect, int playerIndex, Card.TriggerTypes triggerType, int effectValue)
    {
        Effect = effect;
        PlayerIndex = playerIndex;
        TriggerType = triggerType;
        EffectValue = effectValue;
    }
}
