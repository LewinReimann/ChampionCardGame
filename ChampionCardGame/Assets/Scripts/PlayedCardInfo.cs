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
    public List<int> SummonCardIndices { get; set; }
    public ActionController.EffectContext Context { get; set; }

    public PlayedCardInfo(Card.EffectTypes effect, int playerIndex, Card.TriggerTypes triggerType, int effectValue, List<int> summonCardIndices, ActionController.EffectContext context)
    {
        Effect = effect;
        PlayerIndex = playerIndex;
        TriggerType = triggerType;
        EffectValue = effectValue;
        SummonCardIndices = summonCardIndices;

        // Assign the EffectContext
        this.Context = context;
    }
}
