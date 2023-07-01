using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoost : MonoBehaviour
{
    Score score;
    private void Start()
    {
        score=FindObjectOfType<Score>();
    }
    private void OnTriggerEnter(Collider other)
    {
        score.CurrentNumber += 1000;
    }
}
