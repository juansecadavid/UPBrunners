using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TilePooling : MonoBehaviour
{
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
    [SerializeField] public int averlajoda = 0;

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
            averlajoda++;
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
            if(i<30)
            {
                // Usa el array inicial para los primeros 20 tiles
                tileChain[i] = GetValidTileIndexFromInitial(i);
                //tileChain[i] = GetValidTileIndex(i);
            }
            else
            {
                tileChain[i] = GetValidTileIndex(i);
            }
            
        }
    }

    int GetValidTileIndex(int currentChainIndex)
    {
        int tileIndex;
        do
        {
            tileIndex = Random.Range(12, tilePrefabs.Length); // Evitar 0 y 1 después de la inicialización
        } while (IsTileInLastEight(tileIndex, currentChainIndex) || (IsTileSpawner(tileIndex) && HasSpawnerInLastTiles()));

        return tileIndex;
    }

    int GetValidTileIndexFromInitial(int currentChainIndex)
    {
        int tileIndex;
        do
        {
            tileIndex = Random.Range(2, 12); // Evitar 0 y 1 después de la inicialización
        } while (IsTileInLastEight(tileIndex, currentChainIndex) || (IsTileSpawner(tileIndex) && HasSpawnerInLastTiles()));

        return tileIndex;
    }

    bool IsTileInLastEight(int tileIndex, int currentChainIndex)
    {
        int startCheck = Mathf.Max(currentChainIndex - 8, 0);
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