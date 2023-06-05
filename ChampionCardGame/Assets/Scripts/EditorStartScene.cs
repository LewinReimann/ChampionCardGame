using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public static class EditorStartScene
{
    // This is just for the editor to always go to the login screen when we hit play, so we dont need to switch scenes all the time

    static EditorStartScene()
    {
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    private static void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.EnteredPlayMode)
        {
            EditorSceneManager.LoadScene("Assets/Scenes/LoginScene.unity");
        }
    }
}
