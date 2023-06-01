using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed;

    private int desiredLine=1;
    public float lineDistance;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        direction.z = forwardSpeed;
        if(SwipeManager._swipeRight)
        {
            desiredLine++;
            if(desiredLine==3)
            {
                desiredLine = 2;
            }
        }
        if (SwipeManager._swipeLeft)
        {
            desiredLine--;
            if (desiredLine == -1)
            {
                desiredLine = 0;
            }
        }

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if(desiredLine==0)
        {
            targetPosition += Vector3.left * lineDistance;
        }
        else if(desiredLine==2)
        {
            targetPosition += Vector3.right * lineDistance;
        }
        transform.position = Vector3.Lerp(transform.position,targetPosition,10f*Time.deltaTime);

        
    }

    private void FixedUpdate()
    {
        controller.Move(direction * Time.fixedDeltaTime);
    }
}
