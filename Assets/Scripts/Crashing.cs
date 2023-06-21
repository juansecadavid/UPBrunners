using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crashing : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Movement mov = other.GetComponentInParent<Movement>();
            mov.enabled = false;
            //GameManager.FailedAttemp();
            Score score = other.GetComponentInParent<Score>();
            score.enabled = false;
            if(score.CurrentNumber >=GameManager.HighScore)
            {
                GameManager.HighScore = score.CurrentNumber;
                SaveSystem.SaveGame();
            }
        }
    }
}
