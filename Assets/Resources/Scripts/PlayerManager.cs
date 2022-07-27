using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        scoreDisplay = GameObject.FindObjectOfType<ScoreDisplay>();
    }

    ScoreDisplay scoreDisplay;

    public PlayerToken playerToken;

    public int cash;
    public int netWorth;

    public int[] stocks;

    Shop[] shopsOwned;

    public bool[] suitsOwned;

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setCash(int newCash)
    {
        this.cash = newCash;
        scoreDisplay.setCashTextDisplay(playerToken);
    } 
    public void setNetWorth(int newNetWorth)
    {
        Debug.Log("set net worth");
        this.netWorth = newNetWorth;
        scoreDisplay.setNetWorthDisplay(playerToken);
    }

    public void payPlayer(PlayerToken player, int amount)
    {
        // give cash 
        this.cash -= amount;
        player.playerManager.cash += amount;

        this.setCash(this.cash);
        player.playerManager.setCash(player.playerManager.cash);

        // add networth
        this.netWorth -= amount;
        player.playerManager.netWorth += amount;

        this.setNetWorth(this.netWorth);
        player.playerManager.setNetWorth(player.playerManager.netWorth);

    }
}
