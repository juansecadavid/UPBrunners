using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionLive : MonoBehaviour
{
    [SerializeField]
    PowerCoins coins;
    [SerializeField]
    Score HighScore;
    public int coinsCollected;
    public int score;
    MissionManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager=FindAnyObjectByType<MissionManager>();
    }

    // Update is called once per frame
    void Update()
    {
        coinsCollected=coins.Coins;
        score = HighScore.CurrentNumber;
        manager.CheckMissionsStatus();
    }
}
