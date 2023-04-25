using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string name;
    public int health;

    public Player(string name)
    {
        this.name = name;
        this.health = 3;
    }
}
