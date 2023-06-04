using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandLayout : MonoBehaviour
{
    public GameObject cardPrefab;
    public List<Transform> handSlot;
    public CardManager cardManager;

    public float spacing = 0.2f;
    public float startOffset = -5f;

    public void UpdateLayout()
    {
        float totalWidth = handSlot.Count * spacing;
        float startX = -totalWidth / 2f + startOffset;
        int centerIndex = handSlot.Count / 2;

        for (int i = 0; i < handSlot.Count; i++)
        {
            Transform card = handSlot[i];

            float cardX = startX + i * spacing;
            float cardZOffset = i * -0.1f;
            float yOffsetAmount = Mathf.Abs(centerIndex - i) * 0.2f;

            Vector3 cardPos = new Vector3(cardX, -yOffsetAmount, cardZOffset);
            card.localPosition = cardPos;

            float rotationAmount = (centerIndex - i) * 10f;
            Quaternion localRotation = Quaternion.Euler(0f, 0f, rotationAmount);
            card.localRotation = localRotation;
        }
    }
}
