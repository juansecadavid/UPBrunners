using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpLetras : MonoBehaviour
{
    [SerializeField]
    private int letra;
    private SoundManager soundManager;
    private Transform parent;
    public LetterSpawner letterParent;
    Vector3 initialY;
    private void Awake()
    {
        parent = GetComponentInParent<Transform>();
        soundManager = FindObjectOfType<SoundManager>();
        letterParent=GetComponentInParent<LetterSpawner>();
        initialY=transform.position;
    }
    private void Start()
    {   
        /*
        StartCoroutine(Animation());
        StartCoroutine(Animation2());*/
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GameManager.Letras = letra;
            soundManager.PlaySound(1);
            if (GameManager.ActiveLetter1.Count > 0)
            {
                GameManager.ActiveLetter1.RemoveAt(0);
            }
            //letterParent.SpawnLetter(letra);
            if(letra == 0)
            {
                letterParent.Instantiatedletters[2].gameObject.SetActive(false);
            }
            else
            {
                letterParent.Instantiatedletters[letra-1].gameObject.SetActive(false);
            }
            
            
        }     
    }
    IEnumerator Animation()
    {
        while(true)
        {
            LeanTween.moveY(gameObject, transform.position.y+2, 0.5f);
            //LeanTween.rotateX(gameObject, 360, 1f);
            yield return new WaitForSeconds(0.5f);
            LeanTween.moveY(gameObject, transform.position.y-2, 0.5f);
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
    private void OnEnable()
    {
        Vector3 pos = transform.position;
        pos.y = initialY.y;
        transform.position = pos;
        StartCoroutine(Animation());
        StartCoroutine(Animation2());
    }
}
