using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TilePooling : MonoBehaviour
{/*
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
    }*/

    [SerializeField] private List<GameObject> activeTiles = new List<GameObject>();
    [SerializeField] private GameObject[] tilePrefabs;
    private GameObject[] newtilePrefabs;
    [SerializeField] private int[] tileChain = new int[200];
    [SerializeField] private float tileLength = 50;
    [SerializeField] private int numberOfTiles = 6;
    [SerializeField] private int tileToActive = 0;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float zSpawn = 0;
    private Queue<GameObject> tilesToBeActivated = new Queue<GameObject>();

    // Variable pública para rastrear desde el editor de Unity
    [SerializeField] public int currentTileIndexForDebug;

    private void Awake()
    {
        InitializeTilePrefabs();
        GenerateTileChain();
        SpawnInitialTiles();
    }

    void Update()
    {
        currentTileIndexForDebug = Mathf.FloorToInt((playerTransform.position.z - zSpawn) / tileLength);
        if (playerTransform.position.z - tileLength >= zSpawn - (numberOfTiles * tileLength))
        {
            RefreshTiles();
        }
    }

    void InitializeTilePrefabs()
    {
        newtilePrefabs = new GameObject[tilePrefabs.Length];
        for (int i = 0; i < tilePrefabs.Length; i++)
        {
            GameObject tilePrefab = Instantiate(tilePrefabs[i], new Vector3(0, 0, 0), tilePrefabs[i].transform.rotation);
            tilePrefab.SetActive(false);
            newtilePrefabs[i] = tilePrefab;
            tilesToBeActivated.Enqueue(tilePrefab); // Añadir al final de la cola
        }
    }

    void GenerateTileChain()
    {
        tileChain[0] = 0;
        tileChain[1] = 1;
        for (int i = 2; i < tileChain.Length; i++)
        {
            tileChain[i] = GetValidTileIndex(i);
        }
    }

    int GetValidTileIndex(int currentChainIndex)
    {
        int tileIndex;
        do
        {
            tileIndex = Random.Range(2, tilePrefabs.Length); // Evitar 0 y 1 después de la inicialización
        } while (IsTileInLastSix(tileIndex, currentChainIndex) || (IsTileSpawner(tileIndex) && HasSpawnerInLastTiles()));

        return tileIndex;
    }

    bool IsTileInLastSix(int tileIndex, int currentChainIndex)
    {
        int startCheck = Mathf.Max(currentChainIndex - 5, 0);
        for (int i = startCheck; i < currentChainIndex; i++)
        {
            if (tileChain[i] == tileIndex)
            {
                return true;
            }
        }
        return false;
    }

    bool IsTileSpawner(int tileIndex)
    {
        return newtilePrefabs[tileIndex].CompareTag("Spawner");
    }

    // Verifica si hay algún 'Spawner' en los últimos tiles activos
    bool HasSpawnerInLastTiles()
    {
        // Iniciar la comprobación desde el último tile activo hacia atrás, pero no contar el tile recién activado
        int startIndex = activeTiles.Count - 1; // El último tile activado es el actual donde se encuentra el jugador
        int endIndex = Mathf.Max(activeTiles.Count - 6, 0); // Asegurarse de no revisar más allá del rango necesario

        for (int i = startIndex; i >= endIndex; i--)
        {
            if (activeTiles[i].CompareTag("Spawner"))
            {
                return true;
            }
        }
        return false;
    }

    void SpawnInitialTiles()
    {
        for (int i = 0; i < numberOfTiles; i++)
        {
            SpawnTile(tileChain[i]);
            tileToActive++;
        }
    }

    void RefreshTiles()
    {
        // Desactivar el tile más antiguo si hay suficientes tiles activos
        if (activeTiles.Count >= numberOfTiles)
        {
            GameObject oldTile = activeTiles[0];
            oldTile.SetActive(false);
            activeTiles.RemoveAt(0);
            tilesToBeActivated.Enqueue(oldTile); // Ponerlo de vuelta en la cola para ser reutilizado
        }

        // Activar el siguiente tile en la cadena
        while (tileToActive < tileChain.Length)
        {
            int nextTileIndex = tileChain[tileToActive];
            if (!IsTileSpawner(nextTileIndex) || !HasSpawnerInLastTiles())
            {
                GameObject newTile = newtilePrefabs[nextTileIndex];
                newTile.transform.position = new Vector3(0, 0, zSpawn);
                newTile.SetActive(true);
                activeTiles.Add(newTile);
                zSpawn += tileLength;
                tileToActive++; // Incrementar índice para el próximo tile
                break; // Salir después de activar un tile
            }
            else
            {
                tileToActive++; // Saltar este tile y probar con el siguiente
            }
        }
    }
    void SpawnTile(int tileIndex)
    {
        GameObject tile = newtilePrefabs[tileIndex];
        tile.transform.position = new Vector3(0, 0, zSpawn);
        tile.SetActive(true);
        activeTiles.Add(tile);
        zSpawn += tileLength;
    }
}