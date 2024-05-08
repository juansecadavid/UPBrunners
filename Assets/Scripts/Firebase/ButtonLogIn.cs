using System.Collections;
using System.Collections.Generic;
using Firebase.Auth;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLogIn : MonoBehaviour
{
    [SerializeField]
    private Button _logInBtn;

    private Coroutine _logInCoroutine;
    
    private void Reset()
    {
        _logInBtn = GetComponent<Button>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _logInBtn.onClick.AddListener(HandleLogInButtonClicked);
    }

    private void HandleLogInButtonClicked()
    {
        string email = GameObject.Find("InputFieldEmail").GetComponent<TMP_InputField>().text;
        string password = GameObject.Find("InputFieldPassword").GetComponent<TMP_InputField>().text;
        var auth = FirebaseAuth.DefaultInstance;
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.Log("Se canceló");
            }
            else if (task.IsFaulted)
            {
                Debug.Log("Encountered an error" + task.Exception);
            }
            Firebase.Auth.AuthResult result = task.Result;
            
            Debug.LogFormat($"Firebase loged in successfully: {result.User.DisplayName}, {result.User.UserId}");

            string name = "";
        });
    }
}
