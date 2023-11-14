using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeloDesaparece : MonoBehaviour
{
     void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false); // Desactivar el modelo 3D al colisionar con el jugador
        }
    }
}
