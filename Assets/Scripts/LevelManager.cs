using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject panelLose;
    private SkinSelector skinSelector;
    private Animator animator;
    private int losses=0;
    // Start is called before the first frame update
    void Start()
    {
        skinSelector = FindObjectOfType<SkinSelector>();
        animator = skinSelector.Skins[GameManager.Skin].GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Lose()
    {
        if(losses==0)
        {
            panelLose.SetActive(true);
            animator.enabled = false;
            losses++;
        }
        else
        {

        }
        
    }
}
