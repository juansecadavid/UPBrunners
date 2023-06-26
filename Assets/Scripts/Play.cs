using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Play : MonoBehaviour
{
    [SerializeField]
    private GameObject panelConfig;
    [SerializeField]
    private GameObject panelGeneral;
    [SerializeField]
    private GameObject panelSkin;
    private bool configPanel=false;
    private bool configSkin=false;
    [SerializeField]
    private TMP_Dropdown schoolDrop;
    // Start is called before the first frame update
    private void Start()
    {
        schoolDrop.value = GameManager.School;
    }
    public void StartGame()
    {
        SceneManager.LoadScene("LoadingScene1");
    }
    public void Prueba()
    {
        SceneManager.LoadScene("SceneTest");
    }
    public void ConfigPanel()
    {
        if(configPanel)
        {
            panelConfig.SetActive(false);
            configPanel = false;
        }
        else
        {
            panelConfig.SetActive(true);
            configPanel=true;
        }
    }
    public void SkinPanel()
    {
        if(configSkin)
        {
            SaveSystem.SaveSkin();
            panelSkin.SetActive(false);
            panelGeneral.SetActive(true);
            configSkin = false;
        }
        else
        {
            panelSkin.SetActive(true);
            panelGeneral.SetActive(false);
            configSkin=true;
        }
    }
    public void School()
    {
        GameManager.School = schoolDrop.value;
        SaveSystem.SaveSchool();
    }
}
