using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAutomatic : MonoBehaviour
{
    Movement mov;
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
    }
}
