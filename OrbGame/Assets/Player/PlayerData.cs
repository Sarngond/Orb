using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{

    public float health;
    public string cardStatus;
    public float[] position;

    public PlayerData(GameObject player) {
        //health = player.GetComponent<PlayerHealth>().health;
        cardStatus = player.GetComponent<PlayerItems>().hasCardString; 

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;

    }

}
