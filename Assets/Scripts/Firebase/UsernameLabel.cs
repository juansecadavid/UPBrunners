using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using TMPro;
using UnityEngine;

public class UsernameLabel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI userText;
    // Start is called before the first frame update
    void Reset()
    {
        userText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Start()
    {
        FirebaseAuth.DefaultInstance.StateChanged += HandleAuthChanged;
    }

    void HandleAuthChanged(object sender, EventArgs e)
    {
        Debug.Log("Me llamé");
        var currentUser = FirebaseAuth.DefaultInstance.CurrentUser;
        if (currentUser != null)
        {
            FirebaseDatabase.DefaultInstance.GetReference($"users/{currentUser.UserId}/username").GetValueAsync().ContinueWithOnMainThread(
                task =>
                {
                    if (task.IsFaulted)
                    {
                        Debug.Log("Fallé");
                    }
                    else if (task.IsCompleted)
                    {
                        DataSnapshot snapshot = task.Result;

                        userText.text = (string)snapshot.Value;
                        Debug.Log("Se supone que lo logré");
                    }
                } );
        }
    }
}
