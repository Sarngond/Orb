using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
    public float speed = 5;
    public float waitTime = 0.3f;
    public float turnSpeed = 90;
    public bool onPatrol = true;

    public Transform pathHolder;
    public GameObject player;

    private NavMeshAgent navAgent;
    private bool atTargetWaypoint = false;

    void Start() {
        navAgent = GetComponent<NavMeshAgent>();

        SetWaypoints();
    }

    void Update() {
        //FollowPlayer();
    }

    IEnumerator FollowPath(Vector3[] waypoints) {
        transform.position = waypoints[0];

        int targetWaypointIndex = 1;
        Vector3 targetWaypoint = waypoints[targetWaypointIndex];
        transform.LookAt(targetWaypoint);
        //navAgent.SetDestination(targetWaypoint);

        while (true) {
            if (onPatrol) {
                StartCoroutine(TurnToFace(targetWaypoint));
                //navAgent.SetDestination(targetWaypoint);
                transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, speed * Time.deltaTime);
                Debug.Log("ah");
                if (transform.position == targetWaypoint) {
                    Debug.Log("at target");
                    targetWaypointIndex = (targetWaypointIndex + 1) % waypoints.Length;
                    targetWaypoint = waypoints[targetWaypointIndex];
                    yield return new WaitForSeconds(waitTime);
                    yield return StartCoroutine(TurnToFace(targetWaypoint));
                    //navAgent.SetDestination(targetWaypoint);
                }
            } else if(!onPatrol) {
                navAgent.enabled = true;
                navAgent.SetDestination(player.transform.position);
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
    }

    private void SetWaypoints() {

        Vector3[] waypoints = new Vector3[pathHolder.childCount];
        for (int i = 0; i < waypoints.Length; i++) {
            waypoints[i] = pathHolder.GetChild(i).position;
            waypoints[i] = new Vector3(waypoints[i].x, transform.position.y, waypoints[i].z);
        }

        StartCoroutine(FollowPath(waypoints));
    }

    /*private void FollowPlayer() {

        if (!onPatrol) {
            navAgent.enabled = true;
            navAgent.SetDestination(player.transform.position);
        }
    }*/

}
