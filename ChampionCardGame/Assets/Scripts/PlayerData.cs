using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public string playerName;
    public int Health { get; set; }

    public PlayerData(int health)
    {
        Health = health;
    }
}