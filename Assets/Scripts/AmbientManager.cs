using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientManager : MonoBehaviour
{
    private List<GameObject> activeAmbients = new List<GameObject>();
    public GameObject[] ambientPrefabs;
    GameObject[] pooledAmbients;

    public float ambientLength = 100;
    public int numberOfAmbients = 1;
    private int ambientsInScene = 3;

    public float zSpawn = 0;

    public Transform playerTransform;

    private void Awake()
    {
        pooledAmbients = new GameObject[ambientPrefabs.Length];
        for (int i = 0; i < ambientPrefabs.Length; i++)
        {
            GameObject ambient = Instantiate(ambientPrefabs[i], new Vector3(0, 0, 0), ambientPrefabs[i].transform.rotation);
            ambient.SetActive(false);
            pooledAmbients[i] = ambient;

        }
    }
    void Start()
    {
        
        SpawnAmbient(0);
        SpawnAmbient(1);
        SpawnAmbient(2);
    }
    void Update()
    {
        if(playerTransform.position.z - 50 >= zSpawn - (numberOfAmbients * ambientLength))
        {
            SpawnAmbient(ambientsInScene);
            ambientsInScene++;
            if(ambientsInScene == ambientPrefabs.Length)
            {
                ambientsInScene = 1;
            }
            DeleteAmbient();
        }   
    }

    public void SpawnAmbient(int ambientIndex)
    {/*
        GameObject ambient = Instantiate(ambientPrefabs[ambientIndex], -transform.right * zSpawn, ambientPrefabs[ambientIndex].transform.rotation);
        activeAmbients.Add(ambient);
        zSpawn += ambientLength;*/
        GameObject ambient = pooledAmbients[ambientIndex];

        ambient.SetActive(true);
        ambient.transform.position = -transform.right * zSpawn;

        activeAmbients.Add(ambient);

        zSpawn += ambientLength;
    }

    private void DeleteAmbient()
    {
        activeAmbients[0].SetActive(false);
        activeAmbients.RemoveAt(0);
    }
}
