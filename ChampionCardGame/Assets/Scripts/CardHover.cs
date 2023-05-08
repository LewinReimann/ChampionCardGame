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

        handLayout = transform.parent.GetComponent<HandLayout>();
       
    }

    private void OnDestroy()
    {
       
    }
}
