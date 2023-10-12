using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLine : MonoBehaviour
{
    private AudioSource audioSource;
    public event Action PlayerCrossedStartLine;

    private void Start()
    {
        audioSource=GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            Debug.Log("PlayerCrossedStartLine.Invoke()");
            PlayerCrossedStartLine.Invoke();

            audioSource.Play();
        }
    }
}
