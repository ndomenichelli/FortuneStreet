using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopHereDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        stateManager = GameObject.FindObjectOfType<StateManager>();
        diceDisplay = GameObject.FindObjectOfType<DiceRoller>();
        //playerTokens = GameObject.FindObjectsOfType<PlayerToken>();
    }

    StateManager stateManager;
    DiceRoller diceDisplay;

    public PlayerToken[] playerTokens;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StopYes()
    {
        //stateManager.NewTurn();
        stateManager.currentPhase = StateManager.TurnPhase.SPACE_EVENTS;
        stateManager.stopHere.SetActive(false);

        stateManager.isDoneRolling = false;
        stateManager.isDoneClicking = false;
        stateManager.isDoneAnimating = false;
    }

    public void StopNo()
    {
        stateManager.stopHere.SetActive(false);

        // TODO: move player back one space, continue current turn
        PlayerToken currentToken = playerTokens[stateManager.CurrentPlayerID];
        currentToken.moveBack();

        diceDisplay.gameObject.SetActive(true);


    }
}
