using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBlock : MonoBehaviour
{
    public ParticleSystem Slime;

    private bool letPlay;
    private bool playerIsOnSlime;


    private Vector3 playerPosition;
    private float slimeSpawnRate = 2f;
    private float slimeTime;
    public void Update()
    {

        if (letPlay)
        {
            if (Slime.isPlaying)
            {
                Slime.Play();
            }
        }
        else
        {
            if (Slime.isPlaying)
            {
                Slime.Stop();
            }
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            playerIsOnSlime = true;
            player.BeSlowedDown();
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
