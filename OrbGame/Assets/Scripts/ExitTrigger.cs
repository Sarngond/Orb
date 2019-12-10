using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ExitTrigger : MonoBehaviour
{
    public string sceneName;
    public Vector3 playerStartPos;
    public GameObject[] guards;
    public GameObject[] buttons;
    public GameObject[] openButtons;
    private GameObject keyCard;
    public GameObject generator;
    private GameObject sheildFeild;


    private LevelManager levelmanager;
    private GameObject player;

    // Start is called before the first frame update
    void Start() {
        levelmanager = GameObject.FindObjectOfType<LevelManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        keyCard = GameObject.FindGameObjectWithTag("KeyCard");
        buttons = GameObject.FindGameObjectsWithTag("Button");
        guards = GameObject.FindGameObjectsWithTag("Guard");
        sheildFeild = FindObjectOfType<SheildFeild>().gameObject;
    }

    void OnTriggerEnter(Collider collider) {
        if (collider = player.GetComponent<Collider>()) {

            player.transform.position = playerStartPos;
            player.GetComponent<PlayerItems>().hasCardString = "false";
            generator.GetComponent<PowerGenerator>().health = 10f;
            keyCard.SetActive(true);
            keyCard.GetComponent<KeyCard>().obtainedString = "false";
            sheildFeild.GetComponent<SheildFeild>().PowerUp();
            foreach (GameObject button in buttons) {
                button.GetComponent<DoorButton>().isPushedString = "false";
                button.GetComponent<Animator>().SetBool("isPushed", false);
            }
            foreach (GameObject button in openButtons) {
                button.GetComponent<DoorButton>().isPushedString = "true";
                button.GetComponent<Animator>().SetBool("isPushed", true);
            }
            foreach (GameObject guard in guards) {
                guard.GetComponent<EnemyHealth>().health = 100f;
                guard.GetComponent<EnemyHealth>().deadString = "false";
                guard.GetComponent<Guard>().enabled = true;
                guard.GetComponent<GuardAttack>().enabled = true;
                guard.GetComponent<NavMeshAgent>().enabled = true;
                guard.GetComponent<CapsuleCollider>().enabled = true;
                guard.GetComponent<BoxCollider>().enabled = true;
                guard.GetComponent<Guard>().backToPoint = true;
                Light spotLight = guard.GetComponentInChildren<Light>();
                if (spotLight.type == LightType.Spot) {
                    spotLight.enabled = true;
                }
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
            foreach (GameObject button in openButtons) {
                button.GetComponent<DoorButton>().SaveButton();
            }


            levelmanager.LoadLevel(sceneName);
        }
    }
}
