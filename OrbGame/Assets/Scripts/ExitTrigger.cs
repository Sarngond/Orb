using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTrigger : MonoBehaviour
{
    public string sceneName;
    public Vector3 playerStartPos;
    public GameObject[] guards;
    public GameObject[] buttons;
    public GameObject[] openButtons;
    private GameObject keyCard;
    public GameObject generator;


    private LevelManager levelmanager;
    private GameObject player;

    // Start is called before the first frame update
    void Start() {
        levelmanager = GameObject.FindObjectOfType<LevelManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        keyCard = GameObject.FindGameObjectWithTag("KeyCard");
        buttons = GameObject.FindGameObjectsWithTag("Button");
        guards = GameObject.FindGameObjectsWithTag("Guard");
    }

    void OnTriggerEnter(Collider collider) {
        if (collider = player.GetComponent<Collider>()) {
            levelmanager.LoadLevel(sceneName);

            player.transform.position = playerStartPos;
            generator.GetComponent<PowerGenerator>().health = 10f;
            keyCard.SetActive(true);
            keyCard.GetComponent<KeyCard>().cardObtained = false;
            foreach (GameObject button in buttons) {
                button.GetComponent<DoorButton>().isPushed = false;
            }
            foreach (GameObject button in openButtons) {
                button.GetComponent<DoorButton>().isPushed = true;
            }
            foreach (GameObject guard in guards) {
                guard.SetActive(true);
            }

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
