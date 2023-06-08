using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventFieldManager : MonoBehaviour
{
    public float cardSpacing = 2f;

    public float baseSpacing = 10f;
    public float minSpacing = 1f;
    public float maxSpacing = 10f;

    private void Update()
    {
        RearrangeCards();
    }

    private void RearrangeCards()
    {
        // Find all child cards of the object (drop zone)
        List<Transform> cards = new List<Transform>();
        for (int i = 0; i < transform.childCount; i++)
        {
            cards.Add(transform.GetChild(i));
        }

        // Calculate a dynamic card spacing, based on the number of cards
        // This will decrease the spacing as the number of cards increases
        float dynamicCardSpacing = Mathf.Clamp(baseSpacing / (cards.Count + 1), minSpacing, maxSpacing);

        // Determine the total width of the layout
        float totalHeight = dynamicCardSpacing * (cards.Count - 1);

        // Position each card
        for (int i = 0; i < cards.Count; i++)
        {
            float yPosition = (dynamicCardSpacing * i) - (totalHeight / 2);
            cards[i].localPosition = new Vector3(0, yPosition, 0);
        }
    }
}
