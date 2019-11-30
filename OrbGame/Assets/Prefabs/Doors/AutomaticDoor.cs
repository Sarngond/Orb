using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticDoor : MonoBehaviour
{
    private Animator anim;
    public bool canOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collider) {
        if (collider.GetComponent<PlayerMovement>() && canOpen) {
            anim.SetBool("isOpen", true);
        }
    }

    private void OnTriggerExit(Collider collider) {
        if (collider.GetComponent<PlayerMovement>()) {
            anim.SetBool("isOpen", false);
        }
    }
}
