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

    // events
    public static event EventHandler<WaveArgs> OnNextWave;

    //private fields
    private BoxCollider fallCollider;
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
    public static DifficultyMultiplyer coinValue;

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

        fallCollider = GetComponent<BoxCollider>();

        waveNumber = 0;
        cooldownTime = 1.5f;

        enemyHealth = new DifficultyMultiplyer(1, 0.5f, 30);
        enemyArrowSpeed = new DifficultyMultiplyer(5, 0.125f, 10);
        enemyShotInterval = new DifficultyMultiplyer(3, 0.05f, 0.75f, true);
        huggerSpeed = new DifficultyMultiplyer(1, 0.2f, 6);
        dasherSpeed = new DifficultyMultiplyer(75, 5, 250);
        coinDropAmount = new DifficultyMultiplyer(1, 0.5f, 20);
        coinValue = new DifficultyMultiplyer(1, 1, 20);
    }

    private void Update()
    {
        if (cooldownTime < 0.5f)
        {
            cooldownTime += Time.deltaTime;
        }
    }

    private void Action_performed(InputAction.CallbackContext obj)
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
        fallCollider.size = new Vector3(spawnArea.x + 20, 1, spawnArea.y + 20);
    }

    private void HandleNextWave()
    {
        waveNumber++;
        OnNextWave(this, new WaveArgs(waveNumber));

        if (spawnEnemies)
        {
            StartCoroutine(SpawnWithDelay(waveNumber));
        }

        // increment all difficulty multiplyers
        enemyHealth.Increment();
        enemyArrowSpeed.Increment();
        enemyShotInterval.Increment();
        dasherSpeed.Increment();
        huggerSpeed.Increment();
        coinDropAmount.Increment();
        coinValue.Increment();
    }

    private IEnumerator SpawnWithDelay(int amount)
    {
        yield return new WaitForSeconds(0.5f);
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
        if (playerInputActions != null)
        {
            playerInputActions.Player.Action.performed -= Action_performed;
        }
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
