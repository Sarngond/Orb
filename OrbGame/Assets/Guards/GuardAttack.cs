using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardAttack : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;

    //public Camera fpsCam;
    public GameObject hitParticles;
    public GameObject gunEnd;
    public LineRenderer line;
    public Light light;

    public float lineMaxLength = 5f;
    public ParticleSystem muzzleFlash;

    public float rateOfFire = 1f;
    private Guard guardScript;
    public float timeInBetweenFire = 0;
    private Animator anim;

    //public AudioClip shootClip;
    //private AudioSource audioSource;

    private void Start() {
        //audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        guardScript = GetComponent<Guard>();
        line.enabled = false;
    }

    // Update is called once per frame
    void Update() {

        if (guardScript.CanAttack()) {
            timeInBetweenFire -= Time.deltaTime;
            if (timeInBetweenFire > 0) {
                line.enabled = false;
                light.enabled = false;
            }
            else if (timeInBetweenFire <= 0) {
                Shoot();
                anim.SetTrigger("Shoot");

            }
        }
        else {
            line.enabled = false;
            light.enabled = false;
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
            //EnemyHealth enemy = hit.transform.GetComponent<EnemyHealth>();
            //if (enemy != null) {
            //    enemy.TakeDamage(damage);
            //}
            float dist = Vector3.Distance(hit.point, gunEnd.transform.position);

            light.enabled = true;
            line.enabled = true;
            line.SetPosition(1, new Vector3(0, 0, dist - 0.2f));
            sparks = Instantiate(hitParticles, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(sparks, 10f * Time.deltaTime);
        } else {
            line.enabled = true;
            line.SetPosition(1, new Vector3(0,0,30));
        }

    }
}
