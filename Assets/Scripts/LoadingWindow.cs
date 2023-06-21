using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class LoadingWindow : MonoBehaviour
{
    VideoSource source;
    VideoPlayer player;
    private float contador = 0f;
    [SerializeField]
    private VideoClip[] videoClips;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<VideoPlayer>();
        player.clip = videoClips[Random.Range(0,videoClips.Length)];
        Debug.Log($"{videoClips.Length}");
        player.Play();
        StartCoroutine(StartCharge());
    }

    // Update is called once per frame
    void Update()
    {
        contador+=Time.deltaTime;
    }
    IEnumerator StartCharge()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("SceneTest");
        operation.allowSceneActivation = false;
        while(!operation.isDone)
        {
            if(operation.progress>=0.9f)
            {
                if(contador>player.clip.length+2f)
                {
                    operation.allowSceneActivation=true;
                }
            }
            yield return null;
        }
    }
}
