using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTrigger : MonoBehaviour
{
    private GameObject player;
    public GameObject generator;
    public GameObject generatorObject;
    public GameObject button;

    private GameObject keyCard;
    public GameObject keyCardObject;
    public GameObject[] guards;
    public GameObject[] buttons;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        keyCard = GameObject.FindGameObjectWithTag("KeyCard");
        buttons = GameObject.FindGameObjectsWithTag("Button");
        guards = GameObject.FindGameObjectsWithTag("Guard");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider collider) {
        if (collider.GetComponent<PlayerMovement>()) {
            if(button != null) {
                if (button.GetComponent<DoorButton>().isPushed) {
                    Debug.Log("Save");
                    player.GetComponent<PlayerMovement>().SavePlayer();
                    keyCard.GetComponent<KeyCard>().SaveKeyCard();
                    generator.GetComponent<PowerGenerator>().SaveGenerator();
                    foreach (GameObject guard in guards) {
                        guard.GetComponent<Guard>().SaveGuard();
                    }
                    foreach (GameObject button in buttons) {
                        button.GetComponent<DoorButton>().SaveButton();
                    }
                }
            }

            if (generatorObject != null) {
                if (generatorObject.GetComponent<PowerGenerator>().health <= 0) {
                    Debug.Log("Save");
                    player.GetComponent<PlayerMovement>().SavePlayer();
                    keyCard.GetComponent<KeyCard>().SaveKeyCard();
                    generator.GetComponent<PowerGenerator>().SaveGenerator();
                    foreach (GameObject guard in guards) {
                        guard.GetComponent<Guard>().SaveGuard();
                    }
                    foreach (GameObject button in buttons) {
                        button.GetComponent<DoorButton>().SaveButton();
                    }
                }
            }

            if(keyCardObject != null) {
                if (keyCardObject.GetComponent<KeyCard>().cardObtained) {
                    Debug.Log("Save");
                    player.GetComponent<PlayerMovement>().SavePlayer();
                    keyCard.GetComponent<KeyCard>().SaveKeyCard();
                    generator.GetComponent<PowerGenerator>().SaveGenerator();
                    foreach (GameObject guard in guards) {
                        guard.GetComponent<Guard>().SaveGuard();
                    }
                    foreach (GameObject button in buttons) {
                        button.GetComponent<DoorButton>().SaveButton();
                    }
                }
            }
        }
    }
}
