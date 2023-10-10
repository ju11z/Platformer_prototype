using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLine : MonoBehaviour
{
    public event Action PlayerCrossedStartLine;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            Debug.Log("PlayerCrossedStartLine.Invoke()");
            PlayerCrossedStartLine.Invoke();
        }
    }
}
