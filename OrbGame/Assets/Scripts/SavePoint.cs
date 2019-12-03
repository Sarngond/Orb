using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    private GameObject player;
    public GameObject[] guards;
    public ParticleSystem particles;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collider) {
        if(collider == player.GetComponent<Collider>()) {
            Debug.Log("Save");
            particles.Play();
            player.GetComponent<PlayerMovement>().SavePlayer();
            foreach (GameObject guard in guards) {
                guard.GetComponent<Guard>().SaveGuard();
            }
        }
    }
}
