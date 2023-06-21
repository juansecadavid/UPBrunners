using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadGame : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI highScoreText;
    // Start is called before the first frame update
    void Awake()
    {
        SaveSystem.LoadGame(); 
        SaveSystem.LoadVolume();
    }
    private void Start()
    {
        highScoreText.text = $"HighScore: {GameManager.HighScore}";
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
