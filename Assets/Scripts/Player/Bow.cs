using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class Bow : MonoBehaviour
{
    // public fields
    public GameObject arrow;
    public AudioClip shootAudio;
    public AudioSource audioSource;
    public PowerBar powerBar;
    [HideInInspector] public static float heldBackProcentage = 0;
    [HideInInspector] public static bool isShooting = false;

    // private fields
    private PlayerInputActions playerInputActions;
    private Vector2 shootingVector;
    private float heldBackTime = 0;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
    }

    private void Update()
    {
        shootingVector = playerInputActions.Player.ShootDirection.ReadValue<Vector2>();
        isShooting = shootingVector != Vector2.zero;
        if (isShooting)
        {
            HandleShooting();
        }
        else if (!isShooting && heldBackTime > 0)
        {
            heldBackTime = 0;
            heldBackProcentage = 0;
            powerBar.SetPower(heldBackProcentage);
        }
    }


    private void HandleShooting()
    {
        heldBackTime += Time.deltaTime;

        if (heldBackProcentage < 1)
        {
            heldBackProcentage = heldBackTime / Player.stats.drawBackDelay.value;
            powerBar.SetPower(heldBackProcentage);
        }
        else if (heldBackProcentage > 1)
        {
            heldBackProcentage = 1;
        }

        RotatePlayerWithInputVector(shootingVector);
        if (Player.stats.repeater.value == 1 && heldBackProcentage >= 1)
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
            arrow.GetComponent<Arrow>().heldBackProcentage = heldBackProcentage;
            Instantiate(arrow, (transform.position + transform.forward * 0.8f), transform.rotation);
            if (Player.stats.tripleShot.maxed)
            {
                Instantiate(arrow, (transform.position + transform.forward - transform.right * 0.8f), transform.rotation * Quaternion.Euler(new Vector3(0, -10, 0)));
                Instantiate(arrow, (transform.position + transform.forward + transform.right * 0.8f), transform.rotation * Quaternion.Euler(new Vector3(0, 10, 0)));
            }
            audioSource.PlayOneShot(shootAudio);
        }
        heldBackTime = 0;
        heldBackProcentage = 0;
    }

    private void RotatePlayerWithInputVector(Vector2 rotatingVector)
    {
        var angle = Mathf.Atan2(rotatingVector.x, rotatingVector.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(transform.rotation.x, angle, transform.rotation.z);
    }
    private void OnEnable()
    {
        playerInputActions.Player.Enable();
        // subsccriptions
        playerInputActions.Player.Fire.performed += FirePerformed;
    }

    private void OnDisable()
    {
        // Remove subscriptions
        playerInputActions.Player.Fire.performed -= FirePerformed;

        playerInputActions.Player.Disable();
    }

}
