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
    private bool[] isUsingPower=new bool[3];
    private float[] timeOfPower=new float[3];
    private float[] time=new float[3];
    private PowerCoins powerCoins;
    [SerializeField]
    private Slider[] powerBar;
    private  SoundManager soundManager;
    private bool boonPowerOnGoing=false;
    private Movement movement;
    private Score score;
    UpdateSpped updSpd;
    private float speed;
    // Update is called once per frame
    private void Start()
    {
        movement = FindObjectOfType<Movement>();
        powerCoins = FindObjectOfType<PowerCoins>();
        soundManager=FindObjectOfType<SoundManager>();
        score = FindObjectOfType<Score>();
        updSpd = FindObjectOfType<UpdateSpped>();
        speed = movement.velocidadMovimiento;
    }
    void Update()
    {
        if (isUsingPower[0] && !GameManager.IsPaused)
        {
            time[0] += Time.deltaTime;
            powerBar[0].value -= Time.deltaTime;
            if (time[0] > timeOfPower[0])
            {
                powerBar[0].gameObject.SetActive(false);
                isUsingPower[0] = false;
                time[0] = 0f;
                automatic.gameObject.SetActive(false);
            }
        }
        if (isUsingPower[1] && !GameManager.IsPaused&&boonPowerOnGoing)
        {
            time[1] += Time.deltaTime;
            powerBar[1].value -= Time.deltaTime;
            if (time[1] > timeOfPower[1])
            {
                powerBar[1].gameObject.SetActive(false);
                isUsingPower[1] = false;
                time[1] = 0f;
                bonificationPoll.gameObject.SetActive(false);
                bonButtons.ForEach(Destroy);
                bonButtons.Clear();
                boonPowerOnGoing = false;
            }
        }
        else if (GameManager.IsPaused&&boonPowerOnGoing)
        {
            isUsingPower[1] = false;
        }
        else if(!GameManager.IsPaused&&!isUsingPower[1]&&boonPowerOnGoing)
        {
            isUsingPower[1] = true;
            StartCoroutine(BonificationInstantiator());
        }
        if (isUsingPower[2] && !GameManager.IsPaused)
        {
            time[2] += Time.deltaTime;
            powerBar[2].value -= Time.deltaTime;
            if (time[2] > timeOfPower[2])
            {
                powerBar[2].gameObject.SetActive(false);
                isUsingPower[2] = false;
                time[2] = 0f;
                score.ScoreMultiplyer = 1;
                updSpd.enabled = true;
                movement.velocidadMovimiento = speed;
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
    public void PowerDoubleScore(int amountCoins)
    {
        if (!isUsingPower[2] && powerCoins.Coins >= amountCoins)
        {
            soundManager.PlaySound(5);
            powerCoins.Coins = powerCoins.Coins - amountCoins;
            StartCoroutine(DoubleScore());
        }
    }
    IEnumerator AutoPlay()
    {
        //principalCollider.SetActive(false);
        automatic.gameObject.SetActive(true);
        timeOfPower[0] = 5f;
        powerBar[0].maxValue = timeOfPower[0];
        powerBar[0].value = powerBar[0].maxValue;
        powerBar[0].gameObject.SetActive(true);
        isUsingPower[0] = true;
        yield return null;
    }
    IEnumerator Bonification()
    {
        bonificationPoll.gameObject.SetActive(true);
        timeOfPower[1] = 15f;
        powerBar[1].maxValue = timeOfPower[1];
        powerBar[1].value = powerBar[1].maxValue;
        powerBar[1].gameObject.SetActive(true);
        isUsingPower[1] = true;
        boonPowerOnGoing = true;
        StartCoroutine(BonificationInstantiator());
        yield return null;        
    }
    IEnumerator BonificationInstantiator()
    {
        do
        {
            Vector3 pos = new Vector3(Random.Range(-140, 42), Random.Range(-380, 200), 0);
            Button instance = Instantiate(bonPrefab, bonificationPoll.transform);
            RectTransform rectTransform = instance.GetComponent<RectTransform>();
            rectTransform.LeanSetLocalPosX(pos.x);
            rectTransform.LeanSetLocalPosY(pos.y);
            rectTransform.LeanSetLocalPosZ(0);
            bonButtons.Add(instance.gameObject);
            instance.gameObject.SetActive(true);
            yield return new WaitForSeconds(1f);
        }
        while (isUsingPower[1]);
    }
    IEnumerator DoubleScore()
    {
        timeOfPower[2] = 10;
        powerBar[2].maxValue = timeOfPower[2];
        powerBar[2].value = powerBar[2].maxValue;
        powerBar[2].gameObject.SetActive(true);
        isUsingPower[2] = true;
        movement.velocidadMovimiento += 10;      
        score.ScoreMultiplyer = 2;
        updSpd.enabled = false;
        yield return null;
    }
}
