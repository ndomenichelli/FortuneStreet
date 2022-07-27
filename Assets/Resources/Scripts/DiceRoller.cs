using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceRoller : MonoBehaviour
{
    public Sprite[] DiceImages;
    StateManager stateManager;

    // Start is called before the first frame update
    void Start()
    {
        //amount of dice
        //DiceValues = new int[1];
        stateManager = GameObject.FindObjectOfType<StateManager>();
        spacesDisplay = GameObject.FindObjectOfType<SpaceToMoveDisplay>();
    }

    SpaceToMoveDisplay spacesDisplay;

    // Update is called once per frame
    void Update()
    {
        
    }
    //numbers on dice
    //public int[] DiceValues;

    //number on total number of dice


    public void NewTurn()
    {
        //this is the start of a players new turn
        stateManager.isDoneRolling = false;
    }
    public void RollTheDice()
    {
        if (stateManager.isDoneRolling)
        {
            //already rolled
            return;
        }
        this.gameObject.SetActive(true);

        //roll 1-7
        stateManager.DiceTotal = Random.Range(1, 8);
        spacesDisplay.setDisplay(stateManager.DiceTotal);

        this.transform.GetChild(0).GetComponent<Image>().sprite = DiceImages[stateManager.DiceTotal - 1];

        //multiple dice code

        //DiceTotal = 0;
        //for (int i = 0; i < DiceValues.Length; i++)
        //{
        //    DiceValues[i] = Random.Range(1, 8);
        //    DiceTotal += DiceValues[i];

        //    // TODO:
        //    // update visual for dice rolling

        //    //this.transform.GetChild(i).GetComponent<Text>().text = "" + DiceTotal;

        //    this.transform.GetChild(i).GetComponent<Image>().sprite = DiceImages[i];

        //    //etc
        //    Debug.Log("Dice number: " + i);
        //}

        //Debug.Log("Rolled: " + DiceTotal);

        //hard code roll
        // stateManager.DiceTotal = 7;

        stateManager.isDoneRolling = true;
        stateManager.currentPhase = StateManager.TurnPhase.MOVEMENT;
        stateManager.CheckLegalMoves();

        //Debug.Log("done rolling");
    }
}
