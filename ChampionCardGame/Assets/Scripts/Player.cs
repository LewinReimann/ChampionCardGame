using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string playerName;
    public int health;

    void Start()
    {

    }

    public Player(string name)
    {
        this.name = name;
        this.health = 3;
    }
}
