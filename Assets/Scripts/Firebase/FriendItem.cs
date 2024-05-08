using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Database;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FriendItem : MonoBehaviour
{
    private FirebaseManager _firebaseManager;
    public TextMeshProUGUI usernameText;
    public TextMeshProUGUI scoreText;
    public Image image;
    public string userId;
    private ShowOnlineNotification _showOnlineNotification;

    public void SetUp(string username, string Id, GameObject firebaseM)
    {
        usernameText.text = username;
        userId = Id;
        _firebaseManager = firebaseM.GetComponent<FirebaseManager>();
    }

    private void Start()
    {
        _showOnlineNotification = GameObject.Find("OnlineNotification").GetComponent<ShowOnlineNotification>();
        FirebaseDatabase.DefaultInstance.GetReference("users/" + userId + "/online").ValueChanged += HandleOnlineStatusChanged;
        FirebaseDatabase.DefaultInstance.GetReference("users/" + userId + "/score").ValueChanged += HandleScoreChange;
    }
    
    void HandleOnlineStatusChanged(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.Log(args.DatabaseError.Message);
            return;
        }

        // args.Snapshot.Key será el ID del usuario amigo
        // args.Snapshot.Value será el estado en línea (true o false)
        bool isOnline = (bool)args.Snapshot.Value;
        string friendId = args.Snapshot.Key;

        
        if (isOnline)
        {
            if(image!=null)
                image.color=Color.green;
            if (_showOnlineNotification != null)
            {
                _showOnlineNotification.ShowNotification(usernameText.text);
                Debug.Log("Obvio encontré el objeto");
            }
            else
            {
                Debug.Log("No encontré ese objeto");
            }
                
        }
        else
        {
            if(image!=null)
                image.color=Color.red;
        }
        Debug.Log("No tiré error");
    }

    void HandleScoreChange(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.Log(args.DatabaseError.Message);
            return;
        }
        
        int score = Convert.ToInt32(args.Snapshot.Value);
        string friendId = args.Snapshot.Key;

        if (scoreText != null)
            scoreText.text = $"{score}";
    }
    private void OnDestroy()
    {
        FirebaseDatabase.DefaultInstance.GetReference("users/" + userId + "/online").ValueChanged -= HandleOnlineStatusChanged;
        FirebaseDatabase.DefaultInstance.GetReference("users/" + userId + "/score").ValueChanged -= HandleScoreChange;
    }
}
