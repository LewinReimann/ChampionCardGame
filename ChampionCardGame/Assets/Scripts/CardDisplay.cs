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
    public TextMeshPro championHealthText;

    public GameManager gameManager;

    public bool isChampion = false;
    public bool isInPlay = false;

    public bool isUpdateEnabled = false;

    public int playerID = 0;

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

    // Deactivate the ol Health display with a new one where we get our information from the GameManager

    public void ActivateGameManagerHealth()
    {
        isUpdateEnabled = true;
    }

    public void DeactivateGameManagerHealth()
    {
        isUpdateEnabled = false;
    }

    private void Update()
    {
        if (isUpdateEnabled)
        {
            // Check if it's a champion card
            if (isChampion)
            {
                // Deactivate the HealthText and activate the ChampionHealthText
                transform.Find("HealthText").gameObject.SetActive(false);
                transform.Find("ChampionHealthText").gameObject.SetActive(true);

                // Get the ChampionHealth from the GameManager and set it as the text for the ChampionHealthText
                GameManager gameManager = FindObjectOfType<GameManager>();
                if (gameManager != null)
                {
                    int championHealth = playerID == 1 ? gameManager.player1ChampionHealth : gameManager.player2ChampionHealth;
                    
                    transform.Find("ChampionHealthText").GetComponent<TextMeshPro>().text = championHealth.ToString();
                }
            }
            else
            {
                // Deactivate the ChampionHealthText and activate the HealthText
                transform.Find("ChampionHealthText").gameObject.SetActive(false);
                transform.Find("HealthText").gameObject.SetActive(true);
            }
        }
    }
   
    
}
