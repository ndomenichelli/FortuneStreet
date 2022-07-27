using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        playerScoreDisplays = this.transform.GetChild(0).GetComponentsInChildren<Canvas>();

        int i = 0;
        foreach (var playerScoreDisplay in playerScoreDisplays)
        {
            //getchild 0 is player name, 1 is networth, 2 is cash
            players[i].playerManager.netWorth = int.Parse(playerScoreDisplay.transform.GetChild(0).GetChild(1).GetComponent<Text>().text);
            players[i].playerManager.cash = int.Parse(playerScoreDisplay.transform.GetChild(0).GetChild(2).GetComponent<Text>().text);

            netWorths[i] = playerScoreDisplay.transform.GetChild(0).GetChild(1).GetComponent<Text>();
            cashs[i] = playerScoreDisplay.transform.GetChild(0).GetChild(2).GetComponent<Text>();
            
            updater[i] = playerScoreDisplay.transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<Text>();
            
            //Debug.Log("Player " + i);
            i++;
        }   
    }

    Canvas[] playerScoreDisplays;

    Text[] cashs = new Text[StateManager.NumberOfPlayers];
    Text[] netWorths = new Text[StateManager.NumberOfPlayers];

    Text[] updater = new Text[4];

    public PlayerToken[] players;

    public GameObject[] updateDisplay;


    // Update is called once per frame
    void Update()
    {
        
    }

    public void setCashTextDisplay(PlayerToken player)
    {
        int difference = player.playerManager.cash - int.Parse(cashs[player.playerID].text);

        if(difference > 0)
        {
            updater[player.playerID].color = Color.green;
        }
        else
        {
            updater[player.playerID].color = Color.red;
        }

        updater[player.playerID].text = difference.ToString();

        StartCoroutine(showCashUpdate(player));

        cashs[player.playerID].text = player.playerManager.cash.ToString();

    }

    public void setNetWorthDisplay(PlayerToken player)
    {
        netWorths[player.playerID].text = player.playerManager.netWorth.ToString();
    }

    IEnumerator showCashUpdate(PlayerToken player)
    {
        //display message
        updateDisplay[player.playerID].SetActive(true);

        //wait 2 seconds
        yield return new WaitForSeconds(2f);

        updateDisplay[player.playerID].SetActive(false);
    }

    public void updateSuits(PlayerToken player, Suit.suitType suit)
    {
        string[] suitsChar = { "♤", "♡", "♧", "♢" };
        string[] suitsOwnedChar = { "♠", "♥", "♣", "♦" };

        for (int i = 0; i < StateManager.NumberOfPlayers; i++)
        {
            if (players[player.playerID].playerManager.suitsOwned[i])
            {
                this.transform.GetChild(0).GetChild(player.playerID).GetChild(0).GetChild(4).GetChild(i).GetComponent<Text>().text = suitsOwnedChar[i];
                this.transform.GetChild(0).GetChild(player.playerID).GetChild(0).GetChild(4).GetChild(i).GetComponent<Text>().fontSize = 20;

            }
            else
            {
                this.transform.GetChild(0).GetChild(player.playerID).GetChild(0).GetChild(4).GetChild(i).GetComponent<Text>().text = suitsChar[i];               
            }
            
        }
    }
}
