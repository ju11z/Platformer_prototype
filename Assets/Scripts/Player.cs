using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float JumpForce = 5;
    public float DefaultMovementSpeed = 10;
    public float DefaultRotationSpeed = 180;
    public int MaxHealth=10;

    private float currentMovementSpeed;
    private float currentRotationSpeed;

    private Rigidbody rb;

    
    private int currentHealth;

    public delegate void HealthHandler(int previousHealth, int healthChange, int currentHealth, int maxHealth);
    public event HealthHandler PlayerDamaged;

    public delegate void DeathHandler();
    public event DeathHandler PlayerDied;

    // public delegate void FinishLineHandler();
    //public event FinishLineHandler PlayerCrossedFinishLine;
    public event Action CrossedStartLine;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        SetDefaultHealth();

        currentMovementSpeed = DefaultMovementSpeed;
        currentRotationSpeed = DefaultRotationSpeed;
    }
    private bool IsGrounded()
    {
        return Math.Abs(rb.velocity.y)<0.2f;
    }

    public void BeDamaged(int damage)
    {
        int oldHealth = currentHealth;
        int newHealth = oldHealth - damage;

        currentHealth = newHealth;

        PlayerDamaged.Invoke(oldHealth, damage, newHealth, MaxHealth);
        currentHealth -= damage;

        Debug.Log($"{newHealth} {MaxHealth}");

        

        if (currentHealth < 0)
        {
            Die();
        }
    }

    public void BeHealed(int heal)
    {
        currentHealth += heal;
    }

    private void Die()
    {
        PlayerDied.Invoke();
    }

    public void FallOff()
    {
        Die();
    }

    public void SetDefaultHealth()
    {
        Debug.Log("SetDefaultHealth");
        currentHealth = MaxHealth;
    }

    public void Jump()
    {
        if (IsGrounded())
        {
            rb.AddForce(new Vector3(0, JumpForce, 0), ForceMode.Impulse);
        }
    }

    public void BeForced(Vector3 force)
    {
        rb.AddForce(force, ForceMode.Impulse);
    }

    public void BeUnderWindInfluence(Vector3 direction, float force)
    {
        rb.AddForce(direction * force, ForceMode.Force);
    }

    public void BeSlowedDown()
    {
        currentMovementSpeed = DefaultMovementSpeed / 2;
        currentRotationSpeed = DefaultRotationSpeed / 2;
    }

    public void BeSpedUp()
    {
        currentMovementSpeed = DefaultMovementSpeed;
        currentRotationSpeed = DefaultRotationSpeed;
    }

    public void Move(float moveX, float moveZ)
    {
        //Debug.Log(rb.velocity.y);
        rb.MovePosition(rb.position + transform.forward * currentMovementSpeed *  moveZ * Time.deltaTime);

        float turn = moveX * currentRotationSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);

    }

    private void FixedUpdate()
    {
        
    }

    }
