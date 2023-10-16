using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WreckingBalll : MonoBehaviour
{
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.AddForce(new Vector3(10, 0, 0));
    }


}
