using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // public fields
    [HideInInspector] public Vector2 movementVector;

    // private field
    private PlayerInputActions playerInputActions;
    private Rigidbody rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
    }

    void Update()
    {
        movementVector = playerInputActions.Player.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        if (movementVector != Vector2.zero)
        {
            MoveCharacter();
            HandlePlayerRotation();
        }
        SlowDownCharacter();
    }

    private void MoveCharacter()
    {
        if (rb.velocity.magnitude < 10)
        {
            rb.AddForce(new Vector3(movementVector.x, 0, movementVector.y) * Player.stats.movementSpeed.value);
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
        if (!Bow.isShooting)
        {
            RotatePlayerWithInputVector();
        }
    }

    private void RotatePlayerWithInputVector()
    {
        var angle = Mathf.Atan2(movementVector.x, movementVector.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Lerp(Quaternion.Euler(transform.rotation.eulerAngles), Quaternion.Euler(transform.rotation.x, angle, transform.rotation.z), Time.deltaTime * 10);
    }

    private void OnEnable()
    {
        playerInputActions.Player.Enable();
        PlayerHealth.OnPlayerDeath += PlayerHealth_OnPlayerDeath;
    }

    private void PlayerHealth_OnPlayerDeath(object sender, System.EventArgs e)
    {
        this.enabled = false;
        rb.AddForce(Vector3.up * 100, ForceMode.Impulse);
    }

    private void OnDisable()
    {
        playerInputActions.Player.Disable();
        PlayerHealth.OnPlayerDeath -= PlayerHealth_OnPlayerDeath;
    }
}
