using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class District : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // initalize color
        GameObject[] districts = GameObject.FindGameObjectsWithTag("District");

        foreach (var district in districts)
        {
            Material material = Resources.Load("Materials/" + district.name, typeof(Material)) as Material;

            // set all sides to district color getchild(1 outside, 0-4 each side)
            for (int i = 0; i < district.transform.childCount; i++)
            {
                MeshRenderer outerRenderer = district.transform.GetChild(i).GetChild(1).GetChild(0).GetComponent<MeshRenderer>();
                MeshRenderer outerRenderer1 = district.transform.GetChild(i).GetChild(1).GetChild(1).GetComponent<MeshRenderer>();
                MeshRenderer outerRenderer2 = district.transform.GetChild(i).GetChild(1).GetChild(2).GetComponent<MeshRenderer>();
                MeshRenderer outerRenderer3 = district.transform.GetChild(i).GetChild(1).GetChild(3).GetComponent<MeshRenderer>();

                outerRenderer.material = material;
                outerRenderer1.material = material;
                outerRenderer2.material = material;
                outerRenderer3.material = material;
            }
        }
    }

    public int stockPrice;


    // Update is called once per frame
    void Update()
    {
        
    }

    public int getShopCount()
    {
        return this.transform.childCount;
    }
    public Shop[] getShopsOwned()
    {
        Shop[] shops = new Shop[this.getShopCount()];
        int shopCount = 0;
        foreach (var shop in this.GetComponents<Shop>())
        {
            shops[shopCount] = shop;
            shopCount++;
        }
        return shops;
    }
    public int getShopOwnedCount()
    {
        int shopCount = 0;
        foreach (var shop in getShopsOwned())
        {
            if(shop.ownedBy != null)
            {
                shopCount++;
            }
        }
        return shopCount;
    }
}

