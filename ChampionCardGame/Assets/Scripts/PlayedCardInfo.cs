#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayedCardInfo
{
    public Card.EffectTypes Effect { get; }
    public Card.EffectTypes? SecondaryEffect { get; }
    public int PlayerIndex { get; }
    public Card.TriggerTypes TriggerType { get; }
    public Card.TriggerTypes? SecondaryTriggerType { get; }
    public int EffectValue { get; }
    public int? SecondaryEffectValue { get; }
    public List<int> SummonCardIndices { get; set; }
    public List<int>? SecondarySummonCardIndices { get; set; }
    public ActionController.EffectContext Context { get; set; }

    public PlayedCardInfo(
        Card.EffectTypes effect, Card.EffectTypes? secondaryEffect,
        int playerIndex,
        Card.TriggerTypes triggerType, Card.TriggerTypes? secondaryTriggerType,
        int effectValue, int? secondaryEffectValue,
        List<int> summonCardIndices, List<int>? secondarySummonCardIndices,
        ActionController.EffectContext context)
    {
        Effect = effect;
        SecondaryEffect = secondaryEffect;
        PlayerIndex = playerIndex;
        TriggerType = triggerType;
        SecondaryTriggerType = secondaryTriggerType;
        EffectValue = effectValue;
        SecondaryEffectValue = secondaryEffectValue;
        SummonCardIndices = summonCardIndices;
        SecondarySummonCardIndices = secondarySummonCardIndices;
        
        // Assign the EffectContext
        this.Context = context;
    }
}
