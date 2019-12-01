using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    public bool hasKeyCard = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StoreItem(GameObject item) {
        if (item.GetComponent<KeyCard>()) {
            Debug.Log("got the card");
            hasKeyCard = true;
        }
    }
}
