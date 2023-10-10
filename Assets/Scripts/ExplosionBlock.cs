using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBlock : MonoBehaviour
{
    public float ActivationTime = 1f;
    public float ExplosionTime = 0.5f;
    public float ReloadTime = 5f;

    //private float timer;

    state currentState;
    enum state
    {
        passive,activated,reload
    }

    private void OnCollisionStay(Collision collision)
    {

        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
         
            if (currentState == state.passive)
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
        Debug.Log("explode");
        //взрыв, дамаг всех

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
