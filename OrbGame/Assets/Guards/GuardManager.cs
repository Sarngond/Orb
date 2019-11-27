using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardManager : MonoBehaviour {

    public GameObject[] guards;
    public List<GameObject> guardList;
    private Guard guardScript;
    public GameObject unconsciousGuard = null;
    private GameObject player;
    private Transform playersLastPos;
    private bool playerPosSet = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        guards = GameObject.FindGameObjectsWithTag("Guard");
        foreach (GameObject guard in guards) {
            guardList.Add(guard);
        }
    }

    // Update is called once per frame
    void Update()
    {

        HearPlayer();
        SeePlayer();
        FindUnconsciousGuards();
    }

    public void RemoveGuardFromList(GameObject guard) {
        int guardIndex;
        foreach (GameObject enemy in guardList) {
            if (enemy.GetComponentInChildren<EnemyHealth>().Dead()) {
                guardIndex = guardList.IndexOf(enemy);
                Debug.Log(guardIndex);
                guardList.RemoveAt(guardIndex);
                guardList.Remove(guard);
            }
        }
    }

    void FindUnconsciousGuards() {

        foreach (GameObject guard in guardList) {
            if (guard.tag == "Unconscious Guard") {
                unconsciousGuard = guard;
            }
            if (guard.tag == "Guard") {
                if (!guard.GetComponentInChildren<GuardUnconscious>().isUnconscious() && unconsciousGuard != null) {
                    if (unconsciousGuard.GetComponent<GuardUnconscious>().isUnconscious()) {
                        guard.GetComponent<Guard>().SpotUnconscious(unconsciousGuard);
                    }
                }
            }
        }
    }

    void SeePlayer() {
        foreach (GameObject guard in guardList) {
            if (guard.GetComponent<Guard>().CanSeePlayer()) {
                playersLastPos = guard.transform;
                if (!playerPosSet) {
                    playersLastPos = guard.transform;
                    playerPosSet = true;
                }
                
                foreach (GameObject guard2 in guardList) {
                    guard2.GetComponent<Guard>().GoToPosition(playersLastPos);
                }
            } else {
                playerPosSet = true;
            }

        }
    }

    void HearPlayer() {
        foreach (GameObject guard in guardList) {
            if (player.GetComponentInChildren<PlayerAttack>().Shooting()) {
                playersLastPos = player.transform;
                if (!playerPosSet) {
                    playerPosSet = true;
                }
                Debug.Log("Heard that");
                guard.GetComponent<Guard>().GoToPosition(playersLastPos);
            } else {
                playerPosSet = true;
            }
        }
    }
}
