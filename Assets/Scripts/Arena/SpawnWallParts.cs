using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWallParts : MonoBehaviour
{
    // public fields
    public List<GameObject> objectsToSpawn;
    public int partAmount;
    public float spawnGapZ;
    public float spawnGapX;
    public Vector3 itemOffset;
    public Vector3 moveAmount;
    public bool invertColliderAxis;
    public SpawnFloorTiles spawnFloorTiles;

    // private fields
    private Vector3 spawnPosition;
    private BoxCollider wallCollider;
    private Vector3 targetVector;
    private int spawnObjectIndex;

    private void Start()
    {
        spawnPosition = new Vector3((partAmount * spawnGapX) / 2, 0, (partAmount * spawnGapZ) / 2) + itemOffset;
        wallCollider = GetComponent<BoxCollider>();
    }

    private void Awake()
    {
        ArenaController.OnNextWave += OnNextWave;
    }

    private void OnNextWave(object sender, WaveEventArgs e)
    {
        targetVector = transform.position + moveAmount;
        wallCollider.size += new Vector3((spawnGapX + spawnGapZ), 0, 0);
        if (!invertColliderAxis)
        {
            wallCollider.center += new Vector3((spawnGapX + spawnGapZ) / 2, 0, 0);
        }
        else
        {
            wallCollider.center += new Vector3(-(spawnGapX + spawnGapZ) / 2, 0, 0);
        }
        Spawn();
        partAmount++;
        StartCoroutine(MoveWall());
    }

    private IEnumerator MoveWall()
    {
        float timeElapsed = 0;

        while (transform.position != targetVector)
        {
            transform.position = Vector3.Lerp(transform.position, targetVector, timeElapsed / 3);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }

    private void Spawn()
    {
        Instantiate(objectsToSpawn[spawnObjectIndex], transform.position + spawnPosition, transform.rotation, transform);
        spawnPosition += new Vector3(spawnGapX, 0, spawnGapZ);

        if(spawnObjectIndex < objectsToSpawn.Count - 1)
        {
            spawnObjectIndex++;
        }
        else
        {
            spawnObjectIndex = 0;
        }
    }

    private void OnDisable()
    {
        ArenaController.OnNextWave -= OnNextWave;
    }
}
