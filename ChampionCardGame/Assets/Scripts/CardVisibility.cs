using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CardVisibility : MonoBehaviourPun
{
    public GameObject cardHider;


    private void Start()
    {


        // CHeck if the local player is the owner of the card
        if (!photonView.IsMine)
        {
            // If not the owner, hide the card behind the CardHider
            cardHider.SetActive(false);
        }
        else
        {
            // If the owner, show the card face
            cardHider.SetActive(true);
        }
    }
}
