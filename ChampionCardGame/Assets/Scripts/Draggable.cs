using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    
    public bool isDragging = false;
    private Vector3 screenPoint;
    private Vector3 offset;
    private Vector3 originalPosition;

    private Round round;

    private CardHover cardHover;

    public CardDisplay cardDisplay;

    public bool IsDragging { get; private set; }

    private void Start()
    {
        cardHover = GetComponent<CardHover>();
        round = FindObjectOfType<Round>();
    }


    private void OnMouseDown()
    {

        isDragging = true;
            originalPosition = transform.position;
            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

    }

    private void OnMouseDrag()
    {
        
            if (isDragging)
            {
                Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
                Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
                transform.position = curPosition;
            transform.rotation = Quaternion.Euler(90f, 0f, 0f);

        }
        
       
    }

    private void OnMouseUp()
    {
        transform.localRotation = Quaternion.Euler(0f, 0f, 0f);

        // Find the nearet drop zone
        GameObject[] dropZones = GameObject.FindGameObjectsWithTag("DropZone");
            GameObject nearestDropZone = null;
            float nearestDropZoneDistance = Mathf.Infinity;
            foreach (GameObject dropZone in dropZones)
            {
                float distance = Vector3.Distance(transform.position, dropZone.transform.position);
                if (distance < nearestDropZoneDistance)
                {
                    nearestDropZone = dropZone;
                    nearestDropZoneDistance = distance;
                }
            }

            // If the nearest drop zone is within a certain range, the card will snap to its position
            float dropZoneRadius = 2f;
            Slot slot = nearestDropZone.GetComponent<Slot>();
            if (nearestDropZoneDistance < dropZoneRadius && !slot.isOccupied && round.CanPlayCards(cardDisplay.playerID) && cardDisplay.playerID == slot.playerID)
            {

                Hand hand = GetComponentInParent<Hand>();
                if (hand != null)
                {
                    CardDisplay cardDisplay = GetComponent<CardDisplay>();
                    Card card = cardDisplay.card;

                    // check if it is a ChampionSlot and championPhaseActive is true
                    if (slot.championSlot && round.championPhaseActive)
                    {
                        // set the card as a Champion and play it
                        cardDisplay.isChampion = true;
                        cardDisplay.isInPlay = true;
                        hand.PlayCard(card);

                     

                        // Add the ChampionCard Component
                        ChampionCard championCard = gameObject.AddComponent<ChampionCard>();
                        championCard.GetChampionHealth();

                        transform.position = nearestDropZone.transform.position;
                        transform.SetParent(nearestDropZone.transform);
                        slot.isOccupied = true;

                        // set the scale and rotation of the object after its dropped
                        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                        // transform.rotation = Quaternion.identity;


                    }

                    // check if it is a ChampionSlot and championPhaseActive is false
                    else if (slot.championSlot && !round.championPhaseActive)
                    {
                        // move the card back to its original position
                        transform.position = originalPosition;
                    cardHover.SetBackToOriginal();

                    }

                    // check if it is NOT a ChampionSlot and championPhaseActive is true
                    else if (!slot.championSlot && round.championPhaseActive)
                    {
                    // move card back to its orignal position
                    cardHover.SetBackToOriginal();
                }

                    // check if it is NOT a ChampionSlot and championPhaseActiv is false
                    else
                    {
                        cardDisplay.isInPlay = true;
                        hand.PlayCard(card);

                        transform.position = nearestDropZone.transform.position;
                        transform.SetParent(nearestDropZone.transform);
                        slot.isOccupied = true;

                        // set the scale and rotation of the object after its dropped
                        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                    // transform.rotation = Quaternion.identity;
                }

                    if (Slot.CheckIfChampionSlotsAreOccupied() && !round.championSlotsChecked)
                    {
                        // Activate isUpdateEnabled for both ChampionCards
                        CardDisplay[] allCardDisplays = FindObjectsOfType<CardDisplay>();
                        foreach (CardDisplay display in allCardDisplays)
                        {
                            if (display.isChampion)
                            {
                                display.ActivateGameManagerHealth();

                            }
                        }
                        round.championSlotsChecked = true;
                        round.EndChampionPhase();
                    }

                }



            }
            // Otherwise, return the card to tis original position
            else
            {
                cardHover.SetBackToOriginal();
            }

            isDragging = false;
        
    }
}
