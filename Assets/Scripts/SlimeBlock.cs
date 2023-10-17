using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBlock : MonoBehaviour
{
    /*
     public AudioClip windSound;

    private float windForce;
    private Vector3 windDirection;
    private bool windSoundIsPlaying;

    private AudioSource audioSource;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            audioSource.PlayOneShot(windSound);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            audioSource.Stop();

            player.ResetAllForces();
        }
    }
     */
    public ParticleSystem Slime;
    public AudioClip slimeSound;

    private bool letPlay;
    private bool playerIsOnSlime;

    private AudioSource audioSource;

    private Vector3 playerPosition;
    private float slimeSpawnRate = 2f;
    private float slimeTime;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            playerIsOnSlime = true;
            player.BeSlowedDown();

            audioSource.PlayOneShot(slimeSound);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            //Instantiate(Slime, player.transform);
            playerPosition = player.transform.position;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            playerIsOnSlime = false;
            player.BeSpedUp();

            audioSource.Stop();
        }
    }

    private void FixedUpdate()
    {
        slimeTime -= Time.fixedDeltaTime;

        if (playerIsOnSlime && slimeTime <= 0.0f)
        {
            slimeTime = slimeSpawnRate;

            Instantiate(Slime, playerPosition, Quaternion.identity);// create your bullet, apply force, etc
        }
    }

}
