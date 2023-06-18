using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayedCard
{
    public Card Card { get; }
    public Card.CardType Type { get; }
    public int PlayOrder { get; }

    public PlayedCard(Card card, Card.CardType type, int playOrder)
    {
        Card = card;
        Type = type;
        PlayOrder = playOrder;
    }
}
