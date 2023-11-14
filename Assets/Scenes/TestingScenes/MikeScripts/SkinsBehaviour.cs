using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SkinsBehaviour : MonoBehaviour
{
    public int puntuacionRequerida = 100; 
    public Transform[] modelo3D; 
    private SkinSelector skinaUsar;
    public GameObject colliderAct;
    private PowerCoins powerCoinsScript;
    public Canvas canvas;
    private Score scoreScript; 
    private Movement movement;
    private MessagesOnPlay message;
    private LoadGame loadGameScript;

    public bool[] skinsDesbloqueadas;

    private bool botonPresionado = false;

    void Start()
    {
        loadGameScript=FindAnyObjectByType<LoadGame>();
        skinaUsar=FindAnyObjectByType<SkinSelector>();
        movement = FindAnyObjectByType<Movement>();
        colliderAct.SetActive(false);
        modelo3D[GameManager.AvailableSkins].gameObject.SetActive(true);
        scoreScript = GameObject.FindObjectOfType<Score>();
        powerCoinsScript = GameObject.FindObjectOfType<PowerCoins>(); // Encuentra el script de puntuación en la escena
        message=FindObjectOfType<MessagesOnPlay>();
    }

    void Update()
    {
        if (scoreScript != null) // Asegúrate de que el script de puntuación se encontró correctamente
        {
            int puntuacionActual = scoreScript.CurrentNumber; 

            if (puntuacionActual > puntuacionRequerida && !botonPresionado&&GameManager.AvailableSkins<modelo3D.Length)
            {
                modelo3D[GameManager.AvailableSkins].gameObject.SetActive(true);
                colliderAct.SetActive(true);
            }
            else if(GameManager.AvailableSkins<modelo3D.Length)
            {
                modelo3D[GameManager.AvailableSkins].gameObject.SetActive(false);
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
            modelo3D[GameManager.AvailableSkins].gameObject.SetActive(false);
            Time.timeScale = 1.0f;
            canvas.gameObject.SetActive(false);
            GameManager.AvailableSkins++;      
            SaveSystem.SaveAvailableSkin();
            skinaUsar.UnlockSkin(GameManager.AvailableSkins);
            movement.CambiarAnimator(skinaUsar.animator);
            message.ShowMessage("Has desbloqueado una nueva skin!", 2f);
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
        modelo3D[GameManager.AvailableSkins].gameObject.SetActive(false);
    }
}
