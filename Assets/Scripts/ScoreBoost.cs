using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoost : MonoBehaviour
{
    Score score;
    [SerializeField]
    private int boost;
    MessagesOnPlay messagesOnPlay;
    private void Start()
    {
        score=FindObjectOfType<Score>();
        messagesOnPlay=FindAnyObjectByType<MessagesOnPlay>();
    }
    private void OnTriggerEnter(Collider other)
    {
        score.CurrentNumber += boost;
        string message =$"+ {boost}!";
        messagesOnPlay.ShowMessage(message, 3f);
    }
    public void Boost()
    {
        score.CurrentNumber += boost;
    }
}
