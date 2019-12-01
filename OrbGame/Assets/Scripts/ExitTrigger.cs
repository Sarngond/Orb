using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTrigger : MonoBehaviour
{
    public string sceneName;

    private LevelManager levelmanager;
    private GameObject player;

    // Start is called before the first frame update
    void Start() {
        levelmanager = GameObject.FindObjectOfType<LevelManager>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter(Collider collider) {
        if (collider = player.GetComponent<Collider>()) {
            levelmanager.LoadLevel(sceneName);
        }
    }
}
