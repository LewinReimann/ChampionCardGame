using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Round : MonoBehaviour
{
    
    private bool roundActive = false;
    public int currentPhaseIndex = 0;
    public int RoundCounter = 0;
    public DeckManager deckManager;

    public bool drawPhaseActive = false;
    public bool championPhaseActive = false;
    public bool secondaryPhaseActive = false;
    public bool battlePhaseActive = false;
    public bool endPhaseActive = false;

    public bool championSlotsChecked = false;


    private CardManager cardManager;
    private CardDisplay cardDisplay;

    public TextMeshProUGUI player1DiceRollText;
    public TextMeshProUGUI player2DiceRollText;

    public GameObject Player1DiceRollText;
    public GameObject Player2DiceRollText;

    public int currentPlayerTurn = 1;

    private void Start()
    {
        cardManager = GetComponent<CardManager>();
        StartRound();
      
    }

    public void StartRound()
    {
        
        roundActive = true;
        SwitchPhase();
    }

    public void SwitchPhase()
    {
        
        
        if (currentPhaseIndex == 0)
        {
            DrawPhase();
        }
        else if (currentPhaseIndex == 1)
        {
            
            ChampionPhase();
        }
        else if (currentPhaseIndex == 2)
        {
            SecondaryPhase();
        }
        else if (currentPhaseIndex == 3)
        {
           
            BattlePhase();
        }
        else if (currentPhaseIndex == 4)
        {
            
            EndPhase();
            RoundCounter++;
            Debug.Log("Round has increased by" + RoundCounter);
        }

        if (currentPhaseIndex >= 5)
        {
            currentPhaseIndex = 0;
        }

    }

    public void SwitchPlayerTurn()
    {
        currentPlayerTurn = (currentPlayerTurn == 1) ? 2 : 1;
    }

    public bool CanPlayCards(int playerID)
    {
        // Return false if the current phase is DrawingPhase, BattlePhase or EndPhase,
        if (currentPhaseIndex == 1 || currentPhaseIndex == 4 || currentPhaseIndex == 5)
        {
            return false;
        }

        // Return false if its not the current players turn
        if (playerID != currentPlayerTurn)
        {
            return false;
        }

        return true;
    }
    
    public void DrawPhase()
    {
        Slot[] slots = FindObjectsOfType<Slot>();
        foreach (Slot slot in slots)
        {
            slot.SetColliderEnabled(true); // Enable the Collider
        }

        drawPhaseActive = true;
        deckManager.DrawCard();
        currentPhaseIndex++;

        Invoke("SwitchPhase", 1f); // This will call the SwitcHPhase() method after X Seconds.
    }

    private void ChampionPhase()
{
        drawPhaseActive = false;
        championPhaseActive = true;
        championSlotsChecked = false; // Reset the boolean
        currentPhaseIndex++;


        // TODO Implement logic for the champion phase
    }

    public void EndChampionPhase()
    {

           
            championPhaseActive = false;
            SwitchPhase();
            
            
        
    }

private void SecondaryPhase()   
{
       
        // Set the ChampionHealth inside the GameManager
        GameManager.Instance.SetChampionHealth();

        // Find all CardDisplay components in the scene
        CardDisplay[] cardDisplays = FindObjectsOfType<CardDisplay>();

        // Loop through all the CardDisplay components
        foreach (CardDisplay cardDisplay in cardDisplays)
        {
            // Check if the card is in the onBoard List
            if (cardManager.onBoard.Contains(cardDisplay.card))
            {
                cardDisplay.ActivateGameManagerHealth();
            }
        }


        secondaryPhaseActive = true;
        currentPhaseIndex++;
}

private void BattlePhase()
    {
        
        // Find Slot Component to Deactivate the isColliding for the Highlight
        Slot[] slots = FindObjectsOfType<Slot>();
        foreach (Slot slot in slots)
        {
            slot.SetColliderEnabled(false); // Disable the Collider
        }

        Player1DiceRollText.SetActive(true);
        Player2DiceRollText.SetActive(true);
        secondaryPhaseActive = false;
        battlePhaseActive = true;
        StartCoroutine(ExecuteBattlePhase()); //Co-Routine is for something to Cooperate in a "Routine" of course like two small codes run at the same time and not one after another

        currentPhaseIndex++;
    }

    public IEnumerator EndBattlePhase() // Call this function to end the BattleRound
    {
        battlePhaseActive = false;
        Player1DiceRollText.SetActive(false);
        Player2DiceRollText.SetActive(false);

        Slot[] slots = FindObjectsOfType<Slot>();
        foreach (Slot slot in slots)
        {
            slot.DeactivateHighlight();

        }
        yield return new WaitForSeconds(2f); // Wait for 2 seconds
        SwitchPhase();
    }

    private IEnumerator ExecuteBattlePhase()
    {
        while (battlePhaseActive)
        {

            yield return StartCoroutine(GameManager.Instance.Attack());

            // Update the UI text objects with the dice rolls
            player1DiceRollText.text = GameManager.Instance.diceManager.player1Roll.ToString();
            player2DiceRollText.text = GameManager.Instance.diceManager.player2Roll.ToString();
            

            if (GameManager.Instance.player1ChampionHealth <= 0 && GameManager.Instance.player2ChampionHealth >= 0)
            {
                GameManager.Instance.player1Data.Health--;
                StartCoroutine(EndBattlePhase());
                
            }
            else if (GameManager.Instance.player2ChampionHealth <= 0 && GameManager.Instance.player1ChampionHealth >= 0)
            {
                GameManager.Instance.player2Data.Health--;
                StartCoroutine(EndBattlePhase());

            }
            else if (GameManager.Instance.player1ChampionHealth <= 0 && GameManager.Instance.player2ChampionHealth <= 0)
            {
                StartCoroutine(EndBattlePhase());

            }

            yield return new WaitForSeconds(1f); // wait for 1 second before the next roll
        }
    }

    private void EndPhase()
{
        currentPhaseIndex++;
        // TODO implement logic for the EndPhase
        cardManager.ClearBoard();
     

        cardDisplay = FindObjectOfType<CardDisplay>();
        cardDisplay.DeactivateGameManagerHealth();
        Invoke("SwitchPhase", 2f); 
    }

}
