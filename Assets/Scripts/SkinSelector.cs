using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinSelector : MonoBehaviour
{
    [SerializeField]
    private GameObject[] skins;

    public GameObject[] Skins { get => skins; set => skins = value; }

    // Start is called before the first frame update
    void Awake()
    {
        skins[GameManager.Skin].SetActive(true);
    }
}
