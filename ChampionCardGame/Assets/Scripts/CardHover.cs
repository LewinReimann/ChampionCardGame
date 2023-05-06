using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHover : MonoBehaviour
{
    

    public bool isHovering;
    private Vector3 originalPosition;
    private Vector3 originalScale;
    private HandLayout handLayout;

    private void OnMouseEnter()
    {
        if (transform.parent.CompareTag("Hand"))
        {
        isHovering = true;
        transform.localScale *= 1.2f; // scale up the card when hovering
        transform.position += new Vector3(0f, 0.5f, 1f);
        }
    }

    private void OnMouseExit()
    {
        if (transform.parent.CompareTag("Hand"))
        {
            isHovering = false;
            transform.localScale /= 1.2f; // scale down the card when not hovering
            transform.position -= new Vector3(0f, 0.5f, 1f);
        }
    }

    private void Start()
    {
        handLayout = transform.parent.GetComponent<HandLayout>();
        if (handLayout != null)
        {
            handLayout.OnLayoutUpdated += UpdateOriginalValues;
            UpdateOriginalValues();
        }
    }

    private void OnDestroy()
    {
        if (handLayout != null)
        {
            handLayout.OnLayoutUpdated -= UpdateOriginalValues;
        }
    }

    private void UpdateOriginalValues()
    {
        originalPosition = transform.position;
        originalScale = transform.localScale;
    }
}
