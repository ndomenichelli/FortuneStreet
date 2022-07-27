using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentPlayerDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        stateManager = GameObject.FindObjectOfType<StateManager>();
        myText = GetComponent<Text>();
    }

    StateManager stateManager;
    Text myText;
    // Update is called once per frame
    void Update()
    {
        // current player is 0-3
        myText.text = "Player " + (stateManager.CurrentPlayerID + 1) + "'s Turn";
        if(stateManager.CurrentPlayerID == 0)
        {
            myText.color = Color.red;
        }
        else if (stateManager.CurrentPlayerID == 1)
        {
            myText.color = Color.blue;
        }
        else if (stateManager.CurrentPlayerID == 2)
        {
            myText.color = Color.green;
        }
        else if (stateManager.CurrentPlayerID == 3)
        {
            myText.color = Color.yellow;
        }

    }
}
