using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashRolling : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Movement mov= other.GetComponentInParent<Movement>();   
            if(mov.Rolling==false)
            {
                mov.enabled = false;
                gameManager?.FailedAttemp();
            }
        }
    }
}
