using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;

public class Crashing : MonoBehaviour
{
    SoundManager soundManager;
    private void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            soundManager.PlaySound(2);
            StartCoroutine(Lose(other));
        }
    }
    IEnumerator Lose(Collider other)
    {
        Movement mov = other.GetComponentInParent<Movement>();
        UpdateSpped upd = other.GetComponentInParent<UpdateSpped>();
        Score score = other.GetComponentInParent<Score>();
        mov.HasLost();
        mov.enabled = false;
        score.enabled = false;
        upd.enabled = false;
        yield return new WaitForSeconds(1.5f);
        VolumeSlider volume = FindObjectOfType<VolumeSlider>();
        volume.StopMusic();
        if (score.CurrentNumber >= GameManager.HighScore)
        {
            GameManager.HighScore = score.CurrentNumber;
            //SaveSystem.SaveGame();
            UpdateScoreIfHigher(FirebaseAuth.DefaultInstance.CurrentUser.UserId,score.CurrentNumber);
        }
        LevelManager levelMan = FindObjectOfType<LevelManager>();
        levelMan.Lose();
    }
    public void UpdateScoreIfHigher(string userId, int newScore)
    {
        DatabaseReference userRef = FirebaseDatabase.DefaultInstance.GetReference("users").Child(userId);

        userRef.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError(task.Exception);
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
            
                if (snapshot.Exists && snapshot.HasChild("score"))
                {
                    int currentScore = Convert.ToInt32(snapshot.Child("score").Value);
                
                    if (newScore > currentScore)
                    {
                        userRef.Child("score").SetValueAsync(newScore).ContinueWith(updateTask =>
                        {
                            if (updateTask.IsFaulted)
                            {
                                Debug.LogError(updateTask.Exception);
                            }
                            else if (updateTask.IsCompleted)
                            {
                                Debug.Log("Score actualizado correctamente.");
                            }
                        });
                    }
                    else
                    {
                        // El nuevo score no es mayor, no se actualiza
                        Debug.Log("El nuevo score no es más alto que el score actual. No se realiza la actualización.");
                    }
                }
            }
        });
    }
}
