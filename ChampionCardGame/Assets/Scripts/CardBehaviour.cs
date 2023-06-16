using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IEndDragHandler
{
    public Vector3 originalPosition;
    public Quaternion originalRotation;
    public Transform originalParent;
    public Vector3 originalScale;
    private bool isDragging = false;

    public CardManager cardManager;

    public bool isInPlay;

    private RoundManager roundManager;

    public CardDisplay cardDisplay;

    public PlayerHandLayout playerHandLayout;


    public Card.CardType Type
    {
        get
        {
            if (cardDisplay != null && cardDisplay.card != null)
            {
                return cardDisplay.card.type;
            }
            else
            {
                Debug.LogError("CardDisplay or Card object is null in cardbehaviour");
                return Card.CardType.Other;
            }
        }
    }

    public void Start()
    {
        roundManager = FindObjectOfType<RoundManager>();
        cardDisplay = GetComponent<CardDisplay>();
        playerHandLayout = FindObjectOfType<PlayerHandLayout>();
        cardManager = FindObjectOfType<CardManager>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isInPlay && !isDragging)
        {
            // Save the original position rotation and parent of the card
            originalPosition = transform.localPosition;
            originalRotation = transform.localRotation;
            originalScale = transform.localScale;
            originalParent = transform.parent;

            // Move the card up scale it up and bring it closer to the camera
            transform.localPosition += new Vector3(0, 1, -1);
            transform.localScale *= 1.2f;
            transform.localRotation = Quaternion.identity;

            // Make the card the last child so its on top of the other. BUT Not sure if we want that
            transform.SetAsLastSibling();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isInPlay && !isDragging)
        {
            // Restore the cards original postion, rotation and parent
            transform.localPosition = originalPosition;
            transform.localRotation = originalRotation;
            transform.localScale = originalScale;
            transform.SetParent(originalParent, false);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isInPlay)
        {
            isDragging = true;

            // The card follows the mouse cursor while dragging.
            Vector3 mousePosition = Input.mousePosition;

            // Convert the z component of the center of the hand from world space to local space
            Vector3 handCenter = transform.parent.position;
            float handCenterScreenZ = Camera.main.WorldToScreenPoint(handCenter).z;

            // Set the Z component to be slightly closer then the current distance.
            mousePosition.z = handCenterScreenZ;

            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = worldPosition;

            // Make the card the last child so its drawn on top of the others.
            transform.SetAsLastSibling();
        }
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isInPlay)
        {
            isDragging = false;

            ChampionDropZone championDropZone = FindObjectOfType<ChampionDropZone>();
            DropZone dropZone = FindObjectOfType<DropZone>();
            EventDropZone eventDropZone = FindObjectOfType<EventDropZone>();

            if (dropZone.IsInsideDropZone(transform.position) && roundManager.secondaryPhaseActive && Type != Card.CardType.Event)
            {
                originalParent = dropZone.transform;
                originalRotation = Quaternion.Euler(0, 0, 0);

                // Set the parent of the card to the dropZone
                transform.SetParent(originalParent, true);
                transform.localRotation = originalRotation;

                isInPlay = true;

                EventManager.Instance.RaiseEvent("WheneverCardPlayed");

                // Subscribe to the events AFTER the event was raised
                if (cardDisplay.card.cardTrigger != null)
                {
                    cardDisplay.card.cardEffect.Initialize(cardDisplay.card);

                    cardDisplay.card.cardTrigger.Initialize(cardDisplay.card);
                    cardDisplay.card.cardTrigger.SubscribeToEvents();
                }

                // Inform cardmanager to move the card from hand to field
                cardManager.MoveCard(cardDisplay.card, Card.CardLocation.Hand, Card.CardLocation.Field);

                Invoke("UpdatePlayerHandLayout", 0.1f);
            }
            else if (championDropZone.IsInsideChampionDropZone(transform.position) && roundManager.championPhaseActive && !championDropZone.MainChampion && Type == Card.CardType.Champion)
            {

                championDropZone.PlaceCardInChampionDropZone(this);

                isInPlay = true;

                // Inform cardmanager to move the card from hand to field
                cardManager.MoveCard(cardDisplay.card, Card.CardLocation.Hand, Card.CardLocation.Field);

                Invoke("UpdatePlayerHandLayout", 0.1f);

            }
            else if (Type == Card.CardType.Event && eventDropZone.IsInsideEventDropZone(transform.position) && roundManager.secondaryPhaseActive)
            {
                originalParent = eventDropZone.transform;
                originalRotation = Quaternion.Euler(0, 180, 0);

                transform.SetParent(originalParent, true);
                transform.localRotation = originalRotation;

                isInPlay = true;

                cardManager.MoveCard(cardDisplay.card, Card.CardLocation.Hand, Card.CardLocation.Field);

                Invoke("UpdatePlayerHandLayout", 0.1f);
            }
            else
            {

                // Restore the cards original position roptation and parent.
                transform.localPosition = originalPosition;
                transform.localRotation = originalRotation;
                transform.localScale = originalScale;
                transform.SetParent(originalParent, false);
            }
        }
    }

    void UpdatePlayerHandLayout()
    {
        playerHandLayout.UpdateLayout();
    }
}
