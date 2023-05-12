using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    private bool attemptingToLoadGameScene = false;

    private void Start()
    {
        // Connect to Photon's server
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
    }

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom("RoomName", new RoomOptions { MaxPlayers = 2});
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom("RoomName");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room: " + PhotonNetwork.CurrentRoom.Name);
        attemptingToLoadGameScene = true;

        // GameManager is a singleton and its already initialized at this point
        GameManager.Instance.SetPlayer1(PhotonNetwork.LocalPlayer);

        foreach (Photon.Realtime.Player otherPlayer in PhotonNetwork.CurrentRoom.Players.Values)
        {
            if (otherPlayer != PhotonNetwork.LocalPlayer)
            {
                GameManager.Instance.SetPlayer2(otherPlayer);
                break;
            }
        }
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join room: " + message);
    }

    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join random room: " + message);
        CreateRoom();
    }

    public void LoadGameScene()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            PhotonNetwork.LoadLevel("GameScene");
        }
    }

    public void Update()
    {
        if (attemptingToLoadGameScene)
        {
            LoadGameScene();
        }
    }

}
