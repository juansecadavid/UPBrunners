using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerNPC : MonoBehaviour
{
    [SerializeField]
    float speed;
    Vector3 moveDirection;
    Vector3 posFixer;
    [SerializeField]
    bool moveToLeft;
    [SerializeField]
    ResetPosition resetPosition;
    [SerializeField]
    Animator animator;
    [SerializeField]
    Animator[] child;
    [SerializeField]
    GameObject rotationPref;

    // Update is called once per frame
    void FixedUpdate()
    {
        switch(moveToLeft)
        {
            case true:
                if (transform.position.x > -3 && !GameManager.IsPaused)
                {
                    posFixer += moveDirection * speed * Time.deltaTime;
                    posFixer.y = resetPosition.initialPos.y;
                    //posFixer.z = resetPosition.initialPos.z;
                    transform.position = posFixer;
                    //animator.SetBool("Idle", true);
                }
                else if(transform.position.x <= -3 && !GameManager.IsPaused)
                {
                    animator.SetBool("Idle", true);
                    Debug.Log("Entré aqui");
                }
                else
                {
                    animator.SetBool("Idle", false);
                }
                break;
            case false:
                if (transform.position.x < 3 && !GameManager.IsPaused)
                {
                    posFixer += moveDirection * speed * Time.deltaTime;
                    posFixer.y = resetPosition.initialPos.y;
                    //posFixer.z = resetPosition.initialPos.z;
                    transform.position = posFixer;
                }
                else if (transform.position.x >= 3 && !GameManager.IsPaused)
                {
                    animator.SetBool("Idle", true);
                }
                else
                {
                    animator.SetBool("Idle", false);
                }
                    break;
        }  
    }
    private void OnEnable()
    {
        speed = Random.Range(0.4f, 0.7f);
        awakeChild();
        resetPosition.ResetPos();
        if(moveToLeft)
        {
            moveDirection = new Vector3(-3, transform.position.y, 0).normalized;
        }
        else
        {
            moveDirection = new Vector3(3, transform.position.y, 0).normalized;
            for (int i = 0; i < child.Length; i++)
            {
                child[i].transform.rotation=rotationPref.transform.rotation;
            }
        }
        
        posFixer=transform.position;
    }
    private void awakeChild()
    {
        for (int i = 0; i < child.Length; i++)
        {
            child[i].gameObject.SetActive(false);
        }
        child[Random.Range(0, child.Length)].gameObject.SetActive(true);
        animator = GetComponentInChildren<Animator>();
    }
}
