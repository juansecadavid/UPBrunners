using System;
using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSignUp : MonoBehaviour
{
    [SerializeField]
    private Button _registerBtn;

    private DatabaseReference mDataBaseRef;
    
    private void Reset()
    {
        _registerBtn = GetComponent<Button>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _registerBtn.onClick.AddListener(HandleRegisterButtonClicked);
        mDataBaseRef = FirebaseDatabase.DefaultInstance.RootReference;
    }

    

    private void HandleRegisterButtonClicked()
    {
        string email = GameObject.Find("InputFieldEmail").GetComponent<TMP_InputField>().text;
        string password = GameObject.Find("InputFieldPassword").GetComponent<TMP_InputField>().text;
        if (GameObject.Find("InputFieldUsername").GetComponent<TMP_InputField>().text != "")
        {
            StartCoroutine(RegisterUser(email, password));
        }
    }

    IEnumerator RegisterUser(string email, string password)
    {
        var auth = FirebaseAuth.DefaultInstance;
        var registerTask = auth.CreateUserWithEmailAndPasswordAsync(email, password);
        yield return new WaitUntil(()=>registerTask.IsCompleted);
        if (registerTask.IsCanceled)
        {
            Debug.Log("Se canceló");
        }
        else if (registerTask.IsFaulted)
        {
            Debug.Log("Encountered an error" + registerTask.Exception);
        }
        else
        {
            AuthResult result = registerTask.Result;
            
            Debug.LogFormat($"Firebase created successfully: {result.User.DisplayName}, {result.User.UserId}");
            string name = GameObject.Find("InputFieldUsername").GetComponent<TMP_InputField>().text;
            mDataBaseRef.Child("users").Child(result.User.UserId).Child("username").SetValueAsync(name);
            mDataBaseRef.Child("users").Child(result.User.UserId).Child("score").SetValueAsync(0);
        }
    }
}
