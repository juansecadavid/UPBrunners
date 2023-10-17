using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vendedor : MonoBehaviour
{
    private SoundManager soundManager;
    private LevelManager levelManager;
    private MessagesOnPlay ventas;
    private Movement movement;
    [SerializeField]private int cuota;
    [SerializeField]    private float aumento;
    private void Awake()
    {
        soundManager = FindObjectOfType<SoundManager>();
        levelManager = FindObjectOfType<LevelManager>();
        ventas = FindObjectOfType<MessagesOnPlay>();
        movement = FindObjectOfType<Movement>();
        //StartCoroutine(Animate());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            PowerCoins coins = other.GetComponentInParent<PowerCoins>();

            if (coins.Coins <= cuota)
            {
                coins.Coins = 0;
            }
            else
            {
                coins.Coins = coins.Coins - cuota;       
            }

            ventas.ShowMessage("Te han quitado " + cuota + " monedas", 1);
            movement.velocidadMovimiento += aumento;
            soundManager.PlaySound(0);
            levelManager.HideAndShow(gameObject);


        }

    }
}
