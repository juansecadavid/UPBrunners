using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.AdaptivePerformance.VisualScripting;

public class Vendedores : MonoBehaviour
{
    private SoundManager soundManager;
    private LevelManager levelManager;
    private MessagesOnPlay ventas;

    private CorrutinaVendedor corrutinaVendedor;
    [SerializeField] private int cuota;



    private void Awake()
    {
        soundManager = FindObjectOfType<SoundManager>();
        levelManager = FindObjectOfType<LevelManager>();
        ventas = FindObjectOfType<MessagesOnPlay>();
        corrutinaVendedor = FindAnyObjectByType<CorrutinaVendedor>();

        //StartCoroutine(Animate());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            PowerCoins coins = other.GetComponentInParent<PowerCoins>();

            if (coins.Coins < cuota)
            {

                ventas.ShowMessage("No tienes monedas suficientes", 1);

            }
            else
            {
                if (coins.Coins == cuota)
                {
                    coins.Coins = 0;

                }
                else
                {
                    coins.Coins = coins.Coins - cuota;

                }

                corrutinaVendedor.StartMetodo(other);

                ventas.ShowMessage("Te han quitado " + cuota + " monedas", 1);
            }




            soundManager.PlaySound(0);
            levelManager.HideAndShow(gameObject);


        }



    }

}
