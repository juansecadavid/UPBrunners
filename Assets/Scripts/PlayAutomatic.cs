using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAutomatic : MonoBehaviour
{
    /*Movement mov;
    // Start is called before the first frame update
    void Start()
    {
        mov=GetComponentInParent<Movement>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Chair"))
        {
            mov.Jump();
        }
        else if(other.CompareTag("Table"))
        {
            mov.Roll();
        }
        else if(other.CompareTag("Tree"))
        {
            if(transform.position.x<=0f)
            {
                mov.MoveRight();
            }
            else if(transform.position.x>0f)
            {
                mov.MoveLeft();
            }
        }
    }*/

    Movement mov;
    private bool canJump = true; // Variable que controla si se permite un salto adicional
    private float initialYPosition; // Posición Y inicial del personaje
    private bool isJumping = false; // Variable que indica si el personaje está realizando un salto
    private int maxAttempts = 4; // Máximo número de intentos para encontrar un carril libre
    [SerializeField]
    private GameObject player;
    public float lastPosi = 0;

    void Start()
    {
        mov = GetComponentInParent<Movement>();
        initialYPosition = transform.position.y;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Chair"))
        {
            if (canJump) // Verificar si se permite el salto adicional
            {
                if (isJumping) // Verificar si ya está en medio de un salto
                {/*
                    mov.Roll(); // Realizar el gesto de rodar para cancelar el salto anterior
                    isJumping = false;
                    StartCoroutine(JumpAgain());
                    StartCoroutine(ResetCanJump()); // Iniciar el retraso antes de permitir el salto adicional nuevamente
                    */
                    isJumping = false;
                    StartCoroutine(ResetCanJump());
                    if (player.transform.position.x == 0f)
                    {
                        // Intentar moverse hacia la derecha
                        //TryMoveToFreeLane(2.5f);
                        if (lastPosi > 0)
                        {
                            mov.MoveLeft();
                        }
                        else
                        {
                            mov.MoveRight();
                        }

                    }
                    else if (player.transform.position.x < 0f)
                    {
                        mov.MoveRight();
                        lastPosi = -2.5f;
                        // Intentar moverse hacia la izquierda
                        //TryMoveToFreeLane(-2.5f);
                    }
                    else if(player.transform.position.x > 0f)
                    {
                        mov.MoveLeft();
                        lastPosi = 2.5f;
                    }
                }
                else
                {
                    mov.Jump();
                    isJumping = true;
                    canJump = false; // Desactivar el salto adicional hasta alcanzar la posición Y inicial nuevamente
                    StartCoroutine(ResetCanJump());
                }
            }
            else
            {
                mov.Roll(); // Realizar el gesto de rodar para cancelar el salto anterior
            }
        }
        else if (other.CompareTag("Table"))
        {
            mov.Roll();
        }
        else if (other.CompareTag("Tree"))
        {
            if (player.transform.position.x == 0f)
            {
                if(lastPosi>0)
                {
                    mov.MoveLeft();
                }
                else
                {
                    mov.MoveRight();
                }
                // Intentar moverse hacia la derecha
                //TryMoveToFreeLane(2.5f);
            }
            else if (player.transform.position.x < 0f)
            {
                mov.MoveRight();
                lastPosi = -2.5f;
                // Intentar moverse hacia la izquierda
                //TryMoveToFreeLane(-2.5f);
            }
            else if(player.transform.position.x>0f)
            {
                mov.MoveLeft();
                lastPosi = 2.5f;
            }
        }
    }

    private void TryMoveToFreeLane(float direction)
    {
        float currentLane = player.transform.position.x;   
        float targetLane = currentLane + direction;
        int attempts = 0;

        // Intentar encontrar un carril libre
        while (attempts < maxAttempts && !IsLaneFree(targetLane))
        {
            targetLane += direction;
            attempts++;
        }

        // Moverse al carril libre si se encontró uno
        if (IsLaneFree(targetLane))
        {
            if (direction > 0)
            {
                mov.MoveRight();
                lastPosi = player.transform.position.x;
            }
            else
            {
                mov.MoveLeft();
                lastPosi = player.transform.position.x;
            }
        }
    }

    private bool IsLaneFree(float lane)
    {
        Vector3 nuevoVect = new Vector3(lane,player.transform.position.y,player.transform.position.z);
        // Verificar si el carril está libre de objetos "Tree"
        Collider[] colliders = Physics.OverlapBox(nuevoVect,new Vector3(7.7f,2.6f,1));
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Tree"))
            {
                return false;
            }
        }
        return true;
    }

    private IEnumerator ResetCanJump()
    {
        yield return new WaitForSeconds(0.1f); // Esperar 0.1 segundos antes de permitir el salto adicional
        canJump = true;
        yield return new WaitForSeconds(1f);
        isJumping = false;

    }

    private IEnumerator JumpAgain()
    {
        yield return new WaitForSeconds(0.01f);
        mov.Jump();
    }
}
