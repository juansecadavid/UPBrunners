using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerButtons : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private int coinsNeeded;
    [SerializeField]
    private PowerCoins powerCoins;
    private Image image;
    // Update is called once per frame
    private void Start()
    {
        image = GetComponent<Image>();
    }
    void Update()
    {
        //StartCoroutine(hasEnoughCoins());
        if (powerCoins.Coins < coinsNeeded)
        {
            Color colorNew = image.color;
            colorNew.a = 0.5019608f;
            image.color = colorNew;
        }
        else
        {
            Color colorNew = image.color;
            colorNew.a = 1f;
            image.color = colorNew;
        }
    }
    IEnumerator hasEnoughCoins()
    {
        if(powerCoins.Coins>=coinsNeeded)
        {
            Color colorNew = image.color; 
            colorNew.a = 100;
            image.color=colorNew;
        }else
        {
            Color colorNew = image.color;
            colorNew.a = 255;
            image.color = colorNew;
        }
        yield return new WaitForSeconds(0.2f);
    }
}
