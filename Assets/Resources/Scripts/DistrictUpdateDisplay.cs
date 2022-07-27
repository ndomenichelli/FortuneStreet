using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistrictUpdateDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // move into frame
        this.transform.position += Vector3.down * 50f;

        districtUpdated = this.transform.GetChild(0).GetComponent<Text>();
        beforePrice = this.transform.GetChild(1).GetComponent<Text>();
        afterPrice = this.transform.GetChild(2).GetComponent<Text>();

        this.gameObject.SetActive(false);
    }

    public GameObject districtDiplay;

    public Text districtUpdated;
    public Text beforePrice;
    public Text afterPrice;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void callUpdate(District district, int before, int after)
    {
        StartCoroutine( updateText(district, before, after) );
    }

    IEnumerator updateText(District district, int before, int after)
    {
        //districtDiplay.SetActive(true);
        //Debug.Log(districtUpdated.text);
        
        beforePrice.text = before.ToString();
        afterPrice.text = after.ToString();

        if(before < after)
        {
            districtUpdated.text = district.name.ToString() + " stocks have increased!";
            beforePrice.color = Color.green;
            afterPrice.color = Color.green;
        }
        else
        {
            districtUpdated.text = district.name.ToString() + " stocks have decreased!";
            beforePrice.color = Color.red;
            afterPrice.color = Color.red;
        }

        yield return new WaitForSeconds(2f);

        districtDiplay.SetActive(false);
    }
}
