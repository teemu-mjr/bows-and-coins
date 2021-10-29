using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bow : MonoBehaviour
{
    public GameObject arrow;

    [HideInInspector] public static float heldBackTime = 0;
    [HideInInspector] public static bool isShooting = false;

    private PlayerInputActions playerInputActions;
    private Vector2 shootingVector;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Fire.performed += FirePerformed;
    }

    private void Update()
    {
        shootingVector = playerInputActions.Player.ShootDirection.ReadValue<Vector2>();
        isShooting = shootingVector != Vector2.zero;
        if (isShooting)
        {
            HandleShooting();
        }
        else if (!isShooting && heldBackTime != 0)
        {

        }
    }


    private void HandleShooting()
    {
        heldBackTime += Time.deltaTime;
        RotatePlayerWithInputVector(shootingVector);
        if (Player.stats.repeater && heldBackTime / Player.stats.drawBackDelay.value >= 1)
        {
            Shoot();
        }
    }

    private void FirePerformed(InputAction.CallbackContext context)
    {
        Shoot();
    }

    private void Shoot()
    {
        if (heldBackTime / Player.stats.drawBackDelay.value > 0.2f)
        {
            arrow.GetComponent<Arrow>().heldBackTime = heldBackTime;
            Instantiate(arrow, (transform.position + transform.forward * 0.8f), transform.rotation);
        }
        heldBackTime = 0;
    }

    private void RotatePlayerWithInputVector(Vector2 rotatingVector)
    {
        var angle = Mathf.Atan2(rotatingVector.x, rotatingVector.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(transform.rotation.x, angle, transform.rotation.z);
    }

    private void OnDestroy()
    {
        // Remove subscriptions
        playerInputActions.Player.Fire.performed -= FirePerformed;
    }
}
