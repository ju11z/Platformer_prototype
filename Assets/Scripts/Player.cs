using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float MovementSpeed = 10;
    public float RotationSpeed = 180;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private bool IsGrounded()
    {
        return rb.velocity.y == 0;
    }

    private void FixedUpdate()
    {
        Debug.Log(rb.velocity.y);
        rb.MovePosition(rb.position + transform.forward * MovementSpeed * Input.GetAxis("Vertical") * Time.deltaTime);

        float turn = Input.GetAxis("Horizontal") * RotationSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);


        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.AddForce(new Vector3(0, 100, 0), ForceMode.Impulse);
        }
    }

    }
