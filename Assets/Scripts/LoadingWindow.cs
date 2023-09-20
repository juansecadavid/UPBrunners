using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;

public class LoadingWindow : MonoBehaviour
{
    VideoSource source;
    VideoPlayer player;
    private float contador = 0f;
    [SerializeField]
    private VideoClip[] videoClips;
    [SerializeField]
    private Slider slider;
    bool startGame=false;
    [SerializeField]
    GameObject StartGameBtn;
    // Start is called before the first frame update
    private void Awake()
    {
        player = GetComponent<VideoPlayer>();
        player.clip = videoClips[Random.Range(0, videoClips.Length)];
        Debug.Log($"{videoClips.Length}");
    }
    void Start()
    {
        player.Play();
        slider.maxValue = (float)player.clip.length;
        StartCoroutine(StartCharge());
    }

    // Update is called once per frame
    void Update()
    {
        contador = (float)player.time;
        //contador += Time.deltaTime;
        slider.value = contador;
    }
    IEnumerator StartCharge()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("SceneTest");
        operation.allowSceneActivation = false;
        while (!operation.isDone)
        {
            if (operation.progress >= 0.9f)
            {
                if(contador>3f)
                {
                    StartGameBtn.SetActive(true);
                }
                if (contador >= player.clip.length-0.1f||startGame)
                {
                    operation.allowSceneActivation = true;
                }
            }
            yield return null;
        }
    }
    public void StartGame()
    {
        startGame = true;
    }
}
