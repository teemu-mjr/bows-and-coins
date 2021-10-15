using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public GameObject arrow;
    public static float heldBackTime = 0;
    public static bool isShooting = false;

    private PlayerInputActions playerInputActions;
    private Vector2 shootingVector;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }

    private void Update()
    {
        shootingVector = playerInputActions.Player.Shoot.ReadValue<Vector2>();
        if (shootingVector != Vector2.zero && !isShooting)
        {
            isShooting = true;
        }
    }

    private void FixedUpdate()
    {
        if (shootingVector != Vector2.zero)
        {
            HandleShooting();
        }
        else if (shootingVector == Vector2.zero && heldBackTime != 0)
        {
            Shoot();
        }
    }

    private void HandleShooting()
    {
        heldBackTime += Time.deltaTime;
        RotatePlayerWithInputVector(shootingVector);
    }

    private void Shoot()
    {
        if (heldBackTime / Player.stats.drawBackDelay > 0.2f)
        {
            Instantiate(arrow, (transform.position + transform.forward * 0.8f), transform.rotation);
        }
        isShooting = false;
    }

    private void RotatePlayerWithInputVector(Vector2 rotatingVector)
    {
        var angle = Mathf.Atan2(rotatingVector.x, rotatingVector.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(transform.rotation.x, angle, transform.rotation.z);
    }
}
