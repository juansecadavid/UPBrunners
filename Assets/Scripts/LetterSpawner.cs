using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] letters;
    public GameObject[] Instantiatedletters=new GameObject[3];
    // Start is called before the first frame update
    void Awake()
    {
        Vector3 pos = transform.position;
        Quaternion rot = transform.rotation;
        Instantiatedletters[0] = Instantiate(letters[0], pos, rot,transform);
        Instantiatedletters[1] = Instantiate(letters[1], pos, rot, transform);
        Instantiatedletters[2] = Instantiate(letters[2], pos, rot, transform);

    }
    public void SpawnLetter(int index)
    {
        Instantiatedletters[index].SetActive(true);
        Instantiatedletters[index].transform.position = transform.position;
    }
    private void OnEnable()
    {
        for(int i = 0; i < Instantiatedletters.Length; i++)
        {
            if (i == GameManager.Letras)
            {
                Instantiatedletters[i].SetActive(true);
                Instantiatedletters[i].transform.position = transform.position;
            }
            else
            {
                Instantiatedletters[i].SetActive(false);
                Instantiatedletters[i].transform.position = transform.position;
            }
        }
        
    }
}
