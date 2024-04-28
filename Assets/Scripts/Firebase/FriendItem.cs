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
    public Image image;
    private string userId;

    public void SetUp(string username, string userId, GameObject firebaseM)
    {
        usernameText.text = username;
        this.userId = userId;
        _firebaseManager = firebaseM.GetComponent<FirebaseManager>();
    }

    private void Start()
    {
        ListeningToFriendStatus();
    }

    void ListeningToFriendStatus()
    {
        DatabaseReference onlineRef = FirebaseDatabase.DefaultInstance.GetReference("users/" + userId + "/online");
        onlineRef.ValueChanged += HandleOnlineStatusChanged;
    }
    void HandleOnlineStatusChanged(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }

        // args.Snapshot.Key será el ID del usuario amigo
        // args.Snapshot.Value será el estado en línea (true o false)
        bool isOnline = (bool)args.Snapshot.Value;
        string friendId = args.Snapshot.Key;

        if (isOnline)
        {
            image.color=Color.green;
        }
        else
        {
            image.color=Color.red;
        }
    }

    private void OnDestroy()
    {
        DatabaseReference onlineRef = FirebaseDatabase.DefaultInstance.GetReference("users/" + userId + "/online");
        onlineRef.ValueChanged -= HandleOnlineStatusChanged;
    }
}
