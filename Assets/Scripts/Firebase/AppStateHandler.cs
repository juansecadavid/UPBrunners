using System.Collections;
using System.Collections.Generic;
using Firebase.Auth;
using Firebase.Database;
using UnityEngine;

public class AppStateHandler : MonoBehaviour
{
    // Asegúrate de que este script persista entre escenas si es necesario.
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (FirebaseAuth.DefaultInstance.CurrentUser != null)
        {
            string userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
            DatabaseReference userOnlineRef = FirebaseDatabase.DefaultInstance.GetReference("users/" + userId + "/online");

            if (pauseStatus)
            {
                // La aplicación ha sido pausada (puede ser al salir o ir al fondo)
                userOnlineRef.SetValueAsync(false);
            }
            else
            {
                // La aplicación ha sido reanudada
                userOnlineRef.SetValueAsync(true);
            }
        }
    }

    void OnApplicationQuit()
    {
        if (FirebaseAuth.DefaultInstance.CurrentUser != null)
        {
            string userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
            DatabaseReference userOnlineRef = FirebaseDatabase.DefaultInstance.GetReference("users/" + userId + "/online");

            // La aplicación está a punto de cerrarse
            userOnlineRef.SetValueAsync(false);
        }
    }
}
