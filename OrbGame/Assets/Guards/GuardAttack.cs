using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardAttack : MonoBehaviour
{
    public float damage = 25f;
    public float range = 100f;

    //public Camera fpsCam;
    public GameObject hitParticles;
    public GameObject gunEnd;
    public LineRenderer line;
    public Light shootlight;

    public float lineMaxLength = 5f;
    public ParticleSystem muzzleFlash;

    public float rateOfFire = 1f;
    private Guard guardScript;
    public float timeInBetweenFire = 0;
    private Animator anim;
    private bool isShooting = false;
    private GuardManager guardManager;

    //public AudioClip shootClip;
    //private AudioSource audioSource;

    private void Start() {
        //audioSource = GetComponent<AudioSource>();
        anim = GetComponentInChildren<Animator>();
        guardScript = GetComponent<Guard>();
        guardManager = FindObjectOfType<GuardManager>();
        line.enabled = false;
    }

    // Update is called once per frame
    void Update() {

        Animate();

        if (guardScript.CanAttack()) {
            timeInBetweenFire -= Time.deltaTime;
            if (timeInBetweenFire > 0) {
                line.enabled = false;
                shootlight.enabled = false;
                isShooting = false;
            }
            else if (timeInBetweenFire <= 0) {
                Shoot();
                isShooting = true;
            }
        }
        else {
            line.enabled = false;
            shootlight.enabled = false;
        }

    }

    void Animate() {
        GameObject player = FindObjectOfType<PlayerMovement>().gameObject;
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        float distToUnconscious = 80f;
        float distToPlayer = Vector3.Distance(player.transform.position, transform.position);
        if (guardManager.unconsciousGuard != null) {
            distToUnconscious = Vector3.Distance(guardManager.unconsciousGuard.transform.position, transform.position);
        }
        if (isShooting && distToPlayer <= agent.stoppingDistance ||
            !guardScript.isMoving) {
            anim.SetBool("isWalking", false);
        }
        if (distToPlayer > agent.stoppingDistance && !isShooting && guardScript.isMoving) {
            anim.SetBool("isWalking", true);
        }
    }

    void Shoot() {
        timeInBetweenFire = rateOfFire;
        GameObject sparks;
        muzzleFlash.Play();
        //audioSource.clip = shootClip;
        //audioSource.Play();

        RaycastHit hit;
        if (Physics.Raycast(transform.position, gunEnd.transform.forward, out hit, range)) {
            PlayerHealth player = hit.transform.GetComponent<PlayerHealth>();
            if (player != null) {
                player.TakeDamage(damage);
            }
            float dist = Vector3.Distance(hit.point, gunEnd.transform.position);

            shootlight.enabled = true;
            line.enabled = true;
            line.SetPosition(1, new Vector3(0, 0, dist - 2f));
            sparks = Instantiate(hitParticles, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(sparks, 10f * Time.deltaTime);
        } else {
            line.enabled = true;
            line.SetPosition(1, new Vector3(0,0,80));
        }
    }
}
