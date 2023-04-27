using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHover : MonoBehaviour
{
    public bool isInHand = false; // set this to true if the card is in the hand

    private bool isHovering;

    private void OnMouseEnter()
    {
        if (transform.parent.CompareTag("Hand"))
        {
        isHovering = true;
        transform.localScale *= 1.2f; // scale up the card when hovering
        transform.position += new Vector3(0f, 0.5f, -0.5f);
        }
    }

    private void OnMouseExit()
    {
        if (transform.parent.CompareTag("Hand"))
        {
            isHovering = false;
            transform.localScale /= 1.2f; // scale down the card when not hovering
            transform.position -= new Vector3(0f, 0.5f, -0.5f);
        }
    }
}
