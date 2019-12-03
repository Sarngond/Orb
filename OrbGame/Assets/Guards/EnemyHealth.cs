using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour {

    public float health = 100f;
    public Vector3 offset = new Vector3(0, 1, 0);
    public bool isDead = false;
    private Animator anim;

    public Collider capCollider;
    public Collider knockoutCollider;
    public Light spotLight;

    public string deadString = "false";

    void Start() {
        anim = GetComponentInChildren<Animator>();
    }

    void Update() {
        if(deadString == "true") {
            isDead = true;
            KillGuard();
        }

        if (deadString == "false") {
            isDead = false;
        }
    }

    public void TakeDamage(float amount) {
        health -= amount;
        if (health <= 0) {
            Die();
        }
    }

    void Die() {
        deadString = "true";
        RemoveGuard();
        GetComponent<Guard>().enabled = false;
        GetComponent<GuardAttack>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        spotLight.enabled = false;
        anim.SetTrigger("Dead");
        capCollider.enabled = false;
        knockoutCollider.enabled = false;
        Invoke("KillGuard", 3f);
    }

    public bool Dead() {
        if (isDead) {
            return true;
        }
        return false;
    }

    public void RemoveGuard() {
        GuardManager manager = FindObjectOfType<GuardManager>();
        manager.RemoveGuardFromList(gameObject);
    }

    public void KillGuard() {
        RemoveGuard();
        GetComponent<Guard>().enabled = false;
        GetComponent<GuardAttack>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        spotLight.enabled = false;
        anim.SetTrigger("Dead");
        capCollider.enabled = false;
        knockoutCollider.enabled = false;
        gameObject.SetActive(false);
    }
}
