using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PicUpExperience : MonoBehaviour
{
    private SoundManager soundManager;
    // Start is called before the first frame update
    private void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
        StartCoroutine(Animate());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PowerCoins coins = other.GetComponentInParent<PowerCoins>();
            coins.Coins ++;
            soundManager.PlaySound(0);
            Destroy(gameObject);
        }
    }
    IEnumerator Animate()
    {
        while (true)
        {
            LeanTween.moveY(gameObject, 2.5f, 0.5f);
            yield return new WaitForSeconds(0.5f);
            LeanTween.moveY(gameObject, 1.5f, 0.5f);
            yield return new WaitForSeconds (0.5f);
        }
    }
}
