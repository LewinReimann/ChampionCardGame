using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;

public class PlayFabLogin : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;

    public void Login()
    {
        var request = new LoginWithPlayFabRequest()
        {
            Username = usernameInput.text,
            Password = passwordInput.text
        };

        PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess, OnLoginFailure);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Logged in successfully!");
    }

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogError($"Error logging in: {error.ErrorMessage}");
    }

    public void Register()
    {
        var request = new RegisterPlayFabUserRequest()
        {
            Username = usernameInput.text,
            Password = passwordInput.text,
            RequireBothUsernameAndEmail = false
        };

        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnRegisterFailure);
    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("Successfully registered");
    }

    private void OnRegisterFailure(PlayFabError error)
    {
        Debug.Log("Failed to register");
    }

}
