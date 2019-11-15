using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
    public float speed = 5;
    public float patrolSpeed = 3;
    public float waitTime = 0.3f;
    public float turnSpeed = 90;
    public bool onPatrol = true;

    public float followStopDistance = 5;

    public Transform pathHolder;
    public GameObject player;

    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent navAgent;

    private bool atTargetWaypoint = false;

    void Start() {
        navAgent = GetComponent<NavMeshAgent>();
        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).

        GotoNextPoint();

        //SetWaypoints();
    }

    void Update() {
        FollowPlayer();
        // Choose the next destination point when the agent gets
        // close to the current one.
        if (!navAgent.pathPending && navAgent.remainingDistance < 0.5f && onPatrol)
            GotoNextPoint();
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

        if (!onPatrol) {
            //navAgent.enabled = true;
            navAgent.SetDestination(player.transform.position);
            navAgent.speed = speed;
            navAgent.stoppingDistance = followStopDistance;
        }
    }

}
