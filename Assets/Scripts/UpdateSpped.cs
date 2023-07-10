using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateSpped : MonoBehaviour
{
    private Movement movement;
    private float gameTime=0f;
    [SerializeField]
    private float maxTime;
    [SerializeField]
    private float maxSpeed;
    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(movement.velocidadMovimiento<=50f)
        {
            gameTime += Time.deltaTime;
            if (gameTime >= (1 / 2f))
            {
                movement.velocidadMovimiento += 0.1f;
                movement.velocidadMovimientoLateral += 0.025f;
                gameTime = 0f;
            }
        }
        
        /*float currentSpeed = Mathf.Lerp(movement.velocidadMovimiento,maxSpeed,gameTime/maxTime);
        movement.velocidadMovimiento = currentSpeed;*/
        
    }
}
