using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PowerCoins : MonoBehaviour
{
    private int coins = 0;
    [SerializeField]
    private TextMeshProUGUI coinsText;
    public int Coins { get => coins; set => coins = value; }

    // Start is called before the first frame update
    void Start()
    {
        coinsText.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        coinsText.text = $"{coins}";
    }
}
