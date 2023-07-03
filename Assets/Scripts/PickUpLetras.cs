using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpLetras : MonoBehaviour
{
    [SerializeField]
    private int letra;
    private void Start()
    {
        StartCoroutine(Animation());
        StartCoroutine(Animation2());
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GameManager.Letras = letra;
            Destroy(gameObject);           
        }     
    }
    private void OnDestroy()
    {
        if(GameManager.ActiveLetter1.Count > 0)
        {
            GameManager.ActiveLetter1.RemoveAt(0);
        }
        
    }
    IEnumerator Animation()
    {
        while(true)
        {
            LeanTween.moveY(gameObject, 3.5f, 0.5f);
            //LeanTween.rotateX(gameObject, 360, 1f);
            yield return new WaitForSeconds(0.5f);
            LeanTween.moveY(gameObject, 1.5f, 0.5f);
            //LeanTween.rotateX(gameObject, 0, 0.5f);
            yield return new WaitForSeconds(2.5f);
        }
    }
    IEnumerator Animation2()
    {
        while (true)
        {
            LeanTween.rotateAround(gameObject, new Vector3(360,0,0),360, 1f);
            yield return new WaitForSeconds(3f);
            LeanTween.rotateAround(gameObject, new Vector3(360, 0, 0), -360, 1f);
            yield return new WaitForSeconds(3f);
        }
    }
}
