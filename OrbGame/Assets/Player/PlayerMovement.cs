using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10;
    public GameObject camera;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*Plane playerPlane = new Plane(Vector3.up, player.transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitDist = 0.0f;

        if(playerPlane.Raycast(ray, out hitDist)) {
            Vector3 targetPoint = ray.GetPoint(hitDist);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - player.transform.position);
            targetRotation.x = 0;
            targetRotation.z = 0;
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, targetRotation, 15f * Time.deltaTime);
        }*/

        Move();
    }

    private void Move() {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(h, 0, v) * moveSpeed;

        //float mouseX = Input.mousePosition.;
        //Vector3 rotation = new Vector3(0, mouseX, 0);

        transform.Translate(movement * Time.deltaTime);
        //transform.Rotate(rotation);
    }
}
