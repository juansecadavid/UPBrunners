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
    public float panelConfigPs;
    [SerializeField]
    private GameObject panelGeneral;
    [SerializeField]
    private GameObject panelSkin;
    [SerializeField]
    private GameObject panelCredits;
    private bool configPanel=false;
    private bool configSkin=false;
    [SerializeField]
    private TMP_Dropdown schoolDrop;
    private bool startGame=false;
    // Start is called before the first frame update
    private void Start()
    {
        schoolDrop.value = GameManager.School;
        panelConfigPs = panelConfig.GetComponent<RectTransform>().position.y;
        LeanTween.moveY(panelGeneral.GetComponent<RectTransform>(), 0, 2f).setDelay(0.2f)
            .setEase(LeanTweenType.easeOutElastic);
        StartCoroutine(PreCharge());
    }
    public void StartGame()
    {
        //SceneManager.LoadScene("LoadingScene1");
        startGame = true;
    }
    public void Prueba()
    {
        SceneManager.LoadScene("SceneTest");
    }
    public void ConfigPanel()
    {
        if (configPanel)
        {
            LeanTween.alpha(panelConfig.GetComponent<RectTransform>(), 0f, 0.2f);
            LeanTween.moveY(panelConfig.GetComponent<RectTransform>(), -715, 0.2f).setOnComplete(DesactivatePanel);               
        }
        else
        {
            panelConfig.SetActive(true);
            configPanel = true;
            LeanTween.alpha(panelConfig.GetComponent<RectTransform>(), 1, 0.2f);
            LeanTween.moveY(panelConfig.GetComponent<RectTransform>(), 37, 0.2f);
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
            LeanTween.moveY(panelGeneral.GetComponent<RectTransform>(), 500, 0f);
            LeanTween.moveY(panelGeneral.GetComponent<RectTransform>(), 0, 2f).setDelay(0.2f)
            .setEase(LeanTweenType.easeOutElastic);
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
    public void DesactivatePanel()
    {
        panelConfig.SetActive(false);
        configPanel = false;
    }
    public void PanelCredits()
    {
        if (panelCredits.activeInHierarchy)
        {

            LeanTween.moveY(panelCredits.GetComponent<RectTransform>(), 900, 0.2f).setOnComplete(DesactivateCredits);
            LeanTween.alpha(panelCredits.GetComponent<RectTransform>(), 0, 0.2f);
        }
        else
        {
            panelCredits.SetActive(true);
            LeanTween.moveY(panelCredits.GetComponent<RectTransform>(), 0, 0.2f);
            LeanTween.alpha(panelCredits.GetComponent<RectTransform>(), 1, 0.2f);
        }
    }
    private void DesactivateCredits()
    {
        panelCredits.SetActive(false);
    }
    IEnumerator PreCharge()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("LoadingScene1");
        operation.allowSceneActivation = false;
        while (!operation.isDone)
        {
            if (operation.progress >= 0.9f)
            {        
                if (startGame)
                {
                    operation.allowSceneActivation = true;
                }
            }
            yield return null;
        }
    }
    public void OpenUPBpage()
    {
        Application.OpenURL("https://www.upb.edu.co/es/home");
    }
}
