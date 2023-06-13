using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using System;

public class GameManager : MonoBehaviourPunCallbacks
{
    public TMP_Text playerRollText;
    public TMP_Text opponentRollText;
    public TMP_Text playersLifeText;
    public TMP_Text opponentLifeText;

    public int playerRoll = 0;
    public int opponentRoll = 0;

    public int playersLife = 5;
    public int opponentLife = 5;

    public int playerChampionHealth;
    public int opponentChampionHealth;

    public bool isPlayerTurn = false;
    public bool isBattleActive;

    public RoundManager roundManager;

    new private PhotonView photonView;

    public CardManager cardManager;

    public GameEvents gameEvents;

    public static Action<Card> OnCardPlayed;

    // Dice Stuff

    public GameObject dicePrefabPlayer;
    public GameObject dicePrefabOpponent;
    public Transform spawnPointPlayer;
    public Transform spawnPointOpponent;

    public int rollCount = 0;
    public int player1Roll;
    public int player2Roll;

    public void RollDice()
    {
        SpawnAndRollDice(dicePrefabPlayer, spawnPointPlayer);
        SpawnAndRollDice(dicePrefabOpponent, spawnPointOpponent);
    }

    void Start()
    {
        photonView = GetComponent<PhotonView>();

        // Determine which player goes first
        if (PhotonNetwork.IsMasterClient)
        {
            isPlayerTurn = UnityEngine.Random.Range(0, 2) == 0;
        }

        playersLifeText.text = playersLife.ToString();

        cardManager.InitializeDeck();
    }

    public IEnumerator ExecuteBattlePhase()
    {
        isBattleActive = true;

        while (isBattleActive)
        {
            // Spawn the dice
            Dice playerDice = SpawnAndRollDice(dicePrefabPlayer, spawnPointPlayer).GetComponent<Dice>();
            Dice opponentDice = SpawnAndRollDice(dicePrefabOpponent, spawnPointOpponent).GetComponent<Dice>();

            int playerDiceRoll = 0;
            int opponentDiceRoll = 0;

            // Subscribe to the OnRollCompleted event
            playerDice.OnRollCompleted += (roll) => { playerDiceRoll = roll; };
            opponentDice.OnRollCompleted += (roll) => { opponentDiceRoll = roll; };

            // Wait until both dices have finished rolling
            yield return new WaitUntil(() => playerDiceRoll > 0 && opponentDiceRoll > 0);

            // Compare the dice rolls
            if (playerDiceRoll < opponentDiceRoll)
            {
                opponentChampionHealth--;
            }
            else if (playerDiceRoll > opponentDiceRoll)
            {
                playerChampionHealth--;
            }

            // Check if the battle should end
            if (playerChampionHealth <= 0 || opponentChampionHealth <= 0)
            {
                isBattleActive = false;
            }

            // Delay before next roll
            yield return new WaitForSeconds(1f);
        }

        // Proceed to the next phase
        roundManager.SwitchPhase();
        
    }

    public GameObject SpawnAndRollDice(GameObject dicePrefab, Transform spawnPoint)
    {
        // Instantiate the dice at the specifific spawn point
        GameObject diceInstance = Instantiate(dicePrefab, spawnPoint.position, Quaternion.identity);

        // Set the dice as a child of the spawn point
        diceInstance.transform.SetParent(spawnPoint);

        // Get the Dice component and call the Roll method
        Dice dice = diceInstance.GetComponent<Dice>();
        dice.Roll();

        return diceInstance;
    }

    private void Update()
    {
        
        if (playersLife <= 0)
        {
            Debug.Log("Player 2 wins the game!");
        }
        else if (opponentLife <= 0)
        {
            Debug.Log("Player 1 wins the game!");
        }
    }

    // Event stuff from here on below

    public void OnEnable()
    {
        GameEvents.OnDiceRolled += HandleDiceRolled;
        GameEvents.OnCardPlayed += HandleCardPlayed;
        GameEvents.OnCardEffectOnDiceRoll += HandleCardEffectOnDiceRoll;
        // More event subscriptions
    }

    private void OnDisable()
    {
        GameEvents.OnDiceRolled -= HandleDiceRolled;
        GameEvents.OnCardPlayed -= HandleCardPlayed;
        GameEvents.OnCardEffectOnDiceRoll -= HandleCardEffectOnDiceRoll;
        // More event unsubsciptions
    }

    private void HandleDiceRolled(int value)
    {
        // Handle dice roleld event.
    }

    private void HandleCardPlayed(Card card)
    {
        // Check if the cards trigger condition is met
        if (card.trigger.IsTriggered(this))
        {
            // if yes apply the cards effect
            card.effect.ApplyEffect(this);
        }
    }

    private void HandleCardEffectOnDiceRoll(ref int result)
    {
        // Retrieve active card effects
        List<GameEvents.CardEffect> activeCardEffects = GetActiveCardEffects();

        // Sort or queue them based on when they are played or some other stuff

        // Apply each card effect to the result
        foreach (var cardEffect in activeCardEffects)
        {
            // Apply effect (e.g. result += cardEffect.Modifier)
            // Optionally play animations for card effect
        }
        // now "resullt" holds the final result after applying c ard effects
    }

    private List<GameEvents.CardEffect> GetActiveCardEffects()
    {
        // Retrieve the list of active card effects
        // This could be a list mainted in the GameManager
        // thats gets updated as cards are played
        return new List<GameEvents.CardEffect>();
    }

    // More handles
}
