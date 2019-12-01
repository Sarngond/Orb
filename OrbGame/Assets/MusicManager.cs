using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    public AudioClip sneakMusic;
    public AudioClip sneakIntro;
    public AudioClip attackMusic;
    public AudioClip attackIntro;
    private AudioSource audioSource;

    public  bool sneak = true;
    public bool attack = false;

    public float timeTillMain = 0f;
    public bool playedSneakIntro = false;
    public bool playedAttackIntro = false;

    // Start is called before the first frame update
    void Start()
    {
        timeTillMain = sneakIntro.length;
        audioSource = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ManageMusic();
        timeTillMain -= Time.deltaTime;
    }

    void ManageMusic() {
        GameObject guard = FindObjectOfType<Guard>().gameObject;
        if (!guard.GetComponent<Guard>().attackingPlayer) {
            PlaySneak();
        }
        else if (guard.GetComponent<Guard>().attackingPlayer) {
            PlayAttack();
        }
    }

    void PlaySneak() {
        playedAttackIntro = false;
        if (audioSource.clip != sneakIntro && !playedSneakIntro) {
            timeTillMain = sneakIntro.length;
            audioSource.clip = sneakIntro;
            audioSource.Play();
            playedSneakIntro = true;
        }

        if (timeTillMain <= 0) {
            if (audioSource.clip != sneakMusic) {
                audioSource.clip = sneakMusic;
                audioSource.Play();
            }
        }
    }

    void PlayAttack() {
        playedSneakIntro = false;
        if (audioSource.clip != attackIntro && !playedAttackIntro) {
            timeTillMain = attackIntro.length;
            audioSource.clip = attackIntro;
            audioSource.Play();
            playedAttackIntro = true;
        }

        if (timeTillMain <= 0) {
            if (audioSource.clip != attackMusic) {
                audioSource.clip = attackMusic;
                audioSource.Play();
            }
        }
    }
}
