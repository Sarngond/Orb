using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatedDoor : MonoBehaviour
{
    private Animator anim;
    public bool canOpen = false;

    // Start is called before the first frame update
    void Start() {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        if (canOpen) {
            anim.SetBool("isOpen", true);
        } else if (!canOpen) {
            anim.SetBool("isOpen", false);
        }
    }
}
