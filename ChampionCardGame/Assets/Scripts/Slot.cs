using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public bool isOccupied = false;
    public bool championSlot = false;

    public bool isColliding = false;

    private Round round;

    private CardDisplay cardDisplay;

    private void Start()
    {
   
        // Set the playerID based on the parents objects name
        if (transform.parent.name.Contains("Player1"))
        {
            GameManager.Instance.playerID = 1;
        }
        else if (transform.parent.name.Contains("Player2"))
        {
            GameManager.Instance.playerID = 2;
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false; // start with the sprite renderer deactivated

        round = FindObjectOfType<Round>();
    }

    public void SetOccupied(bool occupied)
    {
        if (occupied)
        {
            isOccupied = true;
        }
        else
        {
            isOccupied = false;
        }

    }

    private void Update()
    {
        
        UpdateHighlight();
        
    }

    public void DeactivateHighlight()
    {
        isColliding = false;
    }

    private void UpdateHighlight()
    {
      if (isOccupied || isColliding)
        {
            spriteRenderer.enabled = true;
        }
      else
        {
            spriteRenderer.enabled = false;
        }
    }

    public void SetColliderEnabled(bool enabled)
    {
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = enabled;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
      
        if (!round.CanPlayCards(GameManager.Instance.playerID)) return;

        if (other.CompareTag("Card"))
        {
            // Check if the required conditions are met for triggering
            if ((championSlot && round.championPhaseActive) || (!championSlot && !round.championPhaseActive))
            {
                isColliding = true;
                UpdateHighlight();
            }
         
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!round.CanPlayCards(GameManager.Instance.playerID)) return;

        if (other.CompareTag("Card"))
        {
            // Check if the required conditions are met for triggering
            if ((championSlot && round.championPhaseActive) || (!championSlot && !round.championPhaseActive))
            {
                isColliding = false;
                UpdateHighlight();
            }
                
        }
    }

    public bool IsCardSlotAvailable(Transform cardTransform)
    {
        GameObject[] dropZones = GameObject.FindGameObjectsWithTag("DropZone");
        foreach (GameObject dropZone in dropZones)
        {
            float distance = Vector3.Distance(cardTransform.position, dropZone.transform.position);
            float dropZoneRadius = 2f;
            if (distance < dropZoneRadius)
            {
                Slot slot = dropZone.GetComponent<Slot>();
                if (slot != null && slot.spriteRenderer.enabled == true && slot.isOccupied == false)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public static bool CheckIfChampionSlotsAreOccupied()
    {
        GameObject[] dropZones = GameObject.FindGameObjectsWithTag("DropZone");
        int occupiedChampionSlots = 0;

        foreach (GameObject dropZone in dropZones)
        {
            Slot slot = dropZone.GetComponent<Slot>();
            if (slot.championSlot && slot.isOccupied)
            {
                occupiedChampionSlots++;
            }
        }

        return (occupiedChampionSlots >= 2);
    }

}
