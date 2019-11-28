using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OrbFollow : MonoBehaviour
{
    public Transform player;
    public float smooth = 0.3f;
    public Vector3 offset;

    private Vector3 velocity = Vector3.zero;
    private NavMeshAgent agent;
    private NavMeshPath path;

    public float viewDistance;
    private float viewAngle = 360f;
    public LayerMask viewMask;

    private bool follow = false;

    private void Start() {
        agent = GetComponent<NavMeshAgent>();
    }

    //Methods
    private void Update() {
        if (CanSeePlayer()) {
            follow = true;
        }
        FollowPlayer();
        //Vector3 pos = new Vector3();
        //pos = player.position + offset;
        //transform.position = Vector3.SmoothDamp(transform.position, pos, ref velocity, smooth);
    }

    void FollowPlayer() {
        if (follow) {
            path = new NavMeshPath();
            agent.CalculatePath(player.transform.position, path);
            agent.SetPath(path);
        }
    }

    public bool CanSeePlayer() {
        if (Vector3.Distance(transform.position, player.transform.position) < viewDistance) {
            Vector3 dirToPlayer = (player.transform.position - transform.position).normalized;
            float angleBetweenGuardAndPlayer = Vector3.Angle(transform.forward, dirToPlayer);
            if (angleBetweenGuardAndPlayer < viewAngle / 2f) {

                if (!Physics.Linecast(transform.position, player.transform.position, viewMask)) {
                    return true;
                }
            }
        }
        return false;
    }
}
