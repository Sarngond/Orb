using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10;
    private float originalSpeed;
    public float crouchSpeed = 5;

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
}
