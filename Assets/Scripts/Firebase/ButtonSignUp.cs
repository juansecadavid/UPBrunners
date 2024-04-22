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

    public AuthErrorMessage _authErrorMessage;
    private void Reset()
    {
        _registerBtn = GetComponent<Button>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _registerBtn.onClick.AddListener(HandleRegisterButtonClicked);
        mDataBaseRef = FirebaseDatabase.DefaultInstance.RootReference;
        //_authErrorMessage = FindAnyObjectByType<AuthErrorMessage>();
    }

    

    private void HandleRegisterButtonClicked()
    {
        string email = GameObject.Find("InputFieldEmail").GetComponent<TMP_InputField>().text;
        string password = GameObject.Find("InputFieldPassword").GetComponent<TMP_InputField>().text;
        if (GameObject.Find("InputFieldUsername").GetComponent<TMP_InputField>().text != ""&& GameObject.Find("InputFieldUsername").GetComponent<TMP_InputField>().text.Length>5)
        {
            StartCoroutine(RegisterUser(email, password));
        }
        else if(GameObject.Find("InputFieldUsername").GetComponent<TMP_InputField>().text.Length>5)
        {
            _authErrorMessage.ShowErrorMessage("El username debe contener mas de 5 letras");
        }
        else
        {
            _authErrorMessage.ShowErrorMessage("Debes poner un username");
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
            FirebaseException firebaseEx = registerTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Encountered an error: " + registerTask.Exception;
            if (errorCode == AuthError.EmailAlreadyInUse)
            {
                _authErrorMessage.ShowErrorMessage("El correo electrónico ya está en uso");
            }
            else if (errorCode == AuthError.WeakPassword)
            {
                // Este es el código de error cuando la contraseña es demasiado débil/shorta
                _authErrorMessage.ShowErrorMessage("La contraseña es demasiado débil o corta");
            }
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
