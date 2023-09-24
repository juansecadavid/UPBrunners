using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePooling : MonoBehaviour
{
    private List<GameObject> activeTiles = new List<GameObject>();

    public GameObject[] tilePrefabs;

    private GameObject[] newtilePrefabs;

    public int[] tileChain=new int[100];

    public float tileLength = 50;

    public int numberOfTiles = 3;

    private int dondePoner = 0;

    public float zSpawn = 0;

    public int tileToActive = 1;

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
        for (int i = 0; i < 100; i++)
        {
            SpawnTile(Random.Range(2, tilePrefabs.Length),i);
        }
        for (int i = 0; i < numberOfTiles; i++)
        {

            if (i < 1)

                SpawnInitialTiles(0);

            else if (i == 1)

                SpawnInitialTiles(1);

            else
            {
                SpawnActive(tileChain[tileToActive]);
                tileToActive++;
            }
        }
    }

    void Update()
    {
        if (playerTransform.position.z - 50 >= zSpawn - (numberOfTiles * tileLength))
        {
            DeleteTile();
            SpawnActive(tileChain[tileToActive]);
            tileToActive++;
        }

    }

    public void SpawnTile(int tileIndex, int index)
    {

        int indexToUse = SpawnerVerficator(tileIndex,index);

        tileChain[index] = indexToUse;

    }

    public void SpawnInitialTiles(int indexToUse)
    {

        GameObject tile = newtilePrefabs[indexToUse];

        tile.transform.position = -transform.right * zSpawn;

        tile.SetActive(true);

        activeTiles.Add(tile);

        zSpawn += tileLength;

    }

    public int SpawnerVerficator(int tileIndex, int actualChainIndex)

    {

        if (newtilePrefabs[tileIndex].CompareTag("Spawner") && !ActiveSpawnerLastFive(tileIndex, actualChainIndex))
        {
            return tileIndex;
        }

        else if (newtilePrefabs[tileIndex].CompareTag("Spawner") && ActiveSpawnerLastFive(tileIndex,actualChainIndex))

        {
            int newIndex = Random.Range(2, tilePrefabs.Length);

            int definitiveIndex = SpawnerVerficator(newIndex,actualChainIndex);

            return definitiveIndex;

        }

        else if (ActiveLastFive(tileIndex,actualChainIndex))
        {

            int newIndex = Random.Range(2, tilePrefabs.Length);

            int definitiveIndex = SpawnerVerficator(newIndex,actualChainIndex);

            return definitiveIndex;

        }

        return tileIndex;

    }

    private void DeleteTile()
    {
        if(tileToActive>6)
        {
            newtilePrefabs[tileChain[tileToActive - 6]].SetActive(false);
        }  
        else if(tileToActive==5)
        {
            newtilePrefabs[0].SetActive(false);
            newtilePrefabs[1].SetActive(false);
        }

    }
    bool ActiveLastFive(int indexToUse,int actualChainIndex)
    {
        if(actualChainIndex>6)
        {
            for (int i = 1; i < 7; i++)
            {
                if (tileChain[actualChainIndex - i] == indexToUse)
                {
                    return true;
                }
            }
        }    
        else
        {
            for (int i = 0; i < actualChainIndex; i++)
            {
                if (tileChain[actualChainIndex - (i+1)] == indexToUse)
                {
                    return true;
                }
            }
        }
        return false;
    }
    bool ActiveSpawnerLastFive(int indexToUse, int actualChainIndex)
    {
        if(actualChainIndex>4)
        {
            for (int i = 1; i < 6; i++)
            {
                int tile = tileChain[actualChainIndex - i];
                if (newtilePrefabs[tile].CompareTag("Spawner"))
                {
                    return true;
                }
            }
        }    
        return false;
    }
    void SpawnActive(int tileToActive)
    {

        GameObject tile = newtilePrefabs[tileToActive];

        tile.transform.position = -transform.right * zSpawn;

        tile.SetActive(true);

        zSpawn += tileLength;
    }
}