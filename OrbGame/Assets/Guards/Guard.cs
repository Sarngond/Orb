using System.Collections;
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
        } else if (!CanSeePlayer() && !onPatrol) {
            returnTimer -= Time.deltaTime;
            if(returnTimer <= 0) {
                onPatrol = true;
                spotlight.color = Color.yellow;
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

    /*IEnumerator FollowPath(Vector3[] waypoints) {
        transform.position = waypoints[0];

        int targetWaypointIndex = 1;
        Vector3 targetWaypoint = waypoints[targetWaypointIndex];
        transform.LookAt(targetWaypoint);
        //navAgent.SetDestination(targetWaypoint);

        while (true) {
            if (onPatrol) {
                navAgent.enabled = false;
                StartCoroutine(TurnToFace(targetWaypoint));
                //navAgent.SetDestination(targetWaypoint);
                transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, speed * Time.deltaTime);
                //Debug.Log("ah");
                //Debug.Log(atTargetWaypoint);

                //if (atTargetWaypoint) {
                //    navAgent.enabled = true;
                //}
                //if(!atTargetWaypoint) {
                //    navAgent.enabled = false;
                //}

                if (transform.position == targetWaypoint) {
                    //atTargetWaypoint = true;
                    //Debug.Log(atTargetWaypoint);
                    targetWaypointIndex = (targetWaypointIndex + 1) % waypoints.Length;
                    targetWaypoint = waypoints[targetWaypointIndex];
                    yield return new WaitForSeconds(waitTime);
                    yield return StartCoroutine(TurnToFace(targetWaypoint));
                    //navAgent.SetDestination(targetWaypoint);
                }
            } else if(!onPatrol) {
                navAgent.enabled = true;
                navAgent.SetDestination(player.transform.position);
                //atTargetWaypoint = false;
            }
            yield return null;
        }
    }

    IEnumerator TurnToFace(Vector3 lookTarget) {
        Vector3 dirToLookTarget = (lookTarget - transform.position).normalized;
        float targetAngle = 90 - Mathf.Atan2(dirToLookTarget.z, dirToLookTarget.x) * Mathf.Rad2Deg;

        while (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, targetAngle)) > 0.05f) {
            float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngle, turnSpeed * Time.deltaTime);
            transform.eulerAngles = Vector3.up * angle;
            yield return null;
        }
    }*/

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

    /*private void SetWaypoints() {

        Vector3[] waypoints = new Vector3[pathHolder.childCount];
        for (int i = 0; i < waypoints.Length; i++) {
            waypoints[i] = pathHolder.GetChild(i).position;
            waypoints[i] = new Vector3(waypoints[i].x, transform.position.y, waypoints[i].z);
        }

        StartCoroutine(FollowPath(waypoints));
    }*/

    private void FollowPlayer() {

        navAgent.SetDestination(player.transform.position);
        if (CanSeePlayer()) {
            Quaternion targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);
            // Smoothly rotate towards the target point.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
        navAgent.speed = speed;
        navAgent.stoppingDistance = followStopDistance;
    }

    bool CanSeePlayer() {
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
