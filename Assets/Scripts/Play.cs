using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartGame()
    {
        SceneManager.LoadScene("LoadingScene1");
    }
    public void Prueba()
    {
        SceneManager.LoadScene("SceneTest");
    }
}
