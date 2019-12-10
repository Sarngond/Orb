using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCard : MonoBehaviour
{
    private GameObject player;
    public bool cardObtained = false;
    public string obtainedString = "false";

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        LoadKeyCard();

        if (obtainedString == "true") {
            cardObtained = true;
        }
        if (obtainedString == "false") {
            cardObtained = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (obtainedString == "true") {
            cardObtained = true;
        }
        if (obtainedString == "false") {
            cardObtained = false;
        }


        if (cardObtained) {
            HideCard();
        }
        if (!cardObtained) {
            ShowCard();
        }
    }

    private void OnTriggerEnter(Collider collider) {
        if (collider.GetComponent<PlayerMovement>()) {
            player.GetComponent<PlayerItems>().StoreItem(gameObject);
            //Destroy(gameObject);
            obtainedString = "true";
            cardObtained = true;
        }
    }

    private void HideCard() {
        gameObject.SetActive(false);
    }
    private void ShowCard() {
        gameObject.SetActive(true);
    }

    public void SaveKeyCard() {
        SaveSystem.SaveKeyCard(gameObject);
    }

    public void LoadKeyCard() {
        KeyCardData data = SaveSystem.LoadKeyCard(gameObject);
        Debug.Log(gameObject.name + " " + data.cardObtained);
        obtainedString = data.cardObtained;
    }
}
