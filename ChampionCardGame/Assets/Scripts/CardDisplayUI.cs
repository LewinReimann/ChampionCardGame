using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CardDisplayUI : MonoBehaviour
{

    public Card card;

    public TMP_Text cardNameText;

    public Image cardArtworkSprite;

    public TMP_Text championEffectText;
    public TMP_Text secondaryEffectText;

    public TMP_Text healthText;


    // Start is called before the first frame update
    void Start()
    {
       
        cardNameText.text = card.cardName;

        cardArtworkSprite.sprite = card.cardArtwork;

        championEffectText.text = card.championEffect;
        secondaryEffectText.text = card.secondaryEffect;

        healthText.text = card.health.ToString();

    }
}
