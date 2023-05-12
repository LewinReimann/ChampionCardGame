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
                // Here, we make sure that playerID matches ActorNumber
                GameManager.Instance.playerID = PhotonNetwork.LocalPlayer.ActorNumber;
            }
            else
            {
                player2.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer);
                // Here, we make sure that playerID matches ActorNumber
                GameManager.Instance.playerID = PhotonNetwork.LocalPlayer.ActorNumber;
            }
        }
        else
        {
            Debug.LogError("Player 1 and or Player 2 are not assigned in GameController");
        }
    }

    private void TransferOwnershipToPlayer(GameObject player, Photon.Realtime.Player newOwner)
    {
        // Transfer ownership of the player object itself
        var photonView = player.GetComponent<PhotonView>();
        if (photonView != null)
        {
            photonView.TransferOwnership(newOwner);
        }

        // Transfer ownership of all child objects that have a PhotonView component
        foreach (var childPhotonView in player.GetComponentsInChildren<PhotonView>())
        {
            childPhotonView.TransferOwnership(newOwner);
        }

    }
}
