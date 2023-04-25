using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public string cardName;

    public Sprite cardArtwork;

    public string championEffect;
    public string secondaryEffect;

    public int health;
}
