using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    // Create a Dictionary to store effects by their CardLocation
    private Dictionary<CardLocation, List<Card>> effectsByLocation = new Dictionary<CardLocation, List<Card>>();

    public void RegisterEffect(Card card, CardLocation location)
    {
        // If the location is not in the dictionary, add an empty list for the effects
    }
}
