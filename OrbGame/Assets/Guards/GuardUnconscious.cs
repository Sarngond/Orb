using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardUnconscious : MonoBehaviour
{
    public bool unconscious = false;
    private bool animPlayed = false;
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
        isUnconscious();
        if (unconscious) {
            KnockOut();
        }
    }

    public bool isUnconscious() {
        if (unconscious) {
            return true;
        }
        return false;
    }

    IEnumerator DelayGetUp() {
        yield return new WaitForSeconds(unconsciousAnim.length);
        WakeUp();
        StopAllCoroutines();
    }

    void KnockOut() {
        tag = "Unconscious Guard";
        if (!animPlayed) {
            anim.SetTrigger("Unconscious");
        }
        animPlayed = true;
        spotlight.enabled = false;
        guardAttackScript.enabled = false;
        guardScript.enabled = false;
        agent.enabled = false;
        StartCoroutine(DelayGetUp());
    }

    void WakeUp() {
        tag = "Guard";
        animPlayed = false;
        guardScript.onPatrol = true;
        spotlight.enabled = true;
        spotlight.color = Color.yellow;
        guardScript.gameObject.GetComponentInChildren<Light>().color = Color.yellow;
        guardAttackScript.enabled = true;
        guardScript.enabled = true;
        agent.enabled = true;
        unconscious = false;
    }
}
