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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float amount) {
        health -= amount;
        if (health <= 0) {
            sheildFeild.GetComponent<SheildFeild>().PowerDown();
        }
    }
}
