using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPosition : MonoBehaviour
{
    public Vector3 initialPos;
    // Start is called before the first frame update
    void Awake()
    {
        initialPos = transform.position;
    }

    // Update is called once per frame
    public void ResetPos()
    {
        Vector3 pos=transform.position;
        pos.y=initialPos.y;
        pos.x =initialPos.x;
        transform.position = pos;
    }
}
