using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour
{
    public GameObject door;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        if (door.GetComponent<AutomaticDoor>()) {
            if (door.GetComponent<AutomaticDoor>().canOpen) {
                anim.SetBool("isPushed", true);
            }
            else if (!door.GetComponent<AutomaticDoor>().canOpen) {
                anim.SetBool("isPushed", false);
            }
        }

        if (door.GetComponent<ActivatedDoor>()) {
            if (door.GetComponent<ActivatedDoor>().canOpen) {
                anim.SetBool("isPushed", true);
            }
            else if (!door.GetComponent<ActivatedDoor>().canOpen) {
                anim.SetBool("isPushed", false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider collider) {
        if (collider.GetComponent<PlayerMovement>()) {
            if (Input.GetKeyDown(KeyCode.E)) {

                if (door.GetComponent<AutomaticDoor>()) {
                    if (!door.GetComponent<AutomaticDoor>().canOpen) {
                        door.GetComponent<AutomaticDoor>().canOpen = true;
                        anim.SetBool("isPushed", true);
                    }
                    else if (door.GetComponent<AutomaticDoor>().canOpen) {
                        door.GetComponent<AutomaticDoor>().canOpen = false;
                        anim.SetBool("isPushed", false);
                    }
                }

                if (door.GetComponent<ActivatedDoor>()) {
                    if (!door.GetComponent<ActivatedDoor>().canOpen) {
                        door.GetComponent<ActivatedDoor>().canOpen = true;
                        anim.SetBool("isPushed", true);
                    }
                    else if (door.GetComponent<ActivatedDoor>().canOpen) {
                        door.GetComponent<ActivatedDoor>().canOpen = false;
                        anim.SetBool("isPushed", false);
                    }
                }
            }
        }
    }
}
