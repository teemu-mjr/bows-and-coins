using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFloorTiles : MonoBehaviour
{
    // public fields
    public GameObject objectToSpawn;
    public float tileCountZ;
    public float tileCountX;
    public float spawnGapZ;
    public float spawnGapX;

    // private fields
    public Vector3 spawnPosition;
    private BoxCollider wallCollider;

    // events
    public event EventHandler OnReady;

    private void Start()
    {
        ArenaController.OnNextWave += OnNextWave;
        spawnPosition = new Vector3((tileCountX * spawnGapX) / 2, 0, (tileCountZ * spawnGapZ) / 2);
        wallCollider = GetComponent<BoxCollider>();
    }

    private void OnNextWave(object sender, ArenaController.WaveArgs e)
    {
        StartCoroutine(SpawnTiles());
        wallCollider.size += new Vector3(spawnGapX, 0, spawnGapZ);
        wallCollider.center += new Vector3(spawnGapX / 2, 0, spawnGapZ / 2);
    }

    private IEnumerator SpawnTiles()
    {
        for (int i = 0; i < tileCountZ; i++)
        {
            Instantiate(objectToSpawn, spawnPosition + new Vector3(0, 0, -spawnGapZ * i), transform.rotation, transform);
            yield return new WaitForSeconds(0.05f);
        }
        tileCountZ++;
        for (int j = 1; j < tileCountX; j++)
        {
            Instantiate(objectToSpawn, spawnPosition + new Vector3(-spawnGapX * j, 0, 0), transform.rotation, transform);
            yield return new WaitForSeconds(0.05f);
        }
        tileCountX++;

        spawnPosition += new Vector3(spawnGapX, 0, spawnGapZ);

        OnReady?.Invoke(this, EventArgs.Empty);
    }

    private void OnDisable()
    {
        ArenaController.OnNextWave -= OnNextWave;
    }
}
