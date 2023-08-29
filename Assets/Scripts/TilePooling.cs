using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TilePooling : MonoBehaviour
{
    private List<GameObject> tilePool;

    private Queue<GameObject> availableTiles;
    [SerializeField]
    private GameObject[] tilePrefabs;
    private List<GameObject> activeTiles = new List<GameObject>();
    [SerializeField]
    private Transform playerTransform;
    private float tileLength = 50;
    private float lastSpawnZ = 0;

    // Other fields

    void Start()
    {
        // Initialize tilePool with a certain number of prefabs
        tilePool = new List<GameObject>();
        for (int i = 0; i < 1; i++)
        {
            for (int j = 0; j < tilePrefabs.Length; j++)
            {
                GameObject tile = Instantiate(tilePrefabs[j]);
                tile.SetActive(false);
                tilePool.Add(tile);
            }
        }

        availableTiles = new Queue<GameObject>();

        foreach (GameObject tile in tilePool)
        {
            availableTiles.Enqueue(tile);
        }

        ShuffleQueue(availableTiles);
        tilePrefabs[0].SetActive(true);
        SpawnTile(tilePrefabs[0]);
        tilePrefabs[1].SetActive(true);
        SpawnTile(tilePrefabs[1]);
        tilePrefabs[2].SetActive(true);
        SpawnTile(tilePrefabs[2]);
    }
    private void Update()
    {
        if (playerTransform.position.z - 50 >= lastSpawnZ-250)
        {

            GameObject newTile = GetTileFromPool();

            SpawnTile(newTile);
        }
    }
    public void SpawnTile(GameObject newTile)
    {

        newTile.transform.position = -transform.right*lastSpawnZ;

        lastSpawnZ += tileLength;

        activeTiles.Add(newTile);

        DeleteTile();
    }
    void ShuffleQueue(Queue<GameObject> queue)
    {

        int n = queue.Count;

        while (n > 1)
        {

            n--;

            int k = Random.Range(0, n + 1);

            GameObject tile = queue.Dequeue();

            queue.Enqueue(tile);

            GameObject value = queue.Dequeue();

            queue.Enqueue(value);

        }

    }
    GameObject GetTileFromPool()
    {
        if (availableTiles.Count == 0)
        {
            // Pool needs to be increased
            // Instantiate more tiles and enqueue them
        }

        GameObject spawnedTile = availableTiles.Dequeue();
        spawnedTile.SetActive(true);
        return spawnedTile;
    }

    private void DeleteTile()
    {
        GameObject destroyedTile = activeTiles[0];
        destroyedTile.SetActive(false);
        activeTiles.RemoveAt(0);

        availableTiles.Enqueue(destroyedTile);
    }
}
