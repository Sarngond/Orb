﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
    public float speed = 5;
    public float patrolSpeed = 3;
    //public float waitTime = 0.3f;
    public float turnSpeed = 90;
    public bool onPatrol = true;
    public float followStopDistance = 5;
    private bool backToPoint = false;

    public Light spotlight;
    private Color originalLightColor;
    public float viewDistance;
    private float viewAngle;
    public LayerMask viewMask;

    public float returnTimer = 5;
    private float firstReturnTimer;

    public Transform pathHolder;
    private GameObject player;

    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent navAgent;

    public bool spottedUnconscious = false;
    private GameObject unconsciousGuard = null;

    void Start() {

        originalLightColor = spotlight.color;
        spotlight.color = Color.yellow;
        player = GameObject.FindGameObjectWithTag("Player");
        navAgent = GetComponent<NavMeshAgent>();
        viewAngle = spotlight.spotAngle;
        firstReturnTimer = returnTimer;

        GotoNextPoint();

        //SetWaypoints();
    }

    void Update() {

        if (!onPatrol) {
            backToPoint = true;
            FollowPlayer();
        }
        // Choose the next destination point when the agent gets
        // close to the current one.
        if (onPatrol) {
            if (backToPoint) {
                GotoNextPoint();
                backToPoint = false;
            }

            if (!navAgent.pathPending && navAgent.remainingDistance < 0.5f)
                GotoNextPoint();
        }

        if (CanSeePlayer()) {
            onPatrol = false;
            spotlight.color = originalLightColor;
            returnTimer = 5;
        }
        if (!CanSeePlayer()) {

            if (!onPatrol) {
                returnTimer -= Time.deltaTime;
                if (returnTimer <= 0) {
                    onPatrol = true;
                    spotlight.color = Color.yellow;
                }
            }

            if (spottedUnconscious && onPatrol) {
                //onPatrol = false;
                navAgent.stoppingDistance = 2f;
                navAgent.SetDestination(unconsciousGuard.transform.position);
                Quaternion targetRotation = Quaternion.LookRotation(unconsciousGuard.transform.position - transform.position);
                //Smoothly rotate towards the target point.
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
                if (!unconsciousGuard.GetComponent<GuardUnconscious>().isUnconscious()) {
                    navAgent.stoppingDistance = 0f;
                    spottedUnconscious = false;
                    onPatrol = true;
                }
            }
        }
    }

    void GotoNextPoint() {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;
        // Set the agent to go to the currently selected destination.
        navAgent.destination = points[destPoint].position;
        navAgent.speed = patrolSpeed;
        navAgent.stoppingDistance = 0f;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }

    private void OnDrawGizmos() {
        Vector3 startPosition = pathHolder.GetChild(0).position;
        Vector3 previousPosition = startPosition;

        foreach (Transform waypoint in pathHolder) {
            Gizmos.DrawSphere(waypoint.position, 0.3f);
            Gizmos.DrawLine(previousPosition, waypoint.position);
            previousPosition = waypoint.position;
        }
        Gizmos.DrawLine(previousPosition, startPosition);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * viewDistance);
    }

    private void FollowPlayer() {

        navAgent.SetDestination(player.transform.position);
        if (CanSeePlayer()) {
            Quaternion targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);
            // Smoothly rotate towards the target point.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
        navAgent.speed = speed;
        if (!CanSeePlayer()) {
            navAgent.stoppingDistance = 2f;
        }
        else if(CanSeePlayer()){
            navAgent.stoppingDistance = followStopDistance;
        }
    }

    public void SpotUnconscious(GameObject unconscious) {
        if(unconscious == null || !unconscious.GetComponent<GuardUnconscious>().isUnconscious()) {
            spottedUnconscious = false;
            return;
        }

        if (unconscious.GetComponent<GuardUnconscious>().isUnconscious()) {

            turnSpeed = 9;
            if (Vector3.Distance(transform.position, unconscious.transform.position) < viewDistance) {
                Vector3 dirToUnconscious = (unconscious.transform.position - transform.position).normalized;
                float angleBetweenGuardAndUnconscious = Vector3.Angle(transform.forward, dirToUnconscious);
                if (angleBetweenGuardAndUnconscious < viewAngle / 2f) {
                    if (!Physics.Linecast(transform.position, unconscious.transform.position, viewMask)) {
                        unconsciousGuard = unconscious;
                        spottedUnconscious = true;
                    }
                }
            }
        }
    }

    public bool CanAttack() {
        float distToPlayer = Vector3.Distance(player.transform.position, transform.position);
        turnSpeed = 20;
        if (!onPatrol && distToPlayer <= followStopDistance +20f) {
            if(distToPlayer <= followStopDistance + 20f && CanSeePlayer()) {
                return true;
            }
        }
        return false;
    }

    public void CallToAttack() {
        if (CanAttack() || !onPatrol) {
            return;
        }
        Debug.Log(name + " attacking");
        FollowPlayer();
    }

    public bool CanSeePlayer() {
        if(Vector3.Distance(transform.position, player.transform.position) < viewDistance) {
            Vector3 dirToPlayer = (player.transform.position - transform.position).normalized;
            float angleBetweenGuardAndPlayer = Vector3.Angle(transform.forward, dirToPlayer);
            if (angleBetweenGuardAndPlayer < viewAngle / 2f) {
                if(!Physics.Linecast(transform.position, player.transform.position, viewMask)) {
                    return true;
                }
            }
        }
        return false;
    }

}
