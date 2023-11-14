using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class CorrutinaVendedor : MonoBehaviour
{
    private SoundManager soundManager;
    private LevelManager levelManager;
    private MessagesOnPlay ventas;
    private Movement movement;
    private Score score;
    private UpdateSpped updateSpped;
    [SerializeField] private int cuota;
    [SerializeField] private float aumento;
    [SerializeField] private float aumentoScore;
    private float startSpeed;
    private float startScoreMultiplier;

    private void Awake()
    {
        soundManager = FindObjectOfType<SoundManager>();
        levelManager = FindObjectOfType<LevelManager>();
        ventas = FindObjectOfType<MessagesOnPlay>();
        movement = FindObjectOfType<Movement>();
        score = FindObjectOfType<Score>();

        //StartCoroutine(Animate());
    }

    public void StartMetodo(Collider other)
    {

        StartCoroutine(Boost(other));
    }
    public IEnumerator Boost(Collider other)
    {
        other.CompareTag("Player");
        startSpeed = movement.velocidadMovimiento;
        score = other.GetComponentInParent<Score>();
        updateSpped = other.GetComponentInParent<UpdateSpped>();
        score.increment = aumentoScore;
        movement.velocidadMovimiento += aumento;
        ventas.ShowMessage("Puntaje X2", 1);
        Debug.Log("hola si funciono");

        yield return new WaitForSeconds(5f);

        Debug.Log("hola si funciono x2");
        other.CompareTag("Player");
        score = other.GetComponentInParent<Score>();
        updateSpped = other.GetComponentInParent<UpdateSpped>();
        aumentoScore = 1;
        aumento = 0.05f;
        score.increment = aumentoScore;
        movement.velocidadMovimiento = startSpeed;


        yield return null;
    }
}
