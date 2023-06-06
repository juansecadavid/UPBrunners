using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationFixer : MonoBehaviour
{
    /*Vector3 vector3 = new Vector3(0, -1, 0);
    Quaternion vector2 = new Quaternion(0, 90, 0, 0);*/
    Vector3 vet;
    Quaternion q;
    // Start is called before the first frame update
    void Start()
    {
        vet = transform.localPosition;
        q = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.position=vet;
        
        transform.rotation = q;
    }
}
