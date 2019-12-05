using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GeneratorData
{
    public float health;

    public GeneratorData(GameObject generator) {

        health = generator.GetComponent<PowerGenerator>().health;

    }
}
