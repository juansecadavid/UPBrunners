using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    private List<GameObject> activeTiles = new List<GameObject>();
    private List<GameObject> respawner = new List<GameObject>();
    public GameObject[] tilePrefabs;

    public float tileLength = 50;
    public int numberOfTiles = 3;

    public float zSpawn = 0;

    public Transform playerTransform;
    private int TileInstatiated = 1;

    void Start()
    {
        for (int i = 0; i < numberOfTiles; i++)
        {
            if(i<2)
                SpawnInitialTiles(0);
            else
                SpawnTile(Random.Range(1, tilePrefabs.Length));
        }
    }
    void Update()
    {
        if(playerTransform.position.z - 50 >= zSpawn - (numberOfTiles * tileLength))
        {
            SpawnTile(Random.Range(1, tilePrefabs.Length));
            DeleteTile();
        }
            
    }

    public void SpawnTile(int tileIndex)
    {
        int indexToUse = SpawnerVerficator(tileIndex);
        GameObject tile = Instantiate(tilePrefabs[indexToUse], -transform.right * zSpawn, tilePrefabs[indexToUse].transform.rotation);
        activeTiles.Add(tile);
        zSpawn += tileLength;

        TileInstatiated = indexToUse;
    }
    public void SpawnInitialTiles(int indexToUse)
    {
        GameObject tile = Instantiate(tilePrefabs[indexToUse], -transform.right * zSpawn, tilePrefabs[indexToUse].transform.rotation);
        activeTiles.Add(tile);
        zSpawn += tileLength;

        TileInstatiated = indexToUse;
    }
    public int SpawnerVerficator(int tileIndex)
    {
        if (tilePrefabs[tileIndex].CompareTag("Spawner")&&respawner.Count==0)
        {
            respawner.Add(tilePrefabs[tileIndex]);
            return tileIndex;
        }
        else if(tilePrefabs[tileIndex].CompareTag("Spawner")&&respawner.Count==1)
        {
            int newIndex= Random.Range(1, tilePrefabs.Length);
            int definitiveIndex=SpawnerVerficator(newIndex);
            return definitiveIndex;
        }
        else if (tilePrefabs[tileIndex] == tilePrefabs[TileInstatiated])
        {
            int newIndex = Random.Range(1, tilePrefabs.Length);
            int definitiveIndex = SpawnerVerficator(newIndex);
            return definitiveIndex;
        }
        return tileIndex;
    }
    public int RepetitionVerificator()
    {
        return 0;
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
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}