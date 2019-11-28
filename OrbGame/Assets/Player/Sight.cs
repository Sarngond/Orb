using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight : MonoBehaviour
{
    public GameObject gunEnd;
    public float range = 100f;
    private LineRenderer line;

    public LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, gunEnd.transform.right, out hit, range, layerMask)) {
            //EnemyHealth enemy = hit.transform.GetComponent<EnemyHealth>();
            //if (enemy != null) {
            //    enemy.TakeDamage(damage);
            //}
            float dist = Vector3.Distance(hit.point, gunEnd.transform.position) * 2;
            line.SetPosition(1, new Vector3(0, 0, dist));
        }
        else {
            line.SetPosition(1, new Vector3(0, 0, 80));
        }
    }
}
