using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropZone : MonoBehaviour
{
    public RoundManager roundManager;
    private BoxCollider zoneCollider;

    public PlayerHandLayout playerHandLayout;

    private void Start()
    {
        zoneCollider = GetComponent<BoxCollider>();
    }

    public bool IsInsideDropZone(Vector3 position)
    {


        return roundManager.secondaryPhaseActive && zoneCollider.bounds.Contains(position);

        
    }
}
