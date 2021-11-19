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
    public bool spawnEnemies = true;

    // events
    public static event EventHandler<WaveArgs> OnNextWave;

    //private fields
    private int waveNumber;
    private float cooldownTime;

    // player input actions
    PlayerInputActions playerInputActions;

    // difficulty values
    public static DifficultyMultiplyer enemyHealth;
    public static DifficultyMultiplyer enemyArrowSpeed;
    public static DifficultyMultiplyer enemyShotInterval;
    public static DifficultyMultiplyer huggerSpeed;
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
        cooldownTime = 1.5f;

        enemyHealth = new DifficultyMultiplyer(1, 0.5f, 30);
        enemyArrowSpeed = new DifficultyMultiplyer(5, 0.125f, 10);
        enemyShotInterval = new DifficultyMultiplyer(3, 0.05f, 0.75f, true);
        huggerSpeed = new DifficultyMultiplyer(1, 0.2f, 6);
        dasherSpeed = new DifficultyMultiplyer(75, 5, 250);
        coinDropAmount = new DifficultyMultiplyer(1, 1, 20);
    }

    private void Update()
    {
        if (cooldownTime < 0.5f)
        {
            cooldownTime += Time.deltaTime;
        }
    }

    private void Action_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (cooldownTime >= 0.5f)
        {
            HandleNextWave();
            TransformSpawnArea();
            cooldownTime = 0;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnArea.x, 2, spawnArea.y));
    }

    private void TransformSpawnArea()
    {
        transform.position += new Vector3(.8f, 0, .5f);
        spawnArea += new Vector2(1.6f, 1f);
    }

    private void HandleNextWave()
    {
        waveNumber++;
        OnNextWave(this, new WaveArgs(waveNumber));

        if (spawnEnemies)
        {
            SpawnEnemies(Mathf.RoundToInt(waveNumber));
        }

        // increment all difficulty multiplyers
        enemyHealth.Increment();
        enemyArrowSpeed.Increment();
        enemyShotInterval.Increment();
        dasherSpeed.Increment();
        coinDropAmount.Increment();
        huggerSpeed.Increment();
    }

    private void SpawnEnemies(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Debug.Log(RandomSpawnVector3());
            Instantiate(enemies[Random.Range(0, enemies.Count)], RandomSpawnVector3(), transform.rotation, transform.GetChild(0));
        }
    }

    private Vector3 RandomSpawnVector3()
    {
        Vector3 spawnVector = new Vector3(Random.Range(-spawnArea.x / 2, spawnArea.x / 2), -2, Random.Range(-spawnArea.y / 2, spawnArea.y / 2));
        return transform.position - spawnVector;
    }

    private void OnDestroy()
    {
        if (playerInputActions != null)
        {
            playerInputActions.Player.Action.performed -= Action_performed;
        }
    }
}
