using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : Space
{
    // Start is called before the first frame update
    void Start()
    {
        //initalize color
        Warp[] warpSpaces = GameObject.FindObjectsOfType<Warp>();

        foreach (var warpSpace in warpSpaces)
        {
            Material material = Resources.Load("Materials/" + "Warp", typeof(Material)) as Material;

            // set all sides to district color getchild(1 outside, 0-4 each side)
            for (int i = 0; i < warpSpace.transform.childCount; i++)
            {
                MeshRenderer outerRenderer = warpSpace.transform.GetChild(1).GetChild(0).GetComponent<MeshRenderer>();
                MeshRenderer outerRenderer1 = warpSpace.transform.GetChild(1).GetChild(1).GetComponent<MeshRenderer>();
                MeshRenderer outerRenderer2 = warpSpace.transform.GetChild(1).GetChild(2).GetComponent<MeshRenderer>();
                MeshRenderer outerRenderer3 = warpSpace.transform.GetChild(1).GetChild(3).GetComponent<MeshRenderer>();

                outerRenderer.material = material;
                outerRenderer1.material = material;
                outerRenderer2.material = material;
                outerRenderer3.material = material;
            }
        }
    }

    public int warpZone;

    // Update is called once per frame
    void Update()
    {
        
    }
}
