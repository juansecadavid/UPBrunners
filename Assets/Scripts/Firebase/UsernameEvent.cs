using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Auth;
using Firebase.Database;
using TMPro;
using UnityEngine;

public class UsernameEvent : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _label;
    // Start is called before the first frame update
    void Start()
    {
        FirebaseDatabase.DefaultInstance.GetReference($"users/{FirebaseAuth.DefaultInstance.CurrentUser.UserId}/username").ValueChanged += HandleValueChanged;
    }

    private void HandleValueChanged(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }
        _label.text = (string)args.Snapshot.Value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
