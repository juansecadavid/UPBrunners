using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crashing : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Movement mov= other.GetComponentInParent<Movement>();
            mov.enabled=false;
            gameManager?.FailedAttemp();
        }
    }
}
