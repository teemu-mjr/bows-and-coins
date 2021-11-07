using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

public class ArenaController : MonoBehaviour
{
    // public fields
    public List<GameObject> enemies;
    public Vector2 spawnArea;
    public TextMeshProUGUI waveText;

    // events
    public static event EventHandler<WaveArgs> OnNextWave;

    //private fields
    private int waveNumber;

    // player input actions
    PlayerInputActions playerInputActions;

    // difficulty values
    public static DifficultyMultiplyer enemyHealth;
    public static DifficultyMultiplyer enemyArrowSpeed;
    public static DifficultyMultiplyer enemyShotInterval;
    public static DifficultyMultiplyer dasherSpeed;
    public static DifficultyMultiplyer coinDropAmount;

    // WaveArgs
    public class WaveArgs : EventArgs
    {
        public int waveNumber;

        public WaveArgs(int waveNumber)
        {
            this.waveNumber = waveNumber;
        }
    }

    void Start()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Action.performed += Action_performed;

        waveNumber = 0;
                
        enemyHealth = new DifficultyMultiplyer(1, 0.5f, 30);
        enemyArrowSpeed = new DifficultyMultiplyer(5, 0.125f, 10);
        enemyShotInterval = new DifficultyMultiplyer(3, 0.05f, 0.75f, true);
        dasherSpeed = new DifficultyMultiplyer(75, 5, 250);
        coinDropAmount = new DifficultyMultiplyer(1, 1, 20);
    }

    private void Action_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        HandleNextWave();
        GrowSpawnArea();
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
        OnNextWave(this, new WaveArgs(waveNumber));

        SpawnEnemies(Mathf.RoundToInt(waveNumber));       

        // increment all difficulty multiplyers
        enemyHealth.Increment();
        enemyArrowSpeed.Increment();
        enemyShotInterval.Increment();
        dasherSpeed.Increment();
        coinDropAmount.Increment();
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
        return new Vector2(Random.Range(-spawnArea.x / 2, spawnArea.x / 2), Random.Range(-spawnArea.y / 2, spawnArea.y / 2));
    }

    private void OnDestroy()
    {
        if (playerInputActions != null)
        {
            playerInputActions.Player.Action.performed -= Action_performed;
        }
    }
}
