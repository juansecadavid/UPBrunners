//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{/*
    private List<GameObject> activeTiles = new List<GameObject>();

    private List<GameObject> respawner = new List<GameObject>();

    public GameObject[] tilePrefabs;

    private GameObject[] newtilePrefabs;

    public float tileLength = 50;

    public int numberOfTiles = 3;

    private int dondePoner = 0;

    public float zSpawn = 0;

    public Transform playerTransform;
    
    private void Awake()
    {
        newtilePrefabs = new GameObject[tilePrefabs.Length];

        foreach (var tile in tilePrefabs)

        {

            GameObject tilePrefab = Instantiate(tile, new Vector3(0, 0, 0), tile.transform.rotation);

            newtilePrefabs[dondePoner] = tilePrefab;

            tilePrefab.SetActive(false);

            dondePoner++;

        }
        for (int i = 0; i < numberOfTiles; i++)

        {

            if (i < 1)

                SpawnInitialTiles(0);

            else if (i == 1)

                SpawnInitialTiles(16);

            else

                SpawnTile(Random.Range(1, tilePrefabs.Length));

        }
    }

    void Update()

    {

        if (playerTransform.position.z - 50 >= zSpawn - (numberOfTiles * tileLength))

        {

            SpawnTile(Random.Range(1, tilePrefabs.Length));

            DeleteTile();

        }

    }

    public void SpawnTile(int tileIndex)

    {

        int indexToUse = SpawnerVerficator(tileIndex);

        //GameObject tile = Instantiate(tilePrefabs[indexToUse], -transform.right * zSpawn, tilePrefabs[indexToUse].transform.rotation);

        GameObject tile = newtilePrefabs[indexToUse];

        tile.transform.position = -transform.right * zSpawn;

        tile.SetActive(true);

        activeTiles.Add(tile);

        zSpawn += tileLength;

    }

    public void SpawnInitialTiles(int indexToUse)

    {

        //GameObject tile = Instantiate(tilePrefabs[indexToUse], -transform.right * zSpawn, tilePrefabs[indexToUse].transform.rotation);

        GameObject tile = newtilePrefabs[indexToUse];

        tile.transform.position = -transform.right * zSpawn;

        tile.SetActive(true);

        activeTiles.Add(tile);

        zSpawn += tileLength;

    }

    public int SpawnerVerficator(int tileIndex)

    {

        if (newtilePrefabs[tileIndex].CompareTag("Spawner") && respawner.Count == 0)

        {

            respawner.Add(newtilePrefabs[tileIndex]);

            return tileIndex;

        }

        else if (newtilePrefabs[tileIndex].CompareTag("Spawner") && respawner.Count == 1)

        {

            int newIndex = Random.Range(1, tilePrefabs.Length);

            int definitiveIndex = SpawnerVerficator(newIndex);

            return definitiveIndex;

        }

        else if (newtilePrefabs[tileIndex].activeInHierarchy)

        {

            int newIndex = Random.Range(1, tilePrefabs.Length);

            int definitiveIndex = SpawnerVerficator(newIndex);

            return definitiveIndex;

        }

        return tileIndex;

    }

    private void DeleteTile()

    {

        if (activeTiles[0].CompareTag("Spawner"))
        {

            if (respawner.Count > 0)

            {

                respawner.RemoveAt(0);

            }

        }

        activeTiles[0].SetActive(false);

        //Destroy(activeTiles[0]);

        activeTiles.RemoveAt(0);

    }*/
}