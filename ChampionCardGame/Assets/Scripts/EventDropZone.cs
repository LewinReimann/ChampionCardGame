using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventDropZone : MonoBehaviour
{
    public RoundManager roundManager;
    private BoxCollider zoneCollider;
    public int playerIndex;

    public PlayerHandLayout playerHandLayout;

    private void Start()
    {
        zoneCollider = GetComponent<BoxCollider>();
    }

    public bool IsInsideEventDropZone(Vector3 position)
    {
        return roundManager.secondaryPhaseActive && zoneCollider.bounds.Contains(position);
    }
}
