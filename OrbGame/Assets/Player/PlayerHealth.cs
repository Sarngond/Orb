using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;
    public float maxHealth = 110f;
    public Slider healthSlider;

    // Use this for initialization
    void Start() {
        healthSlider.value = health / 100;
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
        //gameObject.SetActive(false);
        GetComponent<PlayerMovement>().LoadPlayer();
    }

    public bool PlayerDead() {
        if(health <= 0) {
            return true;
        }
        return false;
    }
}
