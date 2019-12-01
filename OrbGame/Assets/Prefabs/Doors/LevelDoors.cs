using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDoors : MonoBehaviour
{
    private Animator anim;
    //public bool canOpen = false;
    public bool hasCard = false;
    private GameObject player;

    // Start is called before the first frame update
    void Start() {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update() {

        if (!player.GetComponent<PlayerItems>().hasKeyCard) {
            hasCard = false;
        } else if (player.GetComponent<PlayerItems>().hasKeyCard) {
            hasCard = true;
        }

    }

    private void OnTriggerEnter(Collider collider) {
        if (collider.GetComponent<PlayerMovement>() && hasCard) {
            anim.SetBool("isOpen", true);
        }
    }

    private void OnTriggerExit(Collider collider) {
        if (collider.GetComponent<PlayerMovement>()) {
            anim.SetBool("isOpen", false);
        }
    }
}
