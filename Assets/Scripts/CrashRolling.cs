using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashRolling : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Movement mov= other.GetComponentInParent<Movement>();
            Score score = other.GetComponentInParent<Score>();
            if (mov.Rolling==false)
            {
                mov.enabled = false;
                score.enabled = false;
                if (score.CurrentNumber >= GameManager.HighScore)
                {
                    GameManager.HighScore = score.CurrentNumber;
                    SaveSystem.SaveGame();
                }
            }
        }
    }
}
