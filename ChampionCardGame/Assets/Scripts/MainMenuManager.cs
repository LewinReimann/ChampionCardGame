using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public NetworkManager networkManager;

    void Start()
    {
        networkManager = NetworkManager.Instance;
    }

    public void SearchOpponent()
    {
        networkManager.SearchForOpponent();
    }

    public void TestRunStarter()
    {
        networkManager.TestRun();
    }
}
