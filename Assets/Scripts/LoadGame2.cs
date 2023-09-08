using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadGame2 : MonoBehaviour
{
    float contador = 0;
    bool startGame=false;
    [SerializeField]
    GameObject startGameBtn;
    [SerializeField]
    TextMeshProUGUI prueb;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartCharge());
    }

    // Update is called once per frame
    void Update()
    {
        contador += Time.deltaTime;
        prueb.text = contador.ToString();
    }
    IEnumerator StartCharge()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("SceneTest");
        operation.allowSceneActivation = false;
        while (!operation.isDone)
        {
            if (operation.progress >= 0.9f)
            {
                /*if (contador > 3f)
                {
                    startGameBtn.SetActive(true);
                }*/
                startGameBtn.SetActive(true);
                if (startGame)
                {
                    operation.allowSceneActivation = true;
                }
            }
            yield return null;
        }
    }
     public void Play()
    {
        startGame = true;
    }
}
