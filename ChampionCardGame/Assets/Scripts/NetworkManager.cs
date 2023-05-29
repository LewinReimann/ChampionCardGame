using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.MultiplayerModels;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager Instance { get; private set; }
    public string entityId;

    public Animator transition;


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
    }

    public void StoreEntityId(string id)
    {
        entityId = id;
    }

    public void StartMatchmaking()
    {
        var request = new PlayFab.MultiplayerModels.CreateMatchmakingTicketRequest()
        {
            Creator = new PlayFab.MultiplayerModels.MatchmakingPlayer()
            {
                Entity = new PlayFab.MultiplayerModels.EntityKey()
                {
                    Id = entityId,
                    Type = "title_player_account",
                    
                }
            },
            GiveUpAfterSeconds = 30,
            QueueName = "FindAnEnemy"
        };

        PlayFabMultiplayerAPI.CreateMatchmakingTicket(request, OnMatchmakingTicketCreated, OnMatchmakingFailed);
    }

    private IEnumerator PollMatchmakingTicketStatus(string ticketId)
    {
        while (true)
        {
            yield return new WaitForSeconds(5);

            var request = new PlayFab.MultiplayerModels.GetMatchmakingTicketRequest()
            {
                QueueName = "FindAnEnemy",
                TicketId = ticketId
            };

            PlayFabMultiplayerAPI.GetMatchmakingTicket(request, OnGetMatchmakingTicketSuccess, OnGetMatchmakingTicketFailure);
        }
    }

    private void OnMatchmakingTicketCreated(PlayFab.MultiplayerModels.CreateMatchmakingTicketResult result)
    {
        Debug.Log("Matchmaking ticket created: " + result.TicketId);
        // Save the ticket ID somewhere, you'll need it to check the status of the matchmaking request
        StartCoroutine(PollMatchmakingTicketStatus(result.TicketId));
    }

    private void OnMatchmakingFailed(PlayFabError error)
    {
        Debug.LogError("Failed to create matchmaking ticket: " + error.GenerateErrorReport());
    }


    public void CheckmatchmakingStatus(string ticketId)
    {
        var request = new PlayFab.MultiplayerModels.GetMatchmakingTicketRequest()
        {
            QueueName = "FindAnEnemy",
            TicketId = ticketId
        };

        PlayFabMultiplayerAPI.GetMatchmakingTicket(request, OnGetMatchmakingTicketSuccess, OnGetMatchmakingTicketFailure);
    }

    private void OnGetMatchmakingTicketSuccess(PlayFab.MultiplayerModels.GetMatchmakingTicketResult result)
    {
        if (result.Status == "Matched")
        {
            Debug.Log("Match found");
            // Get the server details from result.MatchId and start the game
            LoadScene("GameScene");
        }
        else
        {
            Debug.Log("Match not yet found, status: " + result.Status);
            // You might want to call CheckMatchmakingStatus again after a few seconds
        }
    }

    private void OnGetMatchmakingTicketFailure(PlayFabError error)
    {
        Debug.LogError("Failed to get matchmaking ticket: " + error.GenerateErrorReport());
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
