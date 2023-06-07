using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IEndDragHandler
{
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Transform originalParent;
    private Vector3 originalScale;
    private bool isDragging = false;

    public CardDisplay cardDisplay;

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
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isDragging)
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
        if (!isDragging)
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

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;

        DropZone dropZone = FindObjectOfType<DropZone>();
        if (dropZone.IsInsideDropZone(transform.position))
        {
            originalParent = dropZone.transform;
            originalRotation = Quaternion.Euler(0, 0, 0);
        }
        // Restore the cards original position roptation and parent.
        transform.localPosition = originalPosition;
        transform.localRotation = originalRotation;
        transform.localScale = originalScale;
        transform.SetParent(originalParent, false);
    }
}
