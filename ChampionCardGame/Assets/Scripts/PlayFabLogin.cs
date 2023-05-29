using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.AuthenticationModels;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayFabLogin : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;

    public NetworkManager networkManager;

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

        // Get the entity token to access entity id and type
        PlayFabAuthenticationAPI.GetEntityToken(new GetEntityTokenRequest(),
            resultToken => {
            // Store the entity id and type
            networkManager.StoreEntityId(resultToken.Entity.Id);
            },
            error => Debug.LogError("Failed to get entity token: " + error.GenerateErrorReport())
        );

        networkManager.LoadScene("MainMenu"); // Load the mainmenu
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
            DisplayName = usernameInput.text,
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
