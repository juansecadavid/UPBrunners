using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Firebase.Database;
using Firebase.Extensions;
using TMPro;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] best5Scores;
    private int currentIndex=0;

    public void GetUsersHighScore()
    {
        FirebaseDatabase.DefaultInstance.GetReference("users").OrderByChild("score").GetValueAsync()
            .ContinueWithOnMainThread(
                task =>
                {
                    if (task.IsFaulted)
                    {
                        
                    }
                    else if (task.IsCompleted)
                    {
                        DataSnapshot snapshot = task.Result;
                        
                        var sortedUsers = new SortedDictionary<int, string>();
                        
                        foreach (DataSnapshot childSnapshot in snapshot.Children)
                        {
                            string username = childSnapshot.Child("username").Value.ToString();
                            int score = int.Parse(childSnapshot.Child("score").Value.ToString());
                            sortedUsers[score] = username;
                        }
                        //panel.gameObject.SetActive(true);
                        foreach (var kvp in sortedUsers.Reverse())
                        {
                            Debug.Log($"{kvp.Value} y mi score es {kvp.Key}");
                            if (currentIndex < 5)
                            {
                                best5Scores[currentIndex].text = $"{kvp.Value}: {kvp.Key}\"";
                                currentIndex++;
                            }
                        }
                    }
                });
    }
}
