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
    public bool isChampion = false;
    public bool isInPlay = false;

    public bool isUpdateEnabled = false;


    private bool playerIdSet = false;

    // Let's assume you have two Photon.Realtime.Player objects in your GameManager
    Photon.Realtime.Player player1;
    Photon.Realtime.Player player2;

    // Start is called before the first frame update
    void Start()
    {

        if (!playerIdSet)
        {
            Debug.Log("The first !playerIDSet is functioning");
            Transform currentTransform = transform;

            while (currentTransform.parent != null)
            {
                currentTransform = currentTransform.parent;

                if (currentTransform.name == "Player1")
                {
                    Debug.Log("The currentTransform for Player1 is called correctly");
                    // Transfer ownership to the player who is player1
                    photonView.TransferOwnership(GameManager.Instance.GetPlayer1ActorNumber());
                    playerIdSet = true;
                    break;
                }
                else if (currentTransform.name == "Player2")
                {
                    Debug.Log("The currentTransform for Player2 is called correctly");

                    // Transfer ownership to the player who is player2
                    photonView.TransferOwnership(GameManager.Instance.GetPlayer2ActorNumber());
                    playerIdSet = true;
                    break;
                }
            }
        }

        
        if (card != null)
        {
            Debug.Log("Is the card not null? and is this filled in then");

            cardNameText.text = card.cardName;
            cardArtworkSprite.sprite = card.cardArtwork;
            championEffectText.text = card.championEffect;
            secondaryEffectText.text = card.secondaryEffect;
            healthText.text = card.health.ToString();
        }
        else
        {
            Debug.LogError("Card object is null in CardDisplay.Start()");
        }
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
                if (GameManager.Instance != null)
                {
                    int championHealth = GameManager.Instance.playerID == 1 ? GameManager.Instance.player1ChampionHealth : GameManager.Instance.player2ChampionHealth;
                    
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
