using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.MultiplayerModels;
using TMPro;

// Handles all stuff in our game we send to our server for handling results

public class PlayFabManager : MonoBehaviour
{

    public TMP_Text opponentRollText; // Assign in Unity Editor
    public string opponentEntityId; // assign in Unity Editor

    // Send the rolls to the server to check who has won between the two results

    public void SendRoll(int roll)
    {
        Debug.Log("Sending roll: " + roll); // Log the roll value

        var request = new PlayFab.ClientModels.ExecuteCloudScriptRequest()
        {
            FunctionName = "SubmitDiceRoll", // CloudScript function name
            FunctionParameter = new { roll = roll }, // parameters for the function
            GeneratePlayStreamEvent = true // option to log this event
        };

        PlayFabClientAPI.ExecuteCloudScript(request, OnRollSubmitted, OnRollSubmitFailed);
    }

    private void OnRollSubmitted(PlayFab.ClientModels.ExecuteCloudScriptResult result)
    {
        Debug.Log("Roll submitted successfully");
    }

    private void OnRollSubmitFailed(PlayFab.PlayFabError error)
    {
        Debug.LogError("Failed to submit roll: " + error.GenerateErrorReport());
    }

    // Get the rolls from the server to display it for both players

    public void FetchOpponentRoll()
    {
        var request = new GetUserDataRequest { PlayFabId = opponentEntityId };
        PlayFabClientAPI.GetUserData(request, OnGetObjectsSuccess, OnGetObjectsFailure);
    }

    private void OnGetObjectsSuccess(GetUserDataResult result)
    {
        if (result.Data.ContainsKey("Roll"))
        {
            var roll = result.Data["Roll"].Value;
            opponentRollText.text = "Opponent rolled: " + roll;
        }
        else
        {
            opponentRollText.text = "Opponent has not rolled yet";
        }
    }

    private void OnGetObjectsFailure(PlayFabError error)
    {
        Debug.LogError("Failed to get opponents roll: " + error.GenerateErrorReport());
    }
}
