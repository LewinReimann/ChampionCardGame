using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhaseButton : MonoBehaviour
{
    public Round round;

    public void NextPhase()
    {
        round.SwitchTurn();
    }
  
}
