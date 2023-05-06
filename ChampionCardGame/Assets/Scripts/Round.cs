using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Round : MonoBehaviour
{
    
    private bool roundActive = false;
    public int currentPhaseIndex = 0;
    public int RoundCounter = 0;
    public GameManager gameManager;
    public DeckManager deckManager;

    public bool drawPhaseActive = false;
    public bool championPhaseActive = false;
    public bool secondaryPhaseActive = false;
    public bool battlePhaseActive = false;
    public bool endPhaseActive = false;

    public bool championSlotsChecked = false;

    public GameObject SCSlotPlayer1;

    private CardManager cardManager;
    private CardDisplay cardDisplay;

    public TextMeshProUGUI player1DiceRollText;
    public TextMeshProUGUI player2DiceRollText;

    public GameObject Player1DiceRollText;
    public GameObject Player2DiceRollText;

    private void Start()
    {
        cardManager = GetComponent<CardManager>();
        SCSlotPlayer1 = GameObject.Find("SCSlotPlayer1"); // Define the variable by finding the GameObject in the scene by name
    }

    public void StartRound()
    {
        Debug.Log("Is this called?");
        gameManager = FindObjectOfType<GameManager>(); // Pass reference to GameManager to Round script
        roundActive = true;
        SwitchTurn();
    }

    public void SwitchTurn()
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

    public bool CanPlayCards()
    {
        // Return false if the current phase is DrawingPhase, BattlePhase or EndPhase,
        if (currentPhaseIndex == 1 || currentPhaseIndex == 4 || currentPhaseIndex == 5)
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

        Debug.Log("Draw Phase has begun");
        drawPhaseActive = true;
        deckManager.DrawCard();
        currentPhaseIndex++;

        Invoke("SwitchTurn", 1f); // This will call the SwitcHTurn() method after X Seconds.
    }

    private void ChampionPhase()
{

        Debug.Log("Champion Phase has begun");
        drawPhaseActive = false;
        championPhaseActive = true;
        championSlotsChecked = false; // Reset the boolean
        currentPhaseIndex++;


        // TODO Implement logic for the champion phase
    }

    public void EndChampionPhase()
    {

           
            championPhaseActive = false;
            SwitchTurn();
            
            
        
    }

private void SecondaryPhase()   
{
       
        // Set the ChampionHealth inside the GameManager
        GameManager.instance.SetChampionHealth();

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
        Debug.Log("Secondary Phase has begun");
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
        Debug.Log("Battlephase has begun");
        battlePhaseActive = true;
        StartCoroutine(ExecuteBattlePhase()); //Co-Routine is for something to Cooperate in a "Routine" of course like two small codes run at the same time and not one after another

        currentPhaseIndex++;
    }

    public void EndBattlePhase() // Call this function to end the BattleRound
    {
        battlePhaseActive = false;
        Player1DiceRollText.SetActive(false);
        Player2DiceRollText.SetActive(false);

        Slot[] slots = FindObjectsOfType<Slot>();
        foreach (Slot slot in slots)
        {
            slot.DeactivateHighlight();

        }
    }

    private IEnumerator ExecuteBattlePhase() //IEnumerator are  something that will call actions in a sequence like go again and again like here: Do Attack then check if someones Health is below 0 and then wait one second and execute again from top)
    {
        while (battlePhaseActive)
        {
            (int player1Roll, int player2Roll) = gameManager.Attack();

            // Update the UI Text objects with the dice rolls
            player1DiceRollText.text = player1Roll.ToString();
            player2DiceRollText.text = player2Roll.ToString();

            if (gameManager.player1ChampionHealth <= 0)
            {
                gameManager.player1.health--;
                EndBattlePhase();
                SwitchTurn();
            }
            else if (gameManager.player2ChampionHealth <= 0)
            {
                gameManager.player2.health--;
                EndBattlePhase();
                SwitchTurn();
            }

            yield return new WaitForSeconds(1f); // Wait for 1 second before executing the next attack
        }
    }

    private void EndPhase()
{
        Debug.Log("End Phase has begun");
        currentPhaseIndex++;
        // TODO implement logic for the EndPhase
        cardManager.ClearBoard();
      
       

        cardDisplay = FindObjectOfType<CardDisplay>();
        cardDisplay.DeactivateGameManagerHealth();
        Invoke("SwitchTurn", 2f); 
    }

}
