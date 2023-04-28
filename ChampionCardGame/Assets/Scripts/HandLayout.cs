using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandLayout : MonoBehaviour
{
    public GameObject cardSlotPrefab;
    public List<Transform> cardSlots;
    public float spacing = 0.2f;
    public float startOffset = 0f;

    private void Start()
    {
        UpdateLayout();
    }

    public void UpdateLayout()
    {
        // Calculate the total width of the cards in the hand
        float totalWidth = cardSlots.Count * spacing;

        // Calculate the starting position for the first card slot
        float startX = -totalWidth / 2f + startOffset;

        // Loop through each card slot and update its position
        for (int i = 0; i < cardSlots.Count; i++)
        {
            Transform cardSlot = cardSlots[i];

            // Calculate the position of the card slot based on its index
            float cardX = startX + i * spacing;
            Vector3 cardPos = new Vector3(cardX, 0f, 0f);

            // Set the position of the card slot
            cardSlot.position = transform.TransformPoint(cardPos);
        }
    }

    public void AddCardSlot()
    {
        
        // Instantiate a new card slot and add it to the list of slots
        GameObject slotObject = Instantiate(cardSlotPrefab, transform);
        Transform slotTransform = slotObject.transform;
        cardSlots.Add(slotTransform);

        // Update the layout to accommodate the new slot
        UpdateLayout();
    }

    public void RemoveCardSlot()
    {
        
        // Destroy the last card slot in the list
        int lastIndex = cardSlots.Count - 1;
        Destroy(cardSlots[lastIndex].gameObject);
        cardSlots.RemoveAt(lastIndex);

        // Update the layout to remove the destroyed slot
        UpdateLayout();
    }
}
