using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {
    public float health = 100f;
    public float maxHealth = 110f;
    public Slider healthSlider;

    private GameObject[] guards;

    // Use this for initialization
    void Start() {
        healthSlider.value = health / 100;
        guards = GameObject.FindGameObjectsWithTag("Guard");

        GetComponent<PlayerMovement>().LoadPlayer();
        foreach (GameObject guard in guards) {
            guard.GetComponent<Guard>().LoadGuard();
        }
    }

    // Update is called once per frame
    void Update() {
        healthSlider.value = health / 100f;
    }

    public void TakeDamage(float damageAmount) {
        health -= damageAmount;

        if (health <= 0) {
            health = 0;
            Die();
        }
    }


    public void Die() {
        //SceneManager.LoadScene(8);
        Debug.Log("you're dead");
        SceneManager.LoadScene(4);
        //gameObject.SetActive(false);

    }

    public bool PlayerDead() {
        if(health <= 0) {
            return true;
        }
        return false;
    }
}
