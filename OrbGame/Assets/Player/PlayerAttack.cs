using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public float damage = 20f;
    public float range = 100f;

    //public Camera fpsCam;
    public GameObject hitParticles;
    public GameObject gunEnd;
    public LineRenderer line;
    public Light shootlight;

    //public float lineMaxLength = 5f;
    public ParticleSystem muzzleFlash;

    public float rateOfFire = 1f;
    public float timeInBetweenFire = 0;
    private bool fired = false;
    public bool isShooting = false;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        line.enabled = false;
        shootlight.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (line.enabled) {
            isShooting = true;
        } else if (!line.enabled) {
            isShooting = false;
        }


        if (timeInBetweenFire < rateOfFire) {
            line.enabled = false;
            shootlight.enabled = false;
        }

        if (fired) {
            timeInBetweenFire -= Time.deltaTime;
        }
        if(timeInBetweenFire <= 0) {
            fired = false;
            timeInBetweenFire = 0;
        }

        if (Input.GetButtonDown("Fire1")) {
            if (!fired) {
                Shoot();
                //anim.SetTrigger("Shoot");
            }
            else if (fired) {
                line.enabled = false;
                shootlight.enabled = false;
            }
        }

    }

    void Shoot() {
        timeInBetweenFire = rateOfFire;

        GameObject sparks;
        muzzleFlash.Play();
        //audioSource.clip = shootClip;
        //audioSource.Play();

        RaycastHit hit;
        if (Physics.Raycast(gunEnd.transform.position, gunEnd.transform.right, out hit, range)) {
            EnemyHealth enemy;
            enemy = hit.transform.GetComponent<EnemyHealth>();
            if (enemy != null) {
                enemy.TakeDamage(damage);
            }
            PowerGenerator generator;
            generator = hit.transform.GetComponent<PowerGenerator>();
            if (generator != null) {
                generator.TakeDamage(damage);
            }

            float dist = Vector3.Distance(gunEnd.transform.position, hit.point) * 2;

            shootlight.enabled = true;
            line.enabled = true;
            line.SetPosition(1, new Vector3(0, 0, dist));

            sparks = Instantiate(hitParticles, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(sparks, 2f * Time.deltaTime);
            fired = true;
        }
        else {
            shootlight.enabled = true;
            line.enabled = true;
            line.SetPosition(1, new Vector3(0, 0, 80));
            fired = true;
        }
    }

    public bool Shooting() {
        if (isShooting) {
            return true;
        }
        return false;
    }

}
