using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        transform.parent.GetComponent<HammerWrapper>().CollisionDetected(collision);
    }
}
