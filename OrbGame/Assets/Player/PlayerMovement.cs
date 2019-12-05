using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10;
    private float originalSpeed;
    public float crouchSpeed = 5;

    //public GameObject orb;

    // Start is called before the first frame update
    void Start()
    {
        originalSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move() {
        float h = Input.GetAxis("Horizontal") * moveSpeed;
        float v = Input.GetAxis("Vertical") * moveSpeed;
        Vector3 movement = new Vector3(h, 0, v);

        transform.Translate(Vector3.ClampMagnitude(movement, moveSpeed) * Time.deltaTime);

        if (Input.GetButton("Crouch")) {
            moveSpeed = crouchSpeed;
        }
        else {
            moveSpeed = originalSpeed;
        }
    }

    public void SavePlayer() {
        SaveSystem.SavePlayer(gameObject);
    }

    public void LoadPlayer() {
        PlayerData data = SaveSystem.LoadPlayer();

        //GetComponent<PlayerHealth>().health = data.health;
        GetComponent<PlayerItems>().hasCardString = data.cardStatus;
        Debug.Log(data.cardStatus);

        if (data.position != null) {
            Vector3 position;
            position.x = data.position[0];
            position.y = data.position[1];
            position.z = data.position[2];

            transform.position = position;
        }
        //orb.GetComponent<NavMeshAgent>().enabled = false;
        //orb.transform.position = position;
        //orb.GetComponent<NavMeshAgent>().enabled = true;
    }
}
