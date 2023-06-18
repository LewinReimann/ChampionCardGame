using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChampionDropZone : MonoBehaviour
{
    public RoundManager roundManager;
    public GameManager gameManager;
    private BoxCollider zoneCollider;
    public PlayerHandLayout playerHandLayout;

    public CardBehaviour cardInChampionZone;
    public int playerIndex;
    public bool MainChampion;

    public SpriteRenderer highlight;

    private void Start()
    {
        zoneCollider = GetComponent<BoxCollider>();
        MainChampion = false;
    }

    public void DisableCollider()
    {
        if (zoneCollider != null)
        {
            zoneCollider.enabled = false;
        }
        highlight.enabled = false;
    }

    public void EnableCollider()
    {
        if (zoneCollider != null)
        {
            zoneCollider.enabled = true;
        }
        highlight.enabled = true;
    }

    public bool IsInsideChampionDropZone(Vector3 position)
    {
        return roundManager.championPhaseActive && zoneCollider.bounds.Contains(position);
    }

    public void PlaceCardInChampionDropZone(CardBehaviour cardBehaviour)
    {
        
        if (roundManager.championPhaseActive && cardBehaviour.Type == Card.CardType.Champion)
        {
            if (cardInChampionZone != null)
            {
                // return the previous card to the original position
                cardInChampionZone.transform.SetParent(cardInChampionZone.originalParent);
                cardInChampionZone.transform.localPosition = cardInChampionZone.originalPosition;
                cardInChampionZone.transform.localRotation = Quaternion.Euler(0, 0, 0);
                cardInChampionZone.transform.localScale = cardInChampionZone.originalScale;
            }
            
            cardBehaviour.transform.SetParent(transform);
            cardBehaviour.transform.localPosition = Vector3.zero;
            cardBehaviour.transform.localRotation = Quaternion.Euler(0, 0, 0);
            cardInChampionZone = cardBehaviour;

            Card card = cardBehaviour.cardDisplay.card;

            // Set the appropraite champion health in gameManager
            if (playerIndex == 0)
            {
                gameManager.playerChampionHealth = card.health;
            }
            else if (playerIndex == 1)
            {
                gameManager.opponentChampionHealth = card.health;
            }
            

            MainChampion = true;

            // Notify the round manager to check if both main champs are placed
            roundManager.MainChampionCheck();
        }
    }
}
