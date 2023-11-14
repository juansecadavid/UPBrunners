using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject objetoXd;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
