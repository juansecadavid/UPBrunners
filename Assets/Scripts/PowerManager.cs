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
    private bool[] isUsingPower=new bool[2];
    private float timeOfPower=0f;
    private float[] time=new float[2];
    private PowerCoins powerCoins;
    [SerializeField]
    private Slider[] powerBar;
    private  SoundManager soundManager;
    // Update is called once per frame
    private void Start()
    {
        powerCoins = FindObjectOfType<PowerCoins>();
        soundManager=FindObjectOfType<SoundManager>();
    }
    void Update()
    {
        if (isUsingPower[0]&&!GameManager.IsPaused)
        {
            time[0] += Time.deltaTime;
            powerBar[0].value -= Time.deltaTime;
            if (time[0] > timeOfPower)
            {
                powerBar[0].gameObject.SetActive(false);
                isUsingPower[0] = false;
                time[0] = 0f;
            }
        }
        if (isUsingPower[1]&&!GameManager.IsPaused)
        {
            time[1] += Time.deltaTime;
            powerBar[1].value -= Time.deltaTime;
            if (time[1] > timeOfPower)
            {
                powerBar[1].gameObject.SetActive(false);
                isUsingPower[1] = false;
                time[1] = 1f;
            }
        }
    }

    public void PowerAutoPlay(int amountCoins)
    {
        if (!isUsingPower[0] &&powerCoins.Coins>=amountCoins)
        {
            soundManager.PlaySound(3);
            powerCoins.Coins = powerCoins.Coins - amountCoins;
            StartCoroutine(AutoPlay());
        }         
    }
    public void PowerBonification(int amountCoins)
    {
        if (!isUsingPower[1] && powerCoins.Coins >= amountCoins)
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
        powerBar[0].maxValue = timeOfPower;
        powerBar[0].value = powerBar[0].maxValue;
        powerBar[0].gameObject.SetActive(true);
        isUsingPower[0] = true;
        yield return new WaitForSeconds(5f);
        //principalCollider.SetActive(true);
        automatic.gameObject.SetActive(false);
    }
    IEnumerator Bonification()
    {
        bonificationPoll.gameObject.SetActive(true);
        timeOfPower = 15f;
        powerBar[1].maxValue = timeOfPower;
        powerBar[1].value = powerBar[1].maxValue;
        powerBar[1].gameObject.SetActive(true);
        isUsingPower[1] = true;
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
