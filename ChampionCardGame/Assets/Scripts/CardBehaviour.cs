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

    public CardManager appropriateCardManager;

    public bool isInPlay;

    private RoundManager roundManager;

    public CardDisplay cardDisplay;

    public PlayerHandLayout playerHandLayout;

    public int playerIndex;

    public DropZone correctDropZone;
    public EventDropZone correctEventDropZone;
    public ChampionDropZone correctChampionDropZone;

    public GameObject cardHider;

    public bool isMainChampion = false;


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
        appropriateCardManager = (playerIndex == 0) ? GameManager.instance.playerCardManager : GameManager.instance.opponentCardManager;

        PlayerHandLayout[] allPlayerHandLayouts = FindObjectsOfType<PlayerHandLayout>();
        foreach (var layout in allPlayerHandLayouts)
        {
            if (layout.playerIndex == playerIndex)
            {
                playerHandLayout = layout;
                break;
            }
        }

        // Find all instances and get the correct ones based on playerIndex
        ChampionDropZone[] allChampionDropZones = FindObjectsOfType<ChampionDropZone>();
        foreach (var zone in allChampionDropZones)
        {
            if (zone.playerIndex == playerIndex)
            {
                correctChampionDropZone = zone;
                break;
            }
        }

        DropZone[] allDropZones = FindObjectsOfType<DropZone>();
        foreach (var zone in allDropZones)
        {
            if (zone.playerIndex == playerIndex)
            {
                correctDropZone = zone;
                break;
            }
        }

        EventDropZone[] allEventDropZones = FindObjectsOfType<EventDropZone>();
        foreach (var zone in allEventDropZones)
        {
            if (zone.playerIndex == playerIndex)
            {
                correctEventDropZone = zone;
                break;
            }
        }

        if (GameManager.instance.currentGamePlayerIndex == playerIndex)
        {
            // Disable CardHider if the card belongs tothe current player
            cardHider.SetActive(false);
        }
        else
        {
            // Enable cardhider if the card does not belong to the current player
            cardHider.SetActive(true);
        }
    }

    public void HideCard()
    {
        if (isInPlay)
        {
            cardHider.SetActive(false);
        }
        else if (GameManager.instance.currentGamePlayerIndex == playerIndex)
        {
            // Disable CardHider if the card belongs tothe current player
            cardHider.SetActive(false);
        }
        else
        {
            // Enable cardhider if the card does not belong to the current player
            cardHider.SetActive(true);
        }
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
        if (!isInPlay && GameManager.instance.currentGamePlayerIndex == playerIndex)
        {
            isDragging = true;

            // Only enable the correct DropZone based on the player and the round phase.
            if (playerIndex == GameManager.instance.currentGamePlayerIndex)
            {
                if (roundManager.championPhaseActive && Type == Card.CardType.Champion)
                {
                    correctChampionDropZone.EnableCollider();
                }
                else if (roundManager.secondaryPhaseActive)
                {
                    correctDropZone.EnableCollider();
                    correctEventDropZone.EnableCollider();
                }
                else
                {

                }
            }

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
        if (!isInPlay && GameManager.instance.currentGamePlayerIndex == playerIndex)
        {
            
            isDragging = false;

            // Normal DropZone
            if (correctDropZone.IsInsideDropZone(transform.position) && roundManager.secondaryPhaseActive && Type != Card.CardType.Event && playerIndex == correctDropZone.playerIndex)
            {
                isMainChampion = false;

                originalParent = correctDropZone.transform;
                originalRotation = Quaternion.Euler(0, 0, 0);

                // Set the parent of the card to the dropZone
                transform.SetParent(originalParent, true);
                transform.localRotation = originalRotation;

                isInPlay = true;

                EventManager.Instance.RaiseEvent(Card.TriggerTypes.WheneverCardPlayed);

                // Inform cardmanager to move the card from hand to field
                appropriateCardManager.MoveCard(cardDisplay.card, Card.CardLocation.Hand, Card.CardLocation.Field);

                Invoke("UpdatePlayerHandLayout", 0.1f);
            }
            // Champion DropZone
            else if (correctChampionDropZone.IsInsideChampionDropZone(transform.position) && roundManager.championPhaseActive && !correctChampionDropZone.MainChampion && Type == Card.CardType.Champion && playerIndex == correctChampionDropZone.playerIndex)
            {
                isMainChampion = true;

                correctChampionDropZone.PlaceCardInChampionDropZone(this);

                isInPlay = true;

                // Inform cardmanager to move the card from hand to field
                appropriateCardManager.MoveCard(cardDisplay.card, Card.CardLocation.Hand, Card.CardLocation.Field);

                Invoke("UpdatePlayerHandLayout", 0.1f);

            }
            // Event DropZone
            else if (Type == Card.CardType.Event && correctEventDropZone.IsInsideEventDropZone(transform.position) && roundManager.secondaryPhaseActive && playerIndex == correctEventDropZone.playerIndex)
            {
                isMainChampion = false;

                originalParent = correctEventDropZone.transform;
                originalRotation = Quaternion.Euler(0, 180, 0);

                transform.SetParent(originalParent, true);
                transform.localRotation = originalRotation;

                isInPlay = true;

                appropriateCardManager.MoveCard(cardDisplay.card, Card.CardLocation.Hand, Card.CardLocation.Field);

                Invoke("UpdatePlayerHandLayout", 0.1f);
            }
            else
            {
                isMainChampion = false;

                // Restore the cards original position roptation and parent.
                transform.localPosition = originalPosition;
                transform.localRotation = originalRotation;
                transform.localScale = originalScale;
                transform.SetParent(originalParent, false);
            }
        }
        correctChampionDropZone.DisableCollider();
        correctDropZone.DisableCollider();
        correctEventDropZone.DisableCollider();

        if (isInPlay)
        {
            // CHeck if the card is a championCard
            if (cardDisplay.card.type == Card.CardType.Champion)
            {
                // Create the effectContext
                ActionController.EffectContext context;

                // Execute the primary or secodnary effect based on isMainChampion true or false
                if (isMainChampion)
                {
                    context = new ActionController.EffectContext()
                    {
                        Value = cardDisplay.card.effectValue,
                        playerIndex = playerIndex,
                        CardIndices = cardDisplay.card.summonCardIndices
                    };
                    ActionController.instance.ExecuteEffect(
                        cardDisplay.card.effect, null, // secondaryEffect is null
                        playerIndex, cardDisplay.card.trigger, null, // secondaryTrigger is null
                        cardDisplay.card.type, cardDisplay.card.effectValue, null, // secondaryEffectValue is null
                        cardDisplay.card.summonCardIndices, null, // secondarySummonCardIndices is null
                        context);
                }
                else
                {
                    context = new ActionController.EffectContext()
                    {
                        Value = cardDisplay.card.SecondaryEffectValue,
                        playerIndex = playerIndex,
                        CardIndices = cardDisplay.card.secondarySummonCardIndices
                    };
                    ActionController.instance.ExecuteEffect(
                        cardDisplay.card.secondaryEffect, null, // effect is null
                        playerIndex, cardDisplay.card.secondaryTrigger, null, // trigger is null
                        cardDisplay.card.type, null, cardDisplay.card.secondaryEffectValue, // effectValue is null
                        null, cardDisplay.card.secondarySummonCardIndices, // summonCardIndices is null
                        context);
                }
            }
            else // For non-champion cards
            {
                // Create an EffectContext and call ExecuteEffect
                ActionController.EffectContext context = new ActionController.EffectContext()
                {
                    Value = cardDisplay.card.effectValue,
                    playerIndex = playerIndex,
                    CardIndices = cardDisplay.card.summonCardIndices
                };

                // Check if the cards trigger type is CardPlayed
                if (cardDisplay.card.trigger == Card.TriggerTypes.CardPlayed)
                {
                    // Call the ActionController directly to execute the effect
                    ActionController.instance.ExecuteEffect(cardDisplay.card.effect, context);
                }
            }
            // The card has been played, inform ActionController
            if (cardDisplay.card.type == Card.CardType.Champion)
            {
                if (isMainChampion)
                {
                    ActionController.instance.RegisterPlayedCard(
                        cardDisplay.card.effect, 
                        playerIndex, cardDisplay.card.trigger, 
                        cardDisplay.card.type, cardDisplay.card.effectValue, 
                        cardDisplay.card.summonCardIndices, 
                        context);
                }
                else
                {
                    ActionController.instance.RegisterPlayedCard(
                        cardDisplay.card.secondaryEffect,
                        playerIndex,
                        cardDisplay.card.secondaryTrigger,
                        cardDisplay.card.type,
                        cardDisplay.card.secondaryEffectValue,
                        cardDisplay.card.secondarySummonCardIndices,
                        context);
                }
            }
        }
    }

    void UpdatePlayerHandLayout()
    {
        playerHandLayout.UpdateLayout();
    }
}
