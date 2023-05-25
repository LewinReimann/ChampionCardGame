using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    private bool attemptingToLoadGameScene = false;
    private bool isSearching = false;





    // Start is called before the first frame update
    void Start()
    {
        // Connect to Photons Server
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("ConnectedToMaster");
    }

    public void StartSearch()
    {
        isSearching = true;
        StartCoroutine(SearchForOpponent());
    }

    private IEnumerator SearchForOpponent()
    {
        while (isSearching)
        {
            PhotonNetwork.JoinRandomRoom(); // Will call the OnJoinRandomFailed if it can not find something
            yield return new WaitForSeconds(Random.Range(3, 6));
        }
    }

    public void StopSearch()
    {
        isSearching = false;
    }

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom("RoomName", new RoomOptions { MaxPlayers = 2 });
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join random room " + message);
        StartCoroutine(HostGame());
    }

    private IEnumerator HostGame()
    {
        CreateRoom();
        yield return new WaitForSeconds(Random.Range(3, 6));

        if (PhotonNetwork.CurrentRoom.PlayerCount < 2)
        {
            PhotonNetwork.LeaveRoom();
        }
        else
        {
            StopSearch();
        }
    }
}
