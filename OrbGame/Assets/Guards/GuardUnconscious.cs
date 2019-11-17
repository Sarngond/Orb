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
    private GuardAttack guardAttackScript;
    private NavMeshAgent agent;
    public Light spotlight;

    void Start()
    {
        anim = GetComponent<Animator>();
        guardScript = GetComponent<Guard>();
        guardAttackScript = GetComponent<GuardAttack>();
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
        spotlight.enabled = false;
        anim.SetBool("Unconscious", true);
        guardAttackScript.enabled = false;
        guardScript.enabled = false;
        agent.enabled = false;
        unconscious = false;
        StartCoroutine(DelayGetUp());
    }

    void WakeUp() {
        guardScript.onPatrol = true;
        spotlight.enabled = true;
        guardScript.gameObject.GetComponentInChildren<Light>().color = Color.yellow;
        anim.SetBool("Unconscious", false);
        guardAttackScript.enabled = true;
        guardScript.enabled = true;
        agent.enabled = true;
    }
}
