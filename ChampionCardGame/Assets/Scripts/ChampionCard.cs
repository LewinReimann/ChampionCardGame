using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChampionCard : MonoBehaviour
{
    public int health;

    public GameManager gameManager;

    private void Start()
    {
        GetChampionHealth();

    }

    public void GetChampionHealth()
    {
        // Get the CardDisplay component attached to this GameObject
        CardDisplay cardDisplay = GetComponent<CardDisplay>();

        // Get the Card ScriptableObject from the CardDisplay component
        Card card = cardDisplay.card;

        // Get the health value from the Card ScriptableObject
        health = card.health;

        // Output the health value to the console
        Debug.Log("Health: " + health);
    }
}
