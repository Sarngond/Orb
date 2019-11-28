﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimate : MonoBehaviour
{
    private bool canMove = false;
    private Animator anim;

    private Transform cam;
    private Vector3 camForward;
    private Vector3 move;
    private Vector3 moveInput;

    private float fowardAmount;
    private float turnAmount;

    public Collider playerCollider;
    public Collider crouchCollider;
    public Collider crouchCollider2;

    private bool isCrouched = false;

    public GameObject gunEnd;
    public bool isMeleeing = false;

    public AnimationClip meleeClip;
    private float timeTillCanMelee = 0f;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        cam = Camera.main.transform;

        timeTillCanMelee = meleeClip.length;
    }

    // Update is called once per frame
    void Update()
    {
        Plane playerPlane = new Plane(Vector3.up, gunEnd.transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitDist = 0.0f;

        if (playerPlane.Raycast(ray, out hitDist)) {
            Vector3 targetPoint = ray.GetPoint(hitDist);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - gunEnd.transform.position);
            targetRotation.x = 0;
            targetRotation.z = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 50f * Time.deltaTime);
        }

        Animate();
        Meleeing();
    }

    void Animate() {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (cam != null) {
            camForward = Vector3.Scale(cam.up, new Vector3(1, 0, 1)).normalized;
            move = v * camForward + h * cam.right;
        }
        else {
            move = v * Vector3.forward + h * Vector3.right;
        }

        if (move.magnitude > 1) {
            move.Normalize();
        }

        Move(move);

        if (h == 0 && v == 0) {
            canMove = false;
        }
        else {
            canMove = true;
        }

        anim.SetBool("isWalking", canMove);

        if (Input.GetButton("Crouch")) {
            isCrouched = true;
            anim.SetBool("isCrouching", true);
            crouchCollider.enabled = true;
            crouchCollider2.enabled = true;
            playerCollider.enabled = false;
        }
        else {
            isCrouched = false;
            anim.SetBool("isCrouching", false);
            crouchCollider.enabled = false;
            crouchCollider2.enabled = false;
            playerCollider.enabled = true;
        }

        if (Input.GetButtonDown("Fire2")) {
            if (!isMeleeing) {
                SetIsMelee();
                anim.SetTrigger("Melee");
                Invoke("SetIsMelee", meleeClip.length);
            }
        }
    }

    public void SetIsMelee() {
        if (isMeleeing) {
            isMeleeing = false;
        } else if (!isMeleeing) {
            isMeleeing = true;
        }
    }

    public bool Crouching() {
        if (isCrouched) {
            return true;
        } else {
            return false;
        }
    }

    public bool Meleeing() {
        if (!isMeleeing) {
            return false;
        }
        else {
            return true;
        }
    }

    void Move(Vector3 move) {
        if (move.magnitude > 1) {
            move.Normalize();
        }

        this.moveInput = move;

        ConvertMoveInput();
        UpdateAnimator();
    }

    void ConvertMoveInput() {
        Vector3 localMove = transform.InverseTransformDirection(moveInput);
        turnAmount = localMove.x;

        fowardAmount = localMove.z;
    }

    void UpdateAnimator() {
        anim.SetFloat("VelocityZ", fowardAmount, 0.1f, Time.deltaTime);
        anim.SetFloat("VelocityX", turnAmount, 0.1f, Time.deltaTime);
    }

}
