using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameController : MonoBehaviourPunCallbacks
{
    public GameObject player1;
    public GameObject player2;

    private void OnEnable()
    {
        InitializePlayer();
    }

    private void InitializePlayer()
    {
        if (player1 != null && player2 != null)
        {
            // Assign the local player to player1 or player2  based on their Photon actor number
            if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
            {
                player1.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer);
                // Set up the player's game state here (e.g., assign player ID, initial hand, etc.)
            }
            else
            {
                player2.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer);
                // Set up the player's game state here (e.g., assign player ID, initial hand, etc.)
            }
        }
        else
        {
            Debug.LogError("Player 1 and or Player 2 are not assigend in GameController");
        }
    }

}
