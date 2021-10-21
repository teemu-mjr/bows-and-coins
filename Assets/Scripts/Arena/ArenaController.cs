using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaController : MonoBehaviour
{
    private static float difficultyMultiplyer;
    public List<GameObject> enemies;
    public Vector2 spawnArea;

    public delegate void NextWave();
    public static event NextWave OnNextWave;

    private int waveNumber;

    PlayerInputActions playerInputActions;

    public static float DifficultyMultiplyer
    {
        get
        {
            return difficultyMultiplyer;
        }
    }
    void Start()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Action.performed += Action_performed;

        waveNumber = 0;
        difficultyMultiplyer = 1;
    }

    private void Action_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        HandleNextWave();
        GrowSpawnArea();
        OnNextWave();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnArea.x, 2, spawnArea.y));
    }

    private void GrowSpawnArea()
    {
        spawnArea += new Vector2(.8f, .45f);
    }

    private void HandleNextWave()
    {
        waveNumber++;
        difficultyMultiplyer += 0.125f;
        SpawnEnemies(Mathf.RoundToInt(1 + difficultyMultiplyer));
    }

    private void SpawnEnemies(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Vector2 spawnPoint = RandomVector2();
            Instantiate(enemies[Random.Range(0, enemies.Count)], new Vector3(spawnPoint.x, 2, spawnPoint.y), transform.rotation);
        }
    }

    private Vector2 RandomVector2()
    {
        return (new Vector2(Random.Range(-spawnArea.x / 2, spawnArea.x / 2), Random.Range(-spawnArea.y / 2, spawnArea.y / 2)));
    }

    private void OnDestroy()
    {
        if (playerInputActions != null)
        {
            playerInputActions.Player.Action.performed -= Action_performed;
        }
    }
}
