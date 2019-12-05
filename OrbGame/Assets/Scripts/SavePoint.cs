using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    private GameObject player;
    private GameObject keyCard;
    public GameObject[] guards;
    public GameObject[] buttons;
    public ParticleSystem particles;

    public bool hasSavedData = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        keyCard = GameObject.FindGameObjectWithTag("KeyCard");
        buttons = GameObject.FindGameObjectsWithTag("Button");

        hasSavedData = true;
        particles.Play();
        player.GetComponent<PlayerMovement>().SavePlayer();
        keyCard.GetComponent<KeyCard>().SaveKeyCard();
        foreach (GameObject guard in guards) {
            guard.GetComponent<Guard>().SaveGuard();
        }

        foreach (GameObject button in buttons) {
            button.GetComponent<DoorButton>().SaveButton();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collider) {
        if(collider == player.GetComponent<Collider>()) {
            hasSavedData = true;
            particles.Play();
            player.GetComponent<PlayerMovement>().SavePlayer();
            keyCard.GetComponent<KeyCard>().SaveKeyCard();
            foreach (GameObject guard in guards) {
                guard.GetComponent<Guard>().SaveGuard();
            }

            foreach (GameObject button in buttons) {
                button.GetComponent<DoorButton>().SaveButton();
            }
        }
    }
}
