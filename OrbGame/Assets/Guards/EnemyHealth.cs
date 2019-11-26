using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public float health = 100f;
    //public GameObject deadReeses;
    public Vector3 offset = new Vector3(0, 1, 0);
    private bool isDead = false;

    void Start() {
        
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
        //GameObject deadZombie;
        Destroy(transform.parent.gameObject);
        //deadZombie = Instantiate(deadReeses, transform.position + offset, transform.rotation) as GameObject;
    }

    public bool Dead() {
        if (isDead) {
            return true;
        }
        return false;
    }
}
