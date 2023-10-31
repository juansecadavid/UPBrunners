using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinsBehaviour : MonoBehaviour
{
  public int puntuacionRequerida = 100; 
    public GameObject modelo3D; 

    private GameObject Skin0;
    private SkinSelector skinaUsar;
    private GameObject Skin1;
    public GameObject colliderAct;
    private PowerCoins powerCoinsScript;
    public Canvas canvas;
    private Score scoreScript; 
    private Movement movement;

    private bool botonPresionado = false;

    void Start()
    {
        skinaUsar=FindAnyObjectByType<SkinSelector>();
        movement = FindAnyObjectByType<Movement>();
        if(skinaUsar != null )
        {
            Debug.Log("Loencontré");
        }
        colliderAct.SetActive(false);
        modelo3D.SetActive(false);
        scoreScript = GameObject.FindObjectOfType<Score>();
        powerCoinsScript = GameObject.FindObjectOfType<PowerCoins>(); // Encuentra el script de puntuación en la escena
        Skin0 = GameObject.FindGameObjectWithTag("Skin0");
        Skin1 = GameObject.FindGameObjectWithTag("Skin1");

        if(Skin0 == null)
        {
         Debug.LogError("No se encontraron los objetos Skin0 y/o Skin1 en la escena.");
        }

    }

    void Update()
    {
        if (scoreScript != null) // Asegúrate de que el script de puntuación se encontró correctamente
        {
            int puntuacionActual = scoreScript.CurrentNumber; 

            if (puntuacionActual > puntuacionRequerida && !botonPresionado)
            {
                modelo3D.gameObject.SetActive(true);
                colliderAct.SetActive(true);
            }
            else
            {
                modelo3D.gameObject.SetActive(false);
                colliderAct.SetActive(false);
            }
        }
    }

    public void AdquirirSkin()
    {
        if (powerCoinsScript.Coins >= 1)
        {
            botonPresionado = true;
            powerCoinsScript.Coins -= 1;
            modelo3D.gameObject.SetActive(false);
            Time.timeScale = 1.0f;
            canvas.gameObject.SetActive(false);
            Skin0.gameObject.SetActive(false);
            //Skin1.gameObject.SetActive(true);
            skinaUsar.ponerNuevs(1);
            movement.CambiarAnimator(skinaUsar.animator);
        }
        else
        {
            // El jugador no tiene suficientes monedas para adquirir la skin
            // Puedes agregar un mensaje o alguna otra lógica aquí si deseas.
        }
    }

    public void SeguirSinAdquirir()
    {
        botonPresionado = true;
        Time.timeScale = 1.0f;
        canvas.gameObject.SetActive(false);
        modelo3D.gameObject.SetActive(false);
    }
}
