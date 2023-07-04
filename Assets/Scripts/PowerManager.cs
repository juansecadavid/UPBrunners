using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PowerManager : MonoBehaviour
{
    [SerializeField]
    private PlayAutomatic automatic;
    [SerializeField]
    private GameObject principalCollider;
    [SerializeField]
    private GameObject bonificationPoll;
    [SerializeField]
    private Button bonPrefab;
    List<GameObject> bonButtons = new List<GameObject>();
    private bool isUsingPower=false;
    private float timeOfPower=0f;
    private float time=0f;
    private PowerCoins powerCoins;
    [SerializeField]
    private Slider powerBar;
    private  SoundManager soundManager;
    // Update is called once per frame
    private void Start()
    {
        powerCoins = FindObjectOfType<PowerCoins>();
        soundManager=FindObjectOfType<SoundManager>();
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
            soundManager.PlaySound(3);
            powerCoins.Coins = powerCoins.Coins - amountCoins;
            StartCoroutine(AutoPlay());
        }         
    }
    public void PowerBonification(int amountCoins)
    {
        if(!isUsingPower && powerCoins.Coins >= amountCoins)
        {
            soundManager.PlaySound(4);
            powerCoins.Coins = powerCoins.Coins - amountCoins;
            StartCoroutine(Bonification());
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
    IEnumerator Bonification()
    {
        bonificationPoll.gameObject.SetActive(true);
        timeOfPower = 15f;
        powerBar.maxValue = timeOfPower;
        powerBar.value = powerBar.maxValue;
        powerBar.gameObject.SetActive(true);
        isUsingPower = true;
        StartCoroutine(BonificationInstantiator());
        yield return new WaitForSeconds(timeOfPower);
        //principalCollider.SetActive(true);
        bonificationPoll.gameObject.SetActive(false);
        bonButtons.ForEach(Destroy);
        bonButtons.Clear();
    }
    IEnumerator BonificationInstantiator()
    {
        for (int i = 0; i < timeOfPower; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-140, 42), Random.Range(-380, 200), 0);
            
            Button instance=Instantiate(bonPrefab,bonificationPoll.transform);
            RectTransform rectTransform = instance.GetComponent<RectTransform>();
            rectTransform.LeanSetLocalPosX(pos.x);
            rectTransform.LeanSetLocalPosY(pos.y);
            rectTransform.LeanSetLocalPosZ(0);
            bonButtons.Add(instance.gameObject);
            instance.gameObject.SetActive(true);
            yield return new WaitForSeconds(1f);
        }
    }
}
