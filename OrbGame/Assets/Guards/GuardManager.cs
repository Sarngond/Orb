using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardManager : MonoBehaviour {

    public GameObject[] guards;
    public List<GameObject> guardList;
    public List<GameObject> unconsciousList;
    //public List<GameObject> deadList;
    private Guard guardScript;
    public GameObject unconsciousGuard = null;
    private GameObject player;
    private Transform playersLastPos;
    private bool playerPosSet = false;

    public bool addedUnconscious = false;

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

        if (guardList != null) {
            HearPlayer();
            SeePlayer();
            FindUnconsciousGuards();
        }
    }

    public void RemoveGuardFromList(GameObject guard) {
        int guardIndex;
        foreach (GameObject enemy in guardList) {
            if (enemy.GetComponentInChildren<EnemyHealth>().Dead()) {
                guardIndex = guardList.IndexOf(enemy);
                guardList.RemoveAt(guardIndex);
                guardList.Remove(guard);
            }
        }
    }

    void FindUnconsciousGuards() {

        foreach (GameObject guard in guardList) {
            if (guard.tag == "Unconscious Guard") {
                unconsciousGuard = guard;
                if (!addedUnconscious) {
                    unconsciousList.Add(guard);
                    addedUnconscious = true;
                }
            }
            if (guard.tag == "Guard") {
                if (!guard.GetComponentInChildren<GuardUnconscious>().isUnconscious() && unconsciousGuard != null) {
                    if (unconsciousGuard.GetComponent<GuardUnconscious>().isUnconscious()) {
                        guard.GetComponent<Guard>().SpotUnconscious(unconsciousGuard);
                    }
                    if (!unconsciousGuard.GetComponent<GuardUnconscious>().isUnconscious()) {
                        unconsciousList.Remove(guard);
                        unconsciousGuard = null;
                        addedUnconscious = false;
                    }
                }
            }
        }
    }

    void SeePlayer() {
        foreach (GameObject guard in guardList) {
            if (!unconsciousList.Contains(guard)) {
                //if (guard.GetComponent<Guard>().CanSeePlayer()) {
                if (guard.GetComponent<GuardAttack>().isShooting) {
                    //playersLastPos = guard.transform;
                    if (!playerPosSet) {
                        playersLastPos = player.transform;
                        playerPosSet = true;
                    }

                    foreach (GameObject guard2 in guardList) {
                        guard2.GetComponent<Guard>().GoToPosition(playersLastPos);
                    }
                }
                else {
                    playerPosSet = false;
                    playersLastPos = null;
                }

            }

        }
    }

    void HearPlayer() {
        foreach (GameObject guard in guardList) {
            if (!unconsciousList.Contains(guard)) {
                if (player.GetComponentInChildren<PlayerAttack>().Shooting()) {
                    playersLastPos = player.transform;
                    if (!playerPosSet) {
                        playerPosSet = true;
                    }
                    guard.GetComponent<Guard>().GoToPosition(playersLastPos);
                }
                else {
                    playerPosSet = true;
                }
            }
        }
    }
}
