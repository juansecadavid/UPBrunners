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
    private float initialYPosition; // Posici�n Y inicial del personaje
    private bool isJumping = false; // Variable que indica si el personaje est� realizando un salto
    private int maxAttempts = 3; // M�ximo n�mero de intentos para encontrar un carril libre
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
                 if (isJumping) // Verificar si ya est� en medio de un salto
                 {/*
                     mov.Roll(); // Realizar el gesto de rodar para cancelar el salto anterior
                     isJumping = false;
                     StartCoroutine(JumpAgain());
                     StartCoroutine(ResetCanJump()); // Iniciar el retraso antes de permitir el salto adicional nuevamente
                     */
                     isJumping = false;
                     StartCoroutine(ResetCanJump());
                     if (transform.position.x <= 0f)
                     {
                         // Intentar moverse hacia la derecha
                         TryMoveToFreeLane(1);
                     }
                     else if (transform.position.x > 0f)
                     {
                         // Intentar moverse hacia la izquierda
                         TryMoveToFreeLane(-1);
                     }
                 }
                 else
                 {
                     mov.Jump();
                     isJumping = true;
                     canJump = false; // Desactivar el salto adicional hasta alcanzar la posici�n Y inicial nuevamente
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
             if (transform.position.x <= 0f)
             {
                 // Intentar moverse hacia la derecha
                 TryMoveToFreeLane(1);
             }
             else if (transform.position.x > 0f)
             {
                 // Intentar moverse hacia la izquierda
                 TryMoveToFreeLane(-1);
             }
         }
        else if (other.CompareTag("Car"))
        {
            // Evitar colisionar con el objeto "car"
            if (transform.position.x <= 0f)
            {
                TryMoveToFreeLane(1);
            }
            else if (transform.position.x > 0f)
            {
                TryMoveToFreeLane(-1);
            }
        }
    }

    private void TryMoveToFreeLane(int direction)
    {
        int currentLane = Mathf.RoundToInt(transform.position.x);
        int targetLane = currentLane + direction;
        int attempts = 0;

        // Intentar encontrar un carril libre
        while (attempts < maxAttempts && !IsLaneFree(targetLane))
        {
            targetLane += direction;
            attempts++;
        }

        // Moverse al carril libre si se encontr� uno
        if (IsLaneFree(targetLane))
        {
            if (direction > 0)
            {
                mov.MoveRight();
            }
            else
            {
                mov.MoveLeft();
            }
        }
    }

    private bool IsLaneFree(int lane)
    {
        // Verificar si el carril est� libre de objetos "Tree"
        Collider[] colliders = Physics.OverlapSphere(new Vector3(lane, transform.position.y, transform.position.z), 0.5f);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Tree"))
            {
                return false;
            }
            if (collider.CompareTag("Car"))
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
