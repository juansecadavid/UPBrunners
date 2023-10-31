using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinSelector : MonoBehaviour
{
    [SerializeField]
    private GameObject[] skins;
    public Animator animator;
    public GameObject[] Skins { get => skins; set => skins = value; }

    // Start is called before the first frame update
    void Awake()
    {
        skins[GameManager.Skin].SetActive(true);
    }


    public void ponerNuevs(int cual)
    {
        skins[GameManager.Skin].SetActive(false);
        GameManager.Skin = cual;
        skins[GameManager.Skin].SetActive(true);
        animator = skins[GameManager.Skin].GetComponent<Animator>();
    }
}
