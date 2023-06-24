using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerManager : MonoBehaviour
{
    [SerializeField]
    private PlayAutomatic automatic;
    [SerializeField]
    private GameObject principalCollider;
    private bool isUsingPower=false;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(AutoPlay());
        }
    }

    IEnumerator AutoPlay()
    {
        principalCollider.SetActive(false);
        automatic.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        principalCollider.SetActive(true);
        automatic.gameObject.SetActive(false);
    }
}
