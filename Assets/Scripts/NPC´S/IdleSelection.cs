using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleSelection : MonoBehaviour
{
    [SerializeField] private int selection;
    Animator animator;
    [SerializeField]
    Animator[] child;
    // Start is called before the first frame update
    private void OnEnable()
    {
        for (int i = 0; i < child.Length; i++)
        {
            child[i].gameObject.SetActive(false);
        }
        child[Random.Range(0,child.Length)].gameObject.SetActive(true);
        animator = GetComponentInChildren<Animator>();
        switch (selection)
        {
            case 0:
                animator.SetBool("Thinking", true);
                break;
            case 1:
                animator.SetBool("Happy", true);
                break;
            case 2:
                animator.SetBool("Talking", true);
                break;
            case 3:
                animator.SetBool("TalkingPhone", true);
                break;
            case 4:
                animator.SetBool("SittingTalking", true);
                break;


        }
    }
}
