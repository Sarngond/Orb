using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimate : MonoBehaviour
{
    private bool canMove = false;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitDist = 0.0f;

        if (playerPlane.Raycast(ray, out hitDist)) {
            Vector3 targetPoint = ray.GetPoint(hitDist);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            targetRotation.x = 0;
            targetRotation.z = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 15f * Time.deltaTime);
        }

        Animate();
    }

    void Animate() {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (h == 0 && v == 0) {
            canMove = false;
        }
        else {
            canMove = true;
        }

        anim.SetBool("isWalking", canMove);
        anim.SetFloat("VelocityX", h);
        anim.SetFloat("VelocityZ", v);
    }
}
