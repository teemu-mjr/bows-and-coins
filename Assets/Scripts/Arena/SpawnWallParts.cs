using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWallParts : MonoBehaviour
{
    // public fields
    public BoxCollider wallCollider;
    public GameObject objectToSpawn;
    public int partAmount;
    public float spawnGapZ;
    public float spawnGapX;
    public Vector3 itemOffset;
    public Vector3 moveAmount;

    // private fields
    private Vector3 spawnPosition;

    private void Start()
    {
        ArenaController.OnNextWave += OnNextWave;
        spawnPosition = new Vector3((partAmount * spawnGapX) / 2, 0, (partAmount * spawnGapZ) / 2) + itemOffset;
    }

    private void OnNextWave(object sender, ArenaController.WaveArgs e)
    {
        collider.size += new Vector3(spawnGapX, 0, spawnGapZ);
        Spawn();
        partAmount++;
        transform.position = transform.position + moveAmount;
    }

    private void Spawn()
    {
        Instantiate(objectToSpawn, transform.position + spawnPosition, transform.rotation, transform);
        spawnPosition += new Vector3(spawnGapX, 0, spawnGapZ);
    }
}
