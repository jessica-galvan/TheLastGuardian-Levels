using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private float currentSpeed;
    [SerializeField] private LayerMask groundDetectionList;

    //PRIVATE VARIABLES
    private readonly float groundDistance = 1.1f;
    private ActorStats _actorStats;
    private Rigidbody2D rbody;
    private bool isSprinting;
    private bool facingRight = true;
    private float sqrMaxVelocity;

    private void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (_actorStats == null) return;

        var v = rbody.velocity;
        // Clamp the velocity, if necessary
        // Use sqrMagnitude instead of magnitude for performance reasons.
        // Equivalent to: rigidbody.velocity.magnitude > maxVelocity, but faster.
        if (v.sqrMagnitude > sqrMaxVelocity)
        { 
          // Vector3.normalized returns this vector with a magnitude of 1.
          // This ensures that we're not messing with the direction of the vector, only its magnitude.
            rbody.velocity = v.normalized * _actorStats.JumpForce;
        }
    }

    public void SetStats(ActorStats stats)
    {
        _actorStats = stats;
        currentSpeed = _actorStats.OriginalSpeed;
        sqrMaxVelocity = _actorStats.JumpForce * _actorStats.JumpForce;
    }

    public void Move(Vector2 direction)
    {
        //transform.position += (direction * (currentSpeed * Time.deltaTime));
        rbody.velocity += (direction * currentSpeed * Time.deltaTime);
    }

    public void OnMove2D(float horizontal)
    {
        var movement = horizontal * (currentSpeed * Time.deltaTime);
        transform.Translate(Mathf.Abs(movement), 0, 0); //El Mathf.Abs -> Math Absolute le saca los signos. Esto sirve porque al flippear el personaje siempre se mueve hacia adelante y el Flip me lo rota. 

        if (movement < 0 && facingRight) //Si el movimiento es positivo y esta mirando a la derecha...
        {
            Flip();
        }
        else if (movement > 0 && !facingRight) //Si el movimiento es negativo y no esta mirando a la derecha...
        {
            Flip();
        }
    }

    public void Jump()
    {
        if (_actorStats.CanJump && CheckIfGrounded())
        {
            var jumpForce = _actorStats.JumpForce * transform.up;
            rbody.AddForce(jumpForce, ForceMode2D.Impulse);
        }
    }

    public bool CheckIfGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundDistance, groundDetectionList);
        if(hit.collider != null)
            return true;
        else
            return false;
    }

    public void Sprint()
    {
        if (isSprinting)
        {
            currentSpeed = _actorStats.OriginalSpeed;
            isSprinting = false;
        }
        else
        {
            currentSpeed = _actorStats.BuffedSpeed;
            isSprinting = true;
        }
    }

    private void Flip() //Solo flippea al personaje
    {
        transform.Rotate(0f, 180f, 0f);
        facingRight = !facingRight;
    }
}
