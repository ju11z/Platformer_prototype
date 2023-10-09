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

    public void Jump()
    {
        if (IsGrounded())
        {
            rb.AddForce(new Vector3(0, 100, 0), ForceMode.Impulse);
        }
    }

    public void Move(float moveX, float moveZ)
    {
        Debug.Log(rb.velocity.y);
        rb.MovePosition(rb.position + transform.forward * MovementSpeed *  moveZ * Time.deltaTime);

        float turn = moveX * RotationSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);

    }

    private void FixedUpdate()
    {
        
    }

    }
