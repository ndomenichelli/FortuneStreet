using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BuyShopDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        stateManager = GameObject.FindObjectOfType<StateManager>();
        scoreDisplay = GameObject.FindObjectOfType<ScoreDisplay>();
    }

    StateManager stateManager;
    public PlayerToken[] playerTokens;
    ScoreDisplay scoreDisplay;
    public DistrictUpdateDisplay districtUpdateDisplay;

    Shop[] shops;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyYes()
    {
        // check if can afford, shouldnt get here if owned, set inner material to current player color

        PlayerToken playerHere = playerTokens[stateManager.CurrentPlayerID];

        Shop thisShop = playerHere.currentSpace.transform.GetComponent<Shop>();

        if (playerHere.playerManager.cash >= playerHere.currentSpace.GetComponent<Shop>().buyPrice)
        {
            // subtract buy price from cash
            playerHere.playerManager.setCash(playerHere.playerManager.cash - thisShop.buyPrice);

            // set inner material to player color
            Material material = Resources.Load("Materials/Player " + (stateManager.CurrentPlayerID + 1).ToString(), typeof(Material)) as Material;
            MeshRenderer innerRenderer = playerHere.currentSpace.transform.GetChild(0).GetComponent<MeshRenderer>();
            innerRenderer.material = material;

            // set owned by
            thisShop.SetOwner(playerHere);

            // set rent price on space
            thisShop.GetComponentInChildren<Text>().text = "" + thisShop.buyPrice / stateManager.getBuyToRent();

            // update stock prices
            districtUpdateDisplay.gameObject.SetActive(true);
            districtUpdateDisplay.callUpdate(thisShop.district, thisShop.district.stockPrice, thisShop.district.stockPrice + 1);
            thisShop.district.stockPrice += 1;
        }
        else
        {
            Debug.Log("Not enough cash");
        }

        // disable and end turn

        stateManager.buyHere.SetActive(false);
        stateManager.NewTurn();
    }
    public void BuyNo()
    {
        stateManager.NewTurn();
        stateManager.buyHere.SetActive(false);
    }
}
