using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateSpped : MonoBehaviour
{
    private Movement movement;
    private float gameTime;
    [SerializeField]
    private float maxTime;
    [SerializeField]
    private float maxSpeed;
    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<Movement>();
        gameTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        gameTime+=Time.deltaTime;
        
    }
}
