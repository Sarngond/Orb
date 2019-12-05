using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    public bool hasKeyCard = false;
    public string hasCardString;

    // Start is called before the first frame update
    void Start()
    {
        if(hasCardString == "true") {
            hasKeyCard = true;
        }
        if (hasCardString == "false") {
            hasKeyCard = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (hasKeyCard) {
        //    hasCardString = "true";
        //}
        //if (!hasKeyCard) {
        //    hasCardString = "false";
        //}
        if (hasCardString == "true") {
            hasKeyCard = true;
        }
        if (hasCardString == "false") {
            hasKeyCard = false;
        }
    }

    public void StoreItem(GameObject item) {
        if (item.GetComponent<KeyCard>()) {
            Debug.Log("got the card");
            //hasKeyCard = true;
            hasCardString = "true";
        }
    }
}
