using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour {

    public float health = 100f;
    public Vector3 offset = new Vector3(0, 1, 0);
    private bool isDead = false;
    private Animator anim;

    public Collider capCollider;
    public Collider knockoutCollider;
    public Light spotLight;

    void Start() {
        anim = GetComponentInChildren<Animator>();
    }

    void Update() {
        
    }

    public void TakeDamage(float amount) {
        health -= amount;
        if (health <= 0) {
            Die();
        }
    }

    void Die() {
        isDead = true;
        RemoveGuard();
        GetComponent<Guard>().enabled = false;
        GetComponent<GuardAttack>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        spotLight.enabled = false;
        anim.SetTrigger("Dead");
        capCollider.enabled = false;
        knockoutCollider.enabled = false;
        Destroy(gameObject, 3f);
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
}
