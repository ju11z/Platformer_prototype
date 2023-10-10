using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public event Action PlayerCrossedFinishLine;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            Debug.Log("PlayerCrossedFinishLine.Invoke()");
            PlayerCrossedFinishLine.Invoke();
        }
    }
}
