using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float velocidadMovimiento = 5f;  // Velocidad de movimiento del jugador
    public float velocidadMovimientoLateral = 5f;  // Velocidad de movimiento lateral del jugador
    public float distanciaMovimientoLateral = 3f;  // Distancia a moverse lateralmente al deslizar
    public float alturaSalto = 5f;  // Altura que el jugador alcanzará al saltar
    public float velocidadSalto = 5f;  // Velocidad de ascenso y descenso en el salto

    private bool moverDerecha = false;
    private bool moverIzquierda = false;
    private float destinoX;
    private bool saltando = false;
    private float tiempoSubiendo = 1f;
    private float tiempoSalto = 0f;
    private float alturaInicial;
    public float eso=0;
    private float carriles = 0;

    private Animator animator;
    private Transform child;
    bool firstTime=true;

    private Vector2 touchStartPosition;
    private Vector2 touchEndedPosition;
    private void Start()
    {
        try
        {
            animator = GetComponentInChildren<Animator>();
            child=GetComponentInChildren<Transform>();

        }
        catch (System.Exception)
        {

            throw;
        }
    }
    void Update()
    {
        // Verificar si se debe mover lateralmente
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            carriles += distanciaMovimientoLateral;
            if (carriles > distanciaMovimientoLateral)
            {
                carriles = distanciaMovimientoLateral;
            }
            moverDerecha = true;
            moverIzquierda = false;
            destinoX = carriles;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            carriles -= distanciaMovimientoLateral;
            if (carriles < -distanciaMovimientoLateral)
            {
                carriles = -distanciaMovimientoLateral;
            }
            moverDerecha = false;
            moverIzquierda = true;
            destinoX = carriles;
        }

        // Verificar deslizamiento táctil
        if (Input.touchCount > 0&& Input.GetTouch(0).phase==TouchPhase.Began)
        {
            touchStartPosition = Input.GetTouch(0).position;
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            touchEndedPosition = Input.GetTouch(0).position;

            float mayorY = Mathf.Abs(touchStartPosition.y - touchEndedPosition.y);
            float mayorX = Mathf.Abs(touchStartPosition.x - touchEndedPosition.x);
            if (mayorY > mayorX)
            {
                if(touchEndedPosition.y<touchStartPosition.y)
                {

                }
                else
                {
                    if (!saltando)
                    {
                        saltando = true;
                        tiempoSalto = 0f;
                        alturaInicial = transform.position.y;
                        firstTime = true;
                        animator?.SetBool("isJumping", true);
                    }
                }
            } 
            else
            {
                if (touchEndedPosition.x < touchStartPosition.x)
                {
                    carriles -= distanciaMovimientoLateral;
                    if (carriles < -distanciaMovimientoLateral)
                    {
                        carriles = -distanciaMovimientoLateral;
                    }
                    moverDerecha = false;
                    moverIzquierda = true;
                    destinoX = carriles;
                    Debug.Log("Izquierda");
                }
                if (touchEndedPosition.x > touchStartPosition.x)
                {
                    carriles += distanciaMovimientoLateral;
                    if (carriles > distanciaMovimientoLateral)
                    {
                        carriles = distanciaMovimientoLateral;
                    }
                    moverDerecha = true;
                    moverIzquierda = false;
                    destinoX = carriles;
                    Debug.Log("Derecha");
                }
            }         
        }

        // Verificar salto
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!saltando)
            {
                saltando = true;
                tiempoSalto = 0f;
                alturaInicial = transform.position.y;
                firstTime = true;
                animator?.SetBool("isJumping", true);
            }
        }
    }

    void FixedUpdate()
    {
        // Movimiento hacia adelante constante
        transform.Translate(Vector3.forward * velocidadMovimiento * Time.fixedDeltaTime, Space.World);

        // Movimiento lateral hacia el destino
        if (moverDerecha)
        {
            if (transform.position.x < destinoX)
                transform.Translate(Vector3.right * velocidadMovimientoLateral * Time.fixedDeltaTime, Space.World);
            else
            {
                Vector3 newPos = new Vector3(destinoX, transform.position.y, transform.position.z);
                transform.position = newPos;
                moverDerecha = false;
            }
        }
        else if (moverIzquierda)
        {
            if (transform.position.x > destinoX)
                transform.Translate(Vector3.left * velocidadMovimientoLateral * Time.fixedDeltaTime, Space.World);
            else
            {
                Vector3 newPos = new Vector3(destinoX, transform.position.y, transform.position.z);
                transform.position = newPos;
                moverIzquierda = false;
            }
        }

        // Control de salto
        if (saltando)
        {
            float c =  0.5f * (-20f) * (Mathf.Pow(tiempoSalto,2f));
            eso = c;
            float a = alturaInicial + velocidadSalto * tiempoSalto;
            float alturaSaltoActual = a+c;
            tiempoSalto += Time.fixedDeltaTime;
            if(firstTime)
            {
                alturaSaltoActual = alturaInicial + (velocidadSalto * tiempoSalto) + ((1 / 2) * (-9.8f) * (tiempoSalto * tiempoSalto));
                Vector3 newPosition = transform.position;
                newPosition.y = alturaSaltoActual;
                transform.position = newPosition;
                firstTime = false;
            }
            else
            {
                if (alturaSaltoActual > 1.5f)
                {
                    Vector3 newPosition = transform.position;
                    newPosition.y = alturaSaltoActual;
                    transform.position = newPosition;
                }
                else
                {
                    saltando = false;
                    Vector3 newPosition = transform.position;
                    newPosition.y = alturaInicial;
                    transform.position = newPosition;
                    animator?.SetBool("isJumping", false);
                    
                }
            }
            
        }
    }
    /*
    public float velocidadMovimiento = 5f;  // Velocidad de movimiento del jugador
    public float velocidadMovimientoLateral = 5f;  // Velocidad de movimiento lateral del jugador
    public float distanciaMovimientoLateral = 3f;  // Distancia a moverse lateralmente al deslizar

    private bool moverDerecha = false;
    private bool moverIzquierda = false;
    private float destinoX;

    private float carriles=0;



    void Update()
    {
        // Verificar si se debe mover lateralmente
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            carriles+=distanciaMovimientoLateral;
            if(carriles>distanciaMovimientoLateral)
            {
                carriles = distanciaMovimientoLateral;
            }
            moverDerecha = true;
            moverIzquierda = false;
            destinoX = carriles;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            carriles -= distanciaMovimientoLateral;
            if (carriles < -distanciaMovimientoLateral)
            {
                carriles = -distanciaMovimientoLateral;
            }
            moverDerecha = false;
            moverIzquierda = true;
            destinoX = carriles;
        }

        // Verificar deslizamiento táctil
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                if (touchPos.x > transform.position.x)
                {
                    moverDerecha = true;
                    moverIzquierda = false;
                    destinoX = transform.position.x + distanciaMovimientoLateral;
                }
                else
                {
                    moverDerecha = false;
                    moverIzquierda = true;
                    destinoX = transform.position.x - distanciaMovimientoLateral;
                }
            }
        }
    }

    void FixedUpdate()
    {
        // Movimiento hacia adelante constante
        transform.Translate(Vector3.forward * velocidadMovimiento * Time.fixedDeltaTime, Space.World);

        // Movimiento lateral hacia el destino
        if (moverDerecha)
        {
            if (transform.position.x < destinoX)
                transform.Translate(Vector3.right * velocidadMovimientoLateral * Time.fixedDeltaTime, Space.World);
            else
            {
                Vector3 newPos = new Vector3(destinoX, transform.position.y, transform.position.z);
                transform.position = newPos;
                moverDerecha = false;
            }
        }
        else if (moverIzquierda)
        {
            if (transform.position.x > destinoX)
                transform.Translate(Vector3.left * velocidadMovimientoLateral * Time.fixedDeltaTime, Space.World);
            else
            {
                Vector3 newPos = new Vector3(destinoX, transform.position.y, transform.position.z);
                transform.position = newPos;
                moverIzquierda = false;
            }
        }
    }*/
    private void OnMouseDrag()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("derecha");
        }
        
    }
}
