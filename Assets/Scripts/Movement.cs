using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float velocidadMovimiento = 5f;  // Velocidad de movimiento del jugador
    public float velocidadMovimientoLateral = 5f;  // Velocidad de movimiento del jugador
    public float distanciaMovimientoLateral = 3f;  // Distancia a moverse lateralmente al presionar una flecha

    private bool moverDerecha = false;
    private bool moverIzquierda = false;
    private float destinoX;

    void Update()
    {
        // Verificar si se debe mover lateralmente
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            moverDerecha = true;
            moverIzquierda = false;
            destinoX = transform.position.x + distanciaMovimientoLateral;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            moverDerecha = false;
            moverIzquierda = true;
            destinoX = transform.position.x - distanciaMovimientoLateral;
        }
    }

    void FixedUpdate()
    {
        // Movimiento hacia adelante constante
        transform.Translate(Vector3.forward * velocidadMovimiento * Time.fixedDeltaTime,Space.World);

        // Movimiento lateral hacia el destino
        if (moverDerecha)
        {
            if (transform.position.x < destinoX)
                transform.Translate(Vector3.right * velocidadMovimientoLateral * Time.fixedDeltaTime, Space.World);
            else
            {
                Vector3 eso = new Vector3(destinoX, transform.position.y, transform.position.z);
                transform.position = eso;
                moverDerecha = false;
            }
                
        }
        else if (moverIzquierda)
        {
            if (transform.position.x > destinoX)
                transform.Translate(Vector3.left * velocidadMovimientoLateral * Time.fixedDeltaTime, Space.World);
            else
            {
                Vector3 eso = new Vector3(destinoX, transform.position.y, transform.position.z);
                transform.position = eso;
                moverIzquierda = false;
            }
                
        }
    }
}
