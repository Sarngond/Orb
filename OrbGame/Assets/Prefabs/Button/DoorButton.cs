using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour
{
    public GameObject door;
    private Animator anim;
    public bool isPushed = false;
    public string isPushedString;
    private SavePoint savePoint;

    // Start is called before the first frame update
    void Start()
    {

        anim = GetComponent<Animator>();
        savePoint = FindObjectOfType<SavePoint>();
        LoadButton();

        /*if (door.GetComponent<AutomaticDoor>()) {
            if (door.GetComponent<AutomaticDoor>().canOpen) {
                anim.SetBool("isPushed", true);
                isPushedString = "true";
            }
            else if (!door.GetComponent<AutomaticDoor>().canOpen) {
                anim.SetBool("isPushed", false);
                isPushedString = "false";
            }
        }

        if (door.GetComponent<ActivatedDoor>()) {
            if (door.GetComponent<ActivatedDoor>().canOpen) {
                anim.SetBool("isPushed", true);
                isPushedString = "true";
            }
            else if (!door.GetComponent<ActivatedDoor>().canOpen) {
                anim.SetBool("isPushed", false);
                isPushedString = "false";
            }
        }*/

        if (isPushedString == "true") {
            if (door.GetComponent<AutomaticDoor>()) {
                door.GetComponent<AutomaticDoor>().canOpen = true;
                isPushed = true;
                anim.SetBool("isPushed", true);
            }
            if (door.GetComponent<ActivatedDoor>()) {
                door.GetComponent<ActivatedDoor>().canOpen = true;
                isPushed = true;
                anim.SetBool("isPushed", true);
            }
        }

        if (isPushedString == "false") {
            if (door.GetComponent<AutomaticDoor>()) {
                door.GetComponent<AutomaticDoor>().canOpen = false;
                isPushed = false;
                anim.SetBool("isPushed", false);
            }
            if (door.GetComponent<ActivatedDoor>()) {
                door.GetComponent<ActivatedDoor>().canOpen = false;
                isPushed = false;
                anim.SetBool("isPushed", false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPushedString == "true") {
            if (door.GetComponent<AutomaticDoor>()) {
                door.GetComponent<AutomaticDoor>().canOpen = true;
            }
            if (door.GetComponent<ActivatedDoor>()) {
                door.GetComponent<ActivatedDoor>().canOpen = true;
            }
        }

        if (isPushedString == "false") {
            if (door.GetComponent<AutomaticDoor>()) {
                door.GetComponent<AutomaticDoor>().canOpen = false;
            }
            if (door.GetComponent<ActivatedDoor>()) {
                door.GetComponent<ActivatedDoor>().canOpen = false;
            }
        }
    }

    private void OnTriggerStay(Collider collider) {
        if (collider.GetComponent<PlayerMovement>()) {
            if (Input.GetKeyDown(KeyCode.E)) {

                if (door.GetComponent<AutomaticDoor>()) {
                    if (!door.GetComponent<AutomaticDoor>().canOpen) {
                        //door.GetComponent<AutomaticDoor>().canOpen = true;
                        anim.SetBool("isPushed", true);
                        isPushed = true;
                    }
                    else if (door.GetComponent<AutomaticDoor>().canOpen) {
                        door.GetComponent<AutomaticDoor>().canOpen = false;
                        anim.SetBool("isPushed", false);
                        isPushed = false;
                    }
                }

                if (door.GetComponent<ActivatedDoor>()) {
                    if (!door.GetComponent<ActivatedDoor>().canOpen) {
                        //door.GetComponent<ActivatedDoor>().canOpen = true;
                        anim.SetBool("isPushed", true);
                        isPushed = true;
                    }
                    else if (door.GetComponent<ActivatedDoor>().canOpen) {
                        //door.GetComponent<ActivatedDoor>().canOpen = false;
                        anim.SetBool("isPushed", false);
                        isPushed = false;
                    }
                }
            }
        }

        if (isPushed) {
            isPushedString = "true";
        }

        if (!isPushed) {
            isPushedString = "false";
        }
    }

    public void SaveButton() {
        SaveSystem.SaveButtons(gameObject);
    }

    public void LoadButton() {
        ButtonData data = SaveSystem.LoadButtons(gameObject);
        isPushedString = data.buttonActive;
    }
}
