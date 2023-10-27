using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopCanvaBehavour : MonoBehaviour
{
    public Canvas canvas; // Arrastra y suelta el objeto Canvas aquí desde el Editor de Unity

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Asegúrate de etiquetar al jugador con "Jugador"
        {
            Time.timeScale = 0.1f; // Ralentizar el tiempo
            canvas.gameObject.SetActive(true); // Mostrar el Canvas
        }
    }
}
