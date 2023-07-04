using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoost : MonoBehaviour
{
    Score score;
    [SerializeField]
    private int boost;
    private void Start()
    {
        score=FindObjectOfType<Score>();
    }
    private void OnTriggerEnter(Collider other)
    {
        score.CurrentNumber += boost;
    }
    public void Boost()
    {
        score.CurrentNumber += boost;
    }
}
