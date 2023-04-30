using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardDisplay : MonoBehaviour
{
    
    public Card card;

    public TextMeshPro cardNameText;

    public SpriteRenderer cardArtworkSprite;

    public TextMeshPro championEffectText;
    public TextMeshPro secondaryEffectText;

    public TextMeshPro healthText;

    public GameManager gameManager;

    public bool isChampion = false;
    public bool isInPlay = false;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        cardNameText.text = card.cardName;

        cardArtworkSprite.sprite = card.cardArtwork;

        championEffectText.text = card.championEffect;
        secondaryEffectText.text = card.secondaryEffect;

        healthText.text = card.health.ToString();
    }

    ///////////// CARD HANDLING ///////////////// 
   
    
}
