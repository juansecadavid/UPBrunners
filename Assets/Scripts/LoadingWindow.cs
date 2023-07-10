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
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<VideoPlayer>();
        player.clip = videoClips[Random.Range(0, videoClips.Length)];
        Debug.Log($"{videoClips.Length}");
        player.Play();
        slider.maxValue = (float)player.clip.length;
        StartCoroutine(StartCharge());
    }

    // Update is called once per frame
    void Update()
    {
        contador += Time.deltaTime;
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
                if (contador > player.clip.length + 2f)
                {
                    operation.allowSceneActivation = true;
                }
            }
            yield return null;
        }
    }
}
