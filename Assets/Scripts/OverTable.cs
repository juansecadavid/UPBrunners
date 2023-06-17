using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverTable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Movement mov = other.GetComponentInParent<Movement>();
            mov.IsTouchingTable = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Movement move = other.GetComponentInParent<Movement>();
            move.IsTouchingTable = false;
            move.Saltando = true;
        }
    }
}
