using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientManager : MonoBehaviour
{
    private List<GameObject> activeAmbients = new List<GameObject>();
    public GameObject[] ambientPrefabs;

    public float ambientLength = 50;
    public int numberOfAmbients = 1;
    private int ambientsInScene = 1;

    public float zSpawn = 0;

    public Transform playerTransform;

    void Start()
    {
        SpawnAmbient(0);
    }
    void Update()
    {
        if(playerTransform.position.z - 50 >= zSpawn - (numberOfAmbients * ambientLength))
        {
            SpawnAmbient(ambientsInScene);
            ambientsInScene++;
            if(ambientsInScene == ambientPrefabs.Length)
            {
                ambientsInScene = 0;
            }
            DeleteAmbient();
        }   
    }

    public void SpawnAmbient(int ambientIndex)
    {
        GameObject ambient = Instantiate(ambientPrefabs[ambientIndex], -transform.right * zSpawn, ambientPrefabs[ambientIndex].transform.rotation);
        activeAmbients.Add(ambient);
        zSpawn += ambientLength;
    }

    private void DeleteAmbient()
    {
        Destroy(activeAmbients[0]);
        activeAmbients.RemoveAt(0);
    }
}
