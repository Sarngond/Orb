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

    //function name
    //foreach guard in guards[]
    //if the guard is unconscious then tell the other Guards that can see them
    //
}
