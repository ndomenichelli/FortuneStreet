using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceToMoveDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        stateManager = GameObject.FindObjectOfType<StateManager>();
        spaceDisplay = GameObject.FindObjectOfType<SpaceToMoveDisplay>();
        diceDisplay = GameObject.FindObjectOfType<DiceRoller>();
    }

    StateManager stateManager;
    SpaceToMoveDisplay spaceDisplay;
    DiceRoller diceDisplay;

    // Update is called once per frame
    void Update()
    {
        if (stateManager.isDoneRolling)
        {
            //GetComponent<Text>().text = "Spaces to Move: " + stateManager.DiceTotal;
        }
        if(!stateManager.isDoneAnimating)
        {
            //GetComponent<Text>().text = "Spaces to Move: " + stateManager.DiceTotal;
        }
        else
        {

        }
    }

    public void setDisplay(int spaces)
    {
        GetComponent<Text>().text = "Spaces to Move: " + spaces;
        if(spaces > 0)
        {
            diceDisplay.gameObject.SetActive(true);
            diceDisplay.transform.GetChild(0).GetComponent<Image>().sprite = diceDisplay.DiceImages[spaces - 1];
        }
        else if (spaces == 0)
        {
            diceDisplay.gameObject.SetActive(false);
        }
    }
}
