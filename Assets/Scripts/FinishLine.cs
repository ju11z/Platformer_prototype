using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private AudioSource audioSource;
    public event Action PlayerCrossedFinishLine;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            //Debug.Log("PlayerCrossedFinishLine.Invoke()");
            PlayerCrossedFinishLine.Invoke();

            audioSource.Play();
        }
    }
}
