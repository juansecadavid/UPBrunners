using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseSystem : MonoBehaviour
{
    private bool isPaused=false;
    [SerializeField]
    private GameObject panelPause;
    [SerializeField]
    private Movement mov;
    [SerializeField]
    private UpdateSpped updtSpeed;
    [SerializeField]
    private TextMeshProUGUI pauseTimer;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject canvasPowerBon;
    private SkinSelector skinSelector;
    private Animator animator;
    private Score score;
    private VolumeSlider volume;
    private SoundManager soundManager;
    // Start is called before the first frame update
    void Start()
    {
        skinSelector = FindObjectOfType<SkinSelector>();
        animator = skinSelector.Skins[GameManager.Skin].GetComponent<Animator>();
        score=FindObjectOfType<Score>();
        volume = FindObjectOfType<VolumeSlider>();
        soundManager = FindObjectOfType<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Resume()
    {
        if (isPaused)
        {
            panelPause.SetActive(false);
            //Time.timeScale = 1;
            StartCoroutine(StartAfterPause());
        } 
        else
        {
            isPaused = true;
            GameManager.IsPaused = true;
            if (mov != null)
            {
                mov.enabled = false;
            }
            if(updtSpeed != null)
            {
                updtSpeed.enabled = false;
            }
            animator.enabled = false;
            score.enabled = false;
            panelPause.SetActive(true);
            canvasPowerBon.SetActive(false);

            //Time.timeScale = 0;
        }       
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene("SceneTest");
        Time.timeScale = 1;
        //StartCoroutine(StartCharge());
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("Inicio");
        Time.timeScale=1;   
    }
    IEnumerator StartAfterPause()
    {
        pauseTimer.text = "";
        yield return new WaitForSeconds(0.2f);
        soundManager.PlaySound(6);
        pauseTimer.text = "3";
        yield return new WaitForSeconds(1f);
        pauseTimer.text = "2";
        yield return new WaitForSeconds(1f);
        pauseTimer.text = "1";
        yield return new WaitForSeconds(1f);
        pauseTimer.text = "";
        GameManager.IsPaused = false;
        if(!canvasPowerBon.activeInHierarchy)
        {
            canvasPowerBon.SetActive(true);
        }
        animator.enabled = true;
        score.enabled = true;
        isPaused = false;
        if (mov != null)
        {
            mov.enabled = true;
        }
        if (updtSpeed != null)
        {
            updtSpeed.enabled = true;
        }
        volume.PlayMusic();
    }
    /*IEnumerator StartCharge()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("SceneTest");
        operation.allowSceneActivation = false;
        while (!operation.isDone)
        {
            if (operation.progress >= 0.9f)
            {
                yield return new WaitForSeconds(1f);
                operation.allowSceneActivation = true;
            }
            yield return null;
        }
    }*/
}
