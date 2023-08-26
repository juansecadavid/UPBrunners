using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PicUpExperience : MonoBehaviour
{
    private SoundManager soundManager;
    private LevelManager levelManager;
    private Vector3 initialPos;
    // Start is called before the first frame update
    private void Awake()
    {
        soundManager = FindObjectOfType<SoundManager>();
        levelManager = FindObjectOfType<LevelManager>();
        //StartCoroutine(Animate());
        initialPos=transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PowerCoins coins = other.GetComponentInParent<PowerCoins>();
            coins.Coins ++;
            soundManager.PlaySound(0);
            levelManager.HideAndShow(gameObject);
        }
    }
    IEnumerator Animate()
    {
        while (gameObject.activeInHierarchy)
        {
            LeanTween.moveY(gameObject, transform.position.y+1, 0.5f);
            yield return new WaitForSeconds(0.5f);
            LeanTween.moveY(gameObject, transform.position.y-1, 0.5f);
            yield return new WaitForSeconds (0.5f);
        }
    }
    private void OnEnable()
    {
        Vector3 pos= transform.position;
        pos.y=initialPos.y;
        transform.position = pos;
        StartCoroutine(Animate());
    }

}
