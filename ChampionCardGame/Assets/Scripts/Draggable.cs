using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    
    private bool isDragging = false;
    private Vector3 screenPoint;
    private Vector3 offset;
    private Vector3 originalPosition;

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
        }
    }

    private void OnMouseUp()
    {
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
        if (nearestDropZoneDistance < dropZoneRadius && !slot.isOccupied)
        {
            Round round = FindObjectOfType<Round>();
            
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

                    transform.position = nearestDropZone.transform.position;
                    transform.SetParent(nearestDropZone.transform);
                    slot.isOccupied = true;

                    // set the scale and rotation of the object after its dropped
                    transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    transform.rotation = Quaternion.identity;
                }

                // check if it is a ChampionSlot and championPhaseActive is false
                else if (slot.championSlot && !round.championPhaseActive)
                {
                    // move the card back to its original position
                    transform.position = originalPosition;

                }

                // check if it is NOT a ChampionSlot and championPhaseActive is true
                else if(!slot.championSlot && round.championPhaseActive)
                {
                    // move card back to its orignal position
                    transform.position = originalPosition;
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
                    transform.rotation = Quaternion.identity;
                }

            }
        }
          
        

        // Otherwise, return the card to tis original position
        else
        {
            transform.position = originalPosition;
        }

        isDragging = false;

    }
}
