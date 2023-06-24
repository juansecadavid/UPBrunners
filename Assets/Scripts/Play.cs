using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    // Start is called before the first frame update
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
}
