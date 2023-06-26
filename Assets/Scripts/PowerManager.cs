using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerManager : MonoBehaviour
{
    [SerializeField]
    private PlayAutomatic automatic;
    [SerializeField]
    private GameObject principalCollider;
    private bool isUsingPower=false;
    private float timeOfPower=0f;
    private float time=0f;
    private PowerCoins powerCoins;
    [SerializeField]
    private Slider powerBar;
    // Update is called once per frame
    private void Start()
    {
        powerCoins = FindObjectOfType<PowerCoins>();
    }
    void Update()
    {
        if(isUsingPower)
        {
            time += Time.deltaTime;
            powerBar.value -= Time.deltaTime;
            if(time > timeOfPower)
            {
                powerBar.gameObject.SetActive(false);
                isUsingPower = false;
                time = 0f;
            }
        }
    }

    public void PowerAutoPlay(int amountCoins)
    {
        if(!isUsingPower&&powerCoins.Coins>=amountCoins)
        {
            powerCoins.Coins = powerCoins.Coins - amountCoins;
            StartCoroutine(AutoPlay());
        }
            
    }
    IEnumerator AutoPlay()
    {
        //principalCollider.SetActive(false);
        automatic.gameObject.SetActive(true);
        timeOfPower = 5f;       
        powerBar.maxValue = timeOfPower;
        powerBar.value=powerBar.maxValue;
        powerBar.gameObject.SetActive(true);
        isUsingPower = true;
        yield return new WaitForSeconds(5f);
        //principalCollider.SetActive(true);
        automatic.gameObject.SetActive(false);
    }
}
