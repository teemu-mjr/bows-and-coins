using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    private Vector2 movementVector;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        movementVector = playerInputActions.Player.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        if (movementVector != Vector2.zero)
        {
            MoveCharacter();
            if (!Bow.isShooting)
            {
                HandlePlayerRotation();
            }
        }
        SlowDownCharacter();
    }

    private void MoveCharacter()
    {
        if (rb.velocity.magnitude < 10)
        {
            rb.AddForce(new Vector3(movementVector.x, 0, movementVector.y) * Player.stats.movementSpeed);
        }
    }

    private void SlowDownCharacter()
    {
        if (Mathf.Round(rb.velocity.magnitude) != 0)
        {
            rb.velocity = new Vector3(rb.velocity.x * 0.80f, rb.velocity.y, rb.velocity.z * 0.80f);
        }
    }

    private void HandlePlayerRotation()
    {
        RotatePlayerWithInputVector(movementVector);

    }

    private void RotatePlayerWithInputVector(Vector2 rotatingVector)
    {
        var angle = Mathf.Atan2(rotatingVector.x, rotatingVector.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(transform.rotation.x, angle, transform.rotation.z);
    }
}
