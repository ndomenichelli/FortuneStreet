using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Space : MonoBehaviour
{

    public Space[] NextSpaces;
    public PlayerToken[] playerTokensHere;

    public int getPlayerCount()
    {
        int count = 0;
        foreach (PlayerToken player in playerTokensHere)
        {
            if(player != null)
                count++;
        }
        return count;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
