using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false; // start with the sprite renderer deactivated
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Card"))
        {
            spriteRenderer.enabled = true; // Activate the sprite renderer when the card enters the slot
            Debug.Log("Sprite renderer enabled: " + spriteRenderer.enabled);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Card"))
        {
            spriteRenderer.enabled = false; // deactivate the sprite Renderer when the card exits the collider
        }
    }

}
