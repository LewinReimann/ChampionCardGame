using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HandLayout : MonoBehaviour
{
    public GameObject cardSlotPrefab;
    public List<Transform> cardSlots;
    public float spacing = 0.2f;
    public float startOffset = 0f;

    public event Action OnLayoutUpdated;

    public bool isWaitingForChild = false;

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

        // Invoke the event after updating the layout
        OnLayoutUpdated?.Invoke();
    }

    public void AddCardSlot()
    {
        // Set the flag to indicate that a child will be added soon
        isWaitingForChild = true;

        // Instantiate a new card slot and add it to the list of slots
        GameObject slotObject = Instantiate(cardSlotPrefab, transform);
        Transform slotTransform = slotObject.transform;
        cardSlots.Add(slotTransform);

        isWaitingForChild = false;
    }

    public void RemoveEmptyCardSlot()
    {
        for (int i = cardSlots.Count - 1; i >= 0; i--)
        {
            Transform cardSlot = cardSlots[i];
            if (cardSlot.childCount == 0 && !isWaitingForChild)
            {
                Destroy(cardSlots[i].gameObject);
                cardSlots.RemoveAt(i);
            }
        }
    }

    public void ChildAdded()
    {
        isWaitingForChild = false;
    }

    private void Update()
    {
        UpdateLayout();
        RemoveEmptyCardSlot();
    }
}
