using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float JumpForce = 5;
    public float DefaultMovementSpeed = 10;
    public float DefaultRotationSpeed = 180;
    public int MaxHealth = 10;
    public float DeathFallingPositionY= -40;
    public float FallAnimationVelocityY = -10;

    private float currentMovementSpeed;
    private float currentRotationSpeed;

    private Rigidbody rb;

    private int currentHealth;

    private Animator animator;

    public delegate void HealthHandler(int previousHealth, int healthChange, int currentHealth, int maxHealth);
    public event HealthHandler PlayerDamaged;

    public delegate void DeathHandler();
    public event DeathHandler PlayerDied;

    // public delegate void FinishLineHandler();
    //public event FinishLineHandler PlayerCrossedFinishLine;
    public event Action CrossedStartLine;

    private void Start()
    {
        Debug.Log("player is instantiated");

        rb = GetComponent<Rigidbody>();

        animator = GetComponent<Animator>();

        SetDefaultHealth();

        currentMovementSpeed = DefaultMovementSpeed;
        currentRotationSpeed = DefaultRotationSpeed;

        ResetAllForces();
    }
    private bool IsGrounded()
    {
        return Math.Abs(rb.velocity.y) < 0.2f;
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

            animator.SetTrigger("IsJumping");
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
        if (moveX == 0 && moveZ == 0)
        {
            animator.SetBool("IsRunning", false);
            return;
        }
        //Debug.Log(rb.velocity.y);
        rb.MovePosition(rb.position + transform.forward * currentMovementSpeed * moveZ * Time.deltaTime);

        float turn = moveX * currentRotationSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);

        animator.SetBool("IsRunning", true);

    }

    public void ResetAllForces()
    {
        Debug.Log("resetallforces");
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }


    private void FixedUpdate()
    {
        if (rb.velocity.y < FallAnimationVelocityY)
        {
            animator.SetBool("isFalling", true);
        }
        else
        {
            animator.SetBool("isFalling", false);
        }

        if(transform.position.y < DeathFallingPositionY)
        {
            Die();
        }

        Debug.Log($"velocity y {rb.velocity.y}");
    }
}

    
