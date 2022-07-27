using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : Space 
{
    // Start is called before the first frame update
    void Start()
    {
        stateManager = GameObject.FindObjectOfType<StateManager>();

        // get text on shop tile
        shopPriceText = this.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>();

        shopPriceText.text = "" + buyPrice;
    }

    public PlayerToken ownedBy;

    public int buyPrice;

    Text shopPriceText;

    public District district;
    public StateManager stateManager;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetOwner(PlayerToken newOwner)
    {
        ownedBy = newOwner;
    }
}
