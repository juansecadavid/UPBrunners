using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashRolling : MonoBehaviour
{
    SoundManager soundManager;
    private void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            StartCoroutine(Lose(other));
        }
    }
    IEnumerator Lose(Collider other)
    {
        Movement mov = other.GetComponentInParent<Movement>();
        UpdateSpped upd = other.GetComponentInParent<UpdateSpped>();
        Score score = other.GetComponentInParent<Score>();
        if (mov.Rolling == false)
        {
            soundManager.PlaySound(2);
            mov.HasLost();
            mov.Rolling = false;
            mov.Saltando = false;
            mov.enabled = false;
            score.enabled = false;
            upd.enabled = false;         
            yield return new WaitForSeconds(1.5f);
            VolumeSlider volume = FindObjectOfType<VolumeSlider>();
            volume.StopMusic();
            if (score.CurrentNumber >= GameManager.HighScore)
            {
                GameManager.HighScore = score.CurrentNumber;
                SaveSystem.SaveGame();
            }
            LevelManager levelMan = FindObjectOfType<LevelManager>();
            levelMan.Lose();
        }
    }
}
