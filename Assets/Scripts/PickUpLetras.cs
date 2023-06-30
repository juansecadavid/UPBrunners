using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpLetras : MonoBehaviour
{
    [SerializeField]
    private int letra;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GameManager.Letras = letra;
            Destroy(gameObject);           
        }     
    }
    private void OnDestroy()
    {
        GameManager.ActiveLetter1.RemoveAt(0);
    }
}
