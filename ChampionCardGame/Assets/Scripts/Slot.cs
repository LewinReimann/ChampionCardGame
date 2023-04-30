using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public bool isOccupied = false;
    public bool championSlot = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false; // start with the sprite renderer deactivated
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

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Card"))
        {
            CardDisplay cardDisplay = other.GetComponent<CardDisplay>();
            if (cardDisplay.isInPlay == false)
            {
                spriteRenderer.enabled = true; // Activate the sprite renderer when the card enters the slot
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Card"))
        {
            spriteRenderer.enabled = false; // deactivate the sprite Renderer when the card exits the collider
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

}
