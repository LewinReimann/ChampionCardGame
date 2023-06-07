using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{
    public TMP_Text playerRollText;
    public TMP_Text opponentRollText;
    public TMP_Text playersLifeText;
    public TMP_Text opponentLifeText;
    public int playerRoll = 0;
    public int opponentRoll = 0;
    public int playersLife = 5;
    public int opponentLife = 5;
    public bool isPlayerTurn = false;

    new private PhotonView photonView;

    public CardManager cardManager;

    void Start()
    {
        photonView = GetComponent<PhotonView>();

        // Determine which player goes first
        if (PhotonNetwork.IsMasterClient)
        {
            isPlayerTurn = Random.Range(0, 2) == 0;
        }

        playersLifeText.text = playersLife.ToString();

        cardManager.InitializeDeck();
    }
}
