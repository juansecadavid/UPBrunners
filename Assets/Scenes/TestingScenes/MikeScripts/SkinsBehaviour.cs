using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinsBehaviour : MonoBehaviour
{
  public int puntuacionRequerida = 1500; 
    public GameObject modelo3D; 
    private Score scoreScript; // Esto es nuevo

    void Start()
    {
        modelo3D.SetActive(false);
        scoreScript = GameObject.FindObjectOfType<Score>(); // Encuentra el script de puntuación en la escena
    }

    void Update()
    {
        if (scoreScript != null) // Asegúrate de que el script de puntuación se encontró correctamente
        {
            int puntuacionActual = scoreScript.CurrentNumber; 

            if (puntuacionActual > puntuacionRequerida)
            {
                modelo3D.SetActive(true);
            }
            else
            {
                modelo3D.SetActive(false);
            }
        }
    }
}
