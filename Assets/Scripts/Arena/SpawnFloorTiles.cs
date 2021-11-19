using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFloorTiles : MonoBehaviour
{
    // public fields
    public GameObject objectToSpawn;
    public float tileHeight;
    public float tileWidth;
    public float spawnGapZ;
    public float spawnGapX;

    // private fields
    public Vector3 spawnPosition;

    private void Start()
    {
        ArenaController.OnNextWave += OnNextWave;
        spawnPosition = new Vector3((tileWidth * spawnGapX) / 2, 0, (tileHeight * spawnGapZ) / 2);
    }

    private void OnNextWave(object sender, ArenaController.WaveArgs e)
    {
        SpawnTiles();
    }

    private void SpawnTiles()
    {
        for (int i = 0; i < tileHeight; i++)
        {
            Instantiate(objectToSpawn, spawnPosition + new Vector3(0, 0, -spawnGapZ * i), transform.rotation, transform);
        }
        tileHeight++;
        for (int j = 1; j < tileWidth; j++)
        {
            Instantiate(objectToSpawn, spawnPosition + new Vector3(-spawnGapX * j, 0, 0), transform.rotation, transform);
        }
        tileWidth++;

        spawnPosition += new Vector3(spawnGapX, 0, spawnGapZ);
    }

    private void OnDisable()
    {
        ArenaController.OnNextWave -= OnNextWave;
    }
}
