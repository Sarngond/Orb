using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerGenerator : MonoBehaviour
{
    public float health = 10f;
    private GameObject sheildFeild;

    // Start is called before the first frame update
    void Start()
    {
        sheildFeild = FindObjectOfType<SheildFeild>().gameObject;

        LoadGenerator();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0) {
            sheildFeild.GetComponent<SheildFeild>().PowerDown();
        }
    }

    public void TakeDamage(float amount) {
        health -= amount;
    }

    public void SaveGenerator() {
        SaveSystem.SaveGenerator(gameObject);
    }

    public void LoadGenerator() {
        GeneratorData data = SaveSystem.LoadGenerator(gameObject);
        health = data.health;
    }
}
