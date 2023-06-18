using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventDropZone : MonoBehaviour
{
    public RoundManager roundManager;
    private BoxCollider zoneCollider;
    public int playerIndex;

    public PlayerHandLayout playerHandLayout;

    public SpriteRenderer highlight;

    private void Start()
    {
        zoneCollider = GetComponent<BoxCollider>();
    }

    public void DisableCollider()
    {
        if (zoneCollider != null)
        {
            zoneCollider.enabled = false;
        }
        highlight.enabled = false;
    }

    public void EnableCollider()
    {
        if (zoneCollider != null)
        {
            zoneCollider.enabled = true;
        }
        highlight.enabled = true;
    }

    public bool IsInsideEventDropZone(Vector3 position)
    {
        return roundManager.secondaryPhaseActive && zoneCollider.bounds.Contains(position);
    }
}
