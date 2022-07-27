using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suit : Space
{
    // Start is called before the first frame update
    void Start()
    {
        // initalize color
        stateManager = GameObject.FindObjectOfType<StateManager>();

        Suit[] suitSpaces = GameObject.FindObjectsOfType<Suit>();

        foreach (var suitSpace in suitSpaces)
        {
            Material material = Resources.Load("Materials/" + suitSpace.suit, typeof(Material)) as Material;

            // set all sides to district color getchild(1 outside, 0-4 each side)
            for (int i = 0; i < suitSpace.transform.childCount; i++)
            {
                MeshRenderer outerRenderer = suitSpace.transform.GetChild(1).GetChild(0).GetComponent<MeshRenderer>();
                MeshRenderer outerRenderer1 = suitSpace.transform.GetChild(1).GetChild(1).GetComponent<MeshRenderer>();
                MeshRenderer outerRenderer2 = suitSpace.transform.GetChild(1).GetChild(2).GetComponent<MeshRenderer>();
                MeshRenderer outerRenderer3 = suitSpace.transform.GetChild(1).GetChild(3).GetComponent<MeshRenderer>();

                outerRenderer.material = material;
                outerRenderer1.material = material;
                outerRenderer2.material = material;
                outerRenderer3.material = material;
            }
        }

    }

    StateManager stateManager;

    public enum suitType 
    { 
        Spade = 0,
        Heart = 1,
        Clubs = 2,
        Diamond = 3
    };

    public suitType suit;

    // Update is called once per frame
    void Update()
    {
        
    }
}
