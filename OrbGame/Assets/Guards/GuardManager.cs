using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardManager : MonoBehaviour {

    public GameObject[] guards;
    private Guard guardScript;
    public GameObject unconsciousGuard = null;

    // Start is called before the first frame update
    void Start()
    {
        guards = GameObject.FindGameObjectsWithTag("Guard");
    }

    // Update is called once per frame
    void Update()
    {
        SeePlayer();
        FindUnconsciousGuards();
    }

    void FindUnconsciousGuards() {

        foreach (GameObject guard in guards) {
            if (guard.tag == "Unconscious Guard") {
                unconsciousGuard = guard;
            }
            if (guard.tag == "Guard") {
                if (!guard.GetComponent<GuardUnconscious>().isUnconscious() && unconsciousGuard != null) {
                    if (unconsciousGuard.GetComponent<GuardUnconscious>().isUnconscious()) {
                        guard.GetComponent<Guard>().SpotUnconscious(unconsciousGuard);
                    }
                }
            }
        }
    }

    void SeePlayer() {
        foreach (GameObject guard in guards) {
            if (guard.GetComponent<Guard>().CanSeePlayer()) {
                foreach (GameObject guard2 in guards) {
                    guard2.GetComponent<Guard>().CallToAttack();
                }
            }
        }
    }
}
