using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject panelFirstLose;
    [SerializeField]
    private GameObject panelDefLose;
    private SkinSelector skinSelector;
    private Animator animator;
    private int losses=0;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private TextMeshProUGUI finalScore;
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
            panelFirstLose.SetActive(true);
            animator.enabled = false;
            losses++;
        }
        else
        {
            panelDefLose.SetActive(true);
            animator.enabled = false;
            Score score=player.GetComponent<Score>();
            finalScore.text = $"Tu puntuación final fué: {score.CurrentNumber}";
        }     
    }
    public void Respawn()
    {
        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 5);
        Movement mov=player.GetComponent<Movement>();
        mov.enabled = true;
        animator.enabled = true;
        panelFirstLose.SetActive(false);
    }
}
