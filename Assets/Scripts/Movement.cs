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
    private bool rolling=false;
    private float tiempoSalto = 0f;
    private float alturaInicial=1.5f;
    public float eso=0;
    private float rollingTime=0f;
    private float carriles = 0;
    private float changingTime=0f;
    private bool isChanging=false;

    private bool isTouchingTable=false;

    private Animator animator;
    bool firstTime=true;

    private Vector2 touchStartPosition;
    private Vector2 touchEndedPosition;

    public bool Rolling { get => rolling; set => rolling = value; }
    public bool IsTouchingTable { get => isTouchingTable; set => isTouchingTable = value; }
    public bool Saltando { get => saltando; set => saltando = value; }

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = -1;
        try
        {
            animator = GetComponentInChildren<Animator>();

        }
        catch (System.Exception)
        {

            throw;
        }
    }
    void Update()
    {
        
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
                    if(!GameManager.IsAutoPlaying)
                    {
                        Roll();
                    }              
                }
                else
                {
                    if(!GameManager.IsAutoPlaying)
                    {
                        Jump();
                    }              
                }
            } 
            else
            {
                if (touchEndedPosition.x < touchStartPosition.x)
                {
                    if(!GameManager.IsAutoPlaying)
                    {
                        MoveLeft();
                    }        
                }
                if (touchEndedPosition.x > touchStartPosition.x)
                {
                    if(!GameManager.IsAutoPlaying)
                    {
                        MoveRight();
                    }
                }
            }         
        }
        if (rolling)
        {
            rollingTime += Time.deltaTime;
            if (rollingTime >  0.8f)
            {
                rolling = false;
                animator?.SetBool("isRolling", false);
            }
            if(saltando)
            {
                rolling = false;
                animator?.SetBool("isRolling", false);
            }
        }
        if(isChanging)
        {
            changingTime += Time.deltaTime;
            if(changingTime>0.1f)
            {
                isChanging = false;
                animator?.SetBool("runRight", false);
                animator?.SetBool("runLeft",false);
            }
        }
        // Verificar si se debe mover lateralmente
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();

        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft();

        }
        // Verificar salto
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Roll();
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
            if (isTouchingTable == true)
            {
                alturaInicial = 3.5f;
            }
            else
            {
                alturaInicial = 1.5f;
            }
            if (firstTime)
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
    public void MoveRight()
    {
        carriles += distanciaMovimientoLateral;
        if (carriles > distanciaMovimientoLateral)
        {
            carriles = distanciaMovimientoLateral;
        }
        moverDerecha = true;
        moverIzquierda = false;
        if (destinoX != carriles)
        {
            animator?.SetBool("runLeft", false);
            animator?.SetBool("runRight", true);
            changingTime = 0f;
            isChanging = true;
        }
        destinoX = carriles;
    }
    public void MoveLeft()
    {
        carriles -= distanciaMovimientoLateral;
        if (carriles < -distanciaMovimientoLateral)
        {
            carriles = -distanciaMovimientoLateral;
        }
        moverDerecha = false;
        moverIzquierda = true;
        if (destinoX != carriles)
        {
            animator?.SetBool("runRight", false);
            animator?.SetBool("runLeft", true);
            changingTime = 0f;
            isChanging = true;
        }
        destinoX = carriles;
    }
    public void Roll()
    {
        if (!rolling)
        {
            rolling = true;
            rollingTime = 0f;
            animator?.SetBool("isRolling", true);
            if (saltando)
            {
                saltando = false;
                Vector3 newPosition = transform.position;
                newPosition.y = alturaInicial;
                transform.position = newPosition;
                animator?.SetBool("isJumping", false);
            }
        }
    }
    public void Jump()
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
    public void HasLost()
    {
        Vector3 newPos = transform.position;
        newPos.z = transform.position.z - 1;
        transform.position = newPos;
        animator.SetBool("hasLost", true);
    }
    public void HasRespawn()
    {
        animator.SetBool("hasLost", false);
    }
    public void MoveToLane(float targetLane)
    {
        carriles = targetLane;
        moverDerecha = false;
        moverIzquierda = false;
        if (destinoX != carriles)
        {
            if (carriles > destinoX)
            {
                animator?.SetBool("runLeft", false);
                animator?.SetBool("runRight", true);
            }
            else
            {
                animator?.SetBool("runRight", false);
                animator?.SetBool("runLeft", true);
            }
            changingTime = 0f;
            isChanging = true;
        }
        destinoX = carriles;
    }
    public void CambiarAnimator(Animator animNew)
    {
        animator=animNew;
    }
}
