using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHover : MonoBehaviour
{
    

    public bool isHovering;
    private HandLayout handLayout;

    private Draggable draggable;

    private void OnMouseEnter()
    {
        if (transform.parent.CompareTag("Hand"))
        {
        isHovering = true;
        transform.localScale *= 1.2f; // scale up the card when hovering
        transform.position += new Vector3(0f, 0.5f, 1f);
        }
    }

    public void OnMouseExit()
    {
        if (transform.parent.CompareTag("Hand"))
        {
            isHovering = false;
            SetBackToOriginal();
        }
    }

    public void SetBackToOriginal()
    {
        transform.localPosition = new Vector3(0f, 0f, 0f);
        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }

    private void Start()
    {

        draggable = GetComponent<Draggable>();

        Transform current = transform;
        HandLayout handLayout = null;

        while (current.parent != null && handLayout == null)
        {
            current = current.parent;
            handLayout = current.GetComponent<HandLayout>();
        }

        if (handLayout != null)
        {
            // HandLayout was found
        }
        else
        {
            // HandLayout was not found
        }

    }

    private void OnDestroy()
    {
       
    }
}
