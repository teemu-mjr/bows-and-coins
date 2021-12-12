using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using Random = UnityEngine.Random;

public class ArenaController : MonoBehaviour
{
    // public fields
    public List<GameObject> enemies;
    public Vector2 spawnArea;
    public TextMeshProUGUI waveText;
    public bool spawnEnemies = true;

    //private fields
    private BoxCollider fallCollider;
    private int waveNumber;
    private bool canSpawn;

    // events
    public static event EventHandler<WaveEventArgs> OnNextWave;

    // player input actions
    PlayerInputActions playerInputActions;

    // difficulty values
    public static DifficultyMultiplyer enemyHealth;
    public static DifficultyMultiplyer enemyArrowSpeed;
    public static DifficultyMultiplyer enemyShotInterval;
    public static DifficultyMultiplyer enemyArrowFightTime;
    public static DifficultyMultiplyer huggerSpeed;
    public static DifficultyMultiplyer dasherSpeed;
    public static DifficultyMultiplyer boucerSpeed;
    public static DifficultyMultiplyer coinDropAmount;
    public static DifficultyMultiplyer coinValue;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Action.performed += Action_performed;
        SpawnFloorTiles.OnReady += SpawnFloorTiles_OnReady;
        fallCollider = GetComponent<BoxCollider>();

        waveNumber = 0;
        canSpawn = true;

        enemyHealth = new DifficultyMultiplyer(1, 2f, 100);
        enemyArrowSpeed = new DifficultyMultiplyer(5, 0.125f, 10);
        enemyShotInterval = new DifficultyMultiplyer(3, 0.05f, 0.75f, true);
        enemyArrowFightTime = new DifficultyMultiplyer(1, 0.25f, 3f, true);
        huggerSpeed = new DifficultyMultiplyer(1, 0.2f, 6);
        dasherSpeed = new DifficultyMultiplyer(75, 5, 250);
        boucerSpeed = new DifficultyMultiplyer(0.5f, 0.25f, 5);
        coinDropAmount = new DifficultyMultiplyer(1, 0.5f, 20);

        if (OptionsMenu.DoubleCoins)
        {
            coinValue = new DifficultyMultiplyer(2, 4, 20);
        }
        else
        {
            coinValue = new DifficultyMultiplyer(1, 1, 40);
        }

        PlayerHealth.OnPlayerDeath += PlayerHealth_OnPlayerDeath;
    }

    private void Start()
    {
        GameObject.Find("Music").GetComponent<MusicController>().PlaySong(0);
    }

    private void PlayerHealth_OnPlayerDeath(object sender, EventArgs e)
    {
        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            if (transform.GetChild(0).GetChild(i).GetComponent<EnemyHealth>())
            {
                transform.GetChild(0).GetChild(i).GetComponent<EnemyHealth>().Die();
            }
        }
    }

    private void Action_performed(InputAction.CallbackContext obj)
    {
        if (canSpawn)
        {
            HandleNextWave();
            canSpawn = false;
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
        fallCollider.size = new Vector3(spawnArea.x + 40, 3, spawnArea.y + 40);
    }

    private void HandleNextWave()
    {
        waveNumber++;
        OnNextWave?.Invoke(this, new WaveEventArgs() { waveNumber = this.waveNumber });

        // increment all difficulty multiplyers
        enemyHealth.Increment();
        enemyArrowSpeed.Increment();
        enemyShotInterval.Increment();
        enemyArrowFightTime.Increment();
        dasherSpeed.Increment();
        huggerSpeed.Increment();
        boucerSpeed.Increment();
        coinDropAmount.Increment();
        coinValue.Increment();
    }
    private void SpawnFloorTiles_OnReady(object sender, EventArgs e)
    {
        if (spawnEnemies)
        {
            SpawnWithDelay(waveNumber);
        }
        TransformSpawnArea();
        canSpawn = true;
    }

    private void SpawnWithDelay(int amount)
    {
        SpawnEnemies(amount);
    }

    private void SpawnEnemies(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(enemies[Random.Range(0, enemies.Count)], RandomSpawnVector3(), transform.rotation, transform.GetChild(0));
        }
    }

    private Vector3 RandomSpawnVector3()
    {
        Vector3 spawnVector = new Vector3(Random.Range(-spawnArea.x / 2, spawnArea.x / 2), -2, Random.Range(-spawnArea.y / 2, spawnArea.y / 2));
        return transform.position - spawnVector;
    }

    private void OnDisable()
    {
        playerInputActions.Player.Disable();
        playerInputActions.Player.Action.performed -= Action_performed;
        SpawnFloorTiles.OnReady -= SpawnFloorTiles_OnReady;
        PlayerHealth.OnPlayerDeath -= PlayerHealth_OnPlayerDeath;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().Die();
        }
        Destroy(other.gameObject);
    }
}
