using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GuardData
{
    public float health;
    public string dead;
    public float[] position;

    public GuardData(GameObject guard) {

        //health = guard.GetComponent<EnemyHealth>().health;

        dead = guard.GetComponent<EnemyHealth>().deadString;

        position = new float[3];
        position[0] = guard.transform.position.x;
        position[1] = guard.transform.position.y;
        position[2] = guard.transform.position.z;

    }
}
