using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.MultiplayerModels;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager Instance { get; private set; }
    public string entityId;
    public string opponentEntityId; // assign in Unity Editor

    public Animator transition;

    public void StoreEntityId(string id)
    {
        entityId = id;
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Connected to Master");
    }

    public void SearchForOpponent()
    {
        StartCoroutine("Matchmaking");
    }

    IEnumerator Matchmaking()
    {
        while (PhotonNetwork.CurrentRoom == null || PhotonNetwork.CurrentRoom.PlayerCount != 2)
        {
            PhotonNetwork.JoinRandomRoom();

            yield return new WaitForSeconds(5);

            if (PhotonNetwork.CurrentRoom == null || PhotonNetwork.CurrentRoom.PlayerCount != 2)
            {
                PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 2 });

                yield return new WaitForSeconds(5);
            }
        }

        LoadScene("GameScene");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join random room: " + message);
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 2 });
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log($"Disconnected due to: {cause}");
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(Transition(sceneName));
    }

    IEnumerator Transition(string sceneName)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(2);

        SceneManager.LoadScene(sceneName);
        transition.SetTrigger("End");
    }
}
