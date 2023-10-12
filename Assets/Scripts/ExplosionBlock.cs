//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBlock : MonoBehaviour
{
    public float ActivationTime = 1f;
    public float ExplosionTime = 0.5f;
    public float ReloadTime = 5f;

    private BoxCollider collider;
    private List<Player> players=new List<Player>();

    public ParticleSystem explosion;

    state currentState;
    enum state
    {
        passive,activated,reload
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {

            if (!players.Contains(player))
            {
                players.Add(player);
            }

        }

        //Debug.Log(players.Count);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
                players.Remove(player);
        }

        //Debug.Log(players.Count);
    }

    private void OnCollisionStay(Collision collision)
    {
            if (currentState == state.passive)
            {
                if (collision.gameObject.TryGetComponent<Player>(out Player player))
                {
                    SetActivationState();

                    return;
                }
                
            }
    }

    private void SetActivationState()
    {
        currentState = state.activated;

        gameObject.GetComponent<Renderer>().material.color = Color.yellow;

        Invoke("Explode", ActivationTime);
    }

    private void SetReloadState()
    {
        currentState = state.reload;

        gameObject.GetComponent<Renderer>().material.color = Color.grey;

        Invoke("SetPassiveState", ReloadTime);
    }

    private void Explode()
    {
        //Debug.Log("explode");

        Instantiate(explosion,transform);
        //взрыв, дамаг всех

        foreach (Player player in players)
        {
            player.BeDamaged(2);
            Vector3 force = new Vector3(Random.Range(0,2), Random.Range(3, 6), Random.Range(0, 2));
            player.BeForced(force);
        }

        gameObject.GetComponent<Renderer>().material.color = Color.red;

        Invoke("SetReloadState", ExplosionTime);
    }

    private void SetPassiveState()
    {
        currentState = state.passive;

        gameObject.GetComponent<Renderer>().material.color = Color.green;
    }




    private void Update()
    {
        /*
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        */
    }

    

}
