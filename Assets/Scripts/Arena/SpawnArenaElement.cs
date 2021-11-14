using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArenaElement : MonoBehaviour
{
    // public fields
    public GameObject objectToSpawn;
    public int spawnAmount;
    public int rowAmount;
    public Vector3 spawnPosition;
    public Vector3 moveAmount;
    public Vector3 spawnGap;

    private void Start()
    {
        ArenaController.OnNextWave += OnNextWave;
        spawnPosition += transform.position;
    }

    private void OnNextWave(object sender, ArenaController.WaveArgs e)
    {
        Spawn();
    }

    private void Spawn()
    {
        for (int i = 0; i < rowAmount; i++)
        {
            for (int j = 0; j < spawnAmount; j++)
            {
                Instantiate(objectToSpawn, spawnPosition + (spawnGap * j), transform.rotation);
            }
            spawnPosition += moveAmount;
        }
        spawnAmount++;
        Instantiate(objectToSpawn, spawnPosition * spawnAmount - new Vector3(spawnPosition.x, 0, 0), transform.rotation);
    }

    private void OnDisable()
    {
        ArenaController.OnNextWave -= OnNextWave;
    }
}
