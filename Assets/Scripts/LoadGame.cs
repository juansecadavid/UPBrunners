using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadGame : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI highScoreText;
    [SerializeField]
    private GameObject[] skins;
    [SerializeField]
    private string[] names;
    [SerializeField]
    private TextMeshProUGUI name;
    // Start is called before the first frame update
    void Awake()
    {
        SaveSystem.LoadGame(); 
        SaveSystem.LoadVolume();
        SaveSystem.LoadSkin();
        SaveSystem.LoadSchool();
        highScoreText.text = $"HighScore {GameManager.HighScore}";
        skins[GameManager.Skin].SetActive(true);
        name.text = $"{names[GameManager.Skin]}";
    }
    private void Update()
    {
        name.text = $"{names[GameManager.Skin]}";
    }
    public void RightSkinBtn()
    {
        
        if(GameManager.Skin+1<=skins.Length-1)
        {
            skins[GameManager.Skin].SetActive(false);
            skins[GameManager.Skin + 1].SetActive(true);
            GameManager.Skin++;
        }
        else
        {
            skins[GameManager.Skin].SetActive(false);
            skins[0].SetActive(true);
            GameManager.Skin = 0;
        }      
    }
    public void LeftSkinBtn()
    {
        if(GameManager.Skin==0)
        {
            skins[0].SetActive(false);
            skins[skins.Length-1].SetActive(true);
            GameManager.Skin = skins.Length - 1;
        }
        else
        {
            skins[GameManager.Skin].SetActive(false);
            skins[GameManager.Skin - 1].SetActive(true);
            GameManager.Skin--;
        }
    }
}
