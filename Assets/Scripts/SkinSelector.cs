using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinSelector : MonoBehaviour
{
    [SerializeField]
    private GameObject[] skins;
    public Animator animator;
    public GameObject[] Skins { get => skins; set => skins = value; }

    void Awake()
    {
        skins[GameManager.Skin].SetActive(true);
    }


    public void UnlockSkin(int skin)
    {
        skins[GameManager.Skin].SetActive(false);
        GameManager.Skin = skin;
        skins[GameManager.Skin].SetActive(true);
        animator = skins[GameManager.Skin].GetComponent<Animator>();
    }
}
