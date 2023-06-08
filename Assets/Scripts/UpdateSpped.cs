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
        gameTime+=Time.deltaTime;
        float currentSpeed = Mathf.Lerp(movement.velocidadMovimiento,maxSpeed,gameTime/maxTime);
        movement.velocidadMovimiento = currentSpeed;
        
    }
}
