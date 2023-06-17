using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseSystem : MonoBehaviour
{
    private bool isPaused=false;
    [SerializeField]
    private GameObject panelPause;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Resume()
    {
        if (isPaused)
        {
            isPaused = false;
            panelPause.SetActive(false);
            Time.timeScale = 1;
        } 
        else
        {
            isPaused = true;
            panelPause.SetActive(true);
            Time.timeScale = 0;
        }       
    }
    public void RestartLevel()
    {
        Scene active = SceneManager.GetActiveScene();
        SceneManager.LoadScene(active.buildIndex);
        Time.timeScale = 1;
    }

}
