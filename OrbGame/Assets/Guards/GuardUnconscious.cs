using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardUnconscious : MonoBehaviour
{
    public bool unconscious = false;
    public AnimationClip unconsciousAnim;

    private Animator anim;
    private Guard guardScript;
    private NavMeshAgent agent;

    void Start()
    {
        anim = GetComponent<Animator>();
        guardScript = GetComponent<Guard>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (unconscious) {
            KnockOut();
        }
    }

    IEnumerator DelayGetUp() {
        yield return new WaitForSeconds(unconsciousAnim.length);
        WakeUp();
    }

    void KnockOut() {
        guardScript.gameObject.GetComponentInChildren<Light>().enabled = false;
        anim.SetBool("Unconscious", true);
        guardScript.enabled = false;
        agent.enabled = false;
        unconscious = false;
        StartCoroutine(DelayGetUp());
    }

    void WakeUp() {
        guardScript.onPatrol = true;
        guardScript.gameObject.GetComponentInChildren<Light>().enabled = true;
        guardScript.gameObject.GetComponentInChildren<Light>().color = Color.yellow;
        anim.SetBool("Unconscious", false);
        guardScript.enabled = true;
        agent.enabled = true;
    }
}
