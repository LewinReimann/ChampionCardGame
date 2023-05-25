using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class PlayFabPlayerData : MonoBehaviour
{
    void Awake()
    {
        PlayFab.PlayFabSettings.TitleId = "47B17";
    }

    public void SavePlayerData(string key, string value)
    {
        var request = new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>()
            {
                { key, value },
            },
        };
        PlayFabClientAPI.UpdateUserData(request, OnSaveDataSuccess, OnSaveDataFailure);
    }

    public void LoadPlayerData(string key)
    {
        var request = new GetUserDataRequest()
        {
            Keys = new List<string> { key },
        };

        PlayFabClientAPI.GetUserData(request, OnLoadDataSuccess, OnLoadDataFailure);
    }

    private void OnSaveDataSuccess(UpdateUserDataResult result)
    {
        Debug.Log("Successfully saved user data");
    }

    private void OnSaveDataFailure(PlayFabError error)
    {
        Debug.LogError("Failed to save user data: " + error.GenerateErrorReport());
    }

    public void OnLoadDataSuccess(GetUserDataResult result)
    {
        foreach (var item in result.Data)
        {
            Debug.Log("Key: " + item.Key + " Value: " + item.Value.Value);
        }
    }

    private void OnLoadDataFailure(PlayFabError error)
    {
        Debug.LogError("Failed to load user data: " + error.GenerateErrorReport());
    }

}
