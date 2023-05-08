using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckController : MonoBehaviour
{
    public static DeckController Instance;

    public Deck customDeck;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
