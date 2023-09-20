//using System;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewHishscore : MonoBehaviour
{
    Action newHighscore;
    Score score;
    MessagesOnPlay messages;
    // Start is called before the first frame update
    void Awake()
    {
        messages=FindObjectOfType<MessagesOnPlay>();
        newHighscore = new Action(NewHighScore);
        score=FindObjectOfType<Score>();
    }

    // Update is called once per frame
    void Update()
    {
        if(newHighscore != null&&score.CurrentNumber>GameManager.HighScore)
        {
            newHighscore();
            newHighscore=null;
        }
    }

    void NewHighScore()
    {
        messages.ShowMessage("¡New Highscore!", 3f);
    }
}
