using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class CardDisplay : MonoBehaviourPun
{
    public Card card;

    public TextMeshPro cardNameText;
    public SpriteRenderer cardArtworkSprite;
    public TextMeshPro championEffectText;
    public TextMeshPro secondaryEffectText;
    public TextMeshPro healthText;
    public TextMeshPro championHealthText;
    public TextMeshPro factionText;

    public bool isChampion = false;
    public bool isInPlay = false;
    public bool updateEnabled = false;

    private GameObject healthTextGameObject;
    private GameObject championHealthTextGameObject;

    private bool playerIdSet = false;

    void Start()
    {
        if (card != null)
        {
            cardNameText.text = card.cardName;
            cardArtworkSprite.sprite = card.cardArtwork;
            championEffectText.text = card.championEffectText;
            secondaryEffectText.text = card.secondaryEffectText;
            healthText.text = card.health.ToString();
            factionText.text = card.faction.ToString();
        }
        else
        {
            Debug.LogError("Card object is null in CardDisplay.Start()");
        }

        healthTextGameObject = transform.Find("HealthText").gameObject;
        championHealthTextGameObject = transform.Find("ChampionHealthText").gameObject;
    }

    public void ActivateChampionHealth()
    {
        updateEnabled = true;

        healthTextGameObject.SetActive(false);
        championHealthTextGameObject.SetActive(true);
    }

    public void DeactivateChampionHealth()
    {
        updateEnabled = false;

        healthTextGameObject.SetActive(true);
        championHealthTextGameObject.SetActive(false);
    }

    private void Update()
    {
        if (updateEnabled)
        {
            if (isChampion)
            {
                

             
            }
        }
    }
    
}
