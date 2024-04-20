using System.Collections;
using System.Collections.Generic;
using Firebase.Auth;
using Firebase.Database;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonChangePassword : MonoBehaviour
{
    [SerializeField]
    private Button _changePassBtn;

    private DatabaseReference mDataBaseRef;
    
    private void Reset()
    {
        _changePassBtn = GetComponent<Button>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _changePassBtn.onClick.AddListener(HandleChangePassButtonClicked);
        mDataBaseRef = FirebaseDatabase.DefaultInstance.RootReference;
    }

    

    private void HandleChangePassButtonClicked()
    {
        string email = GameObject.Find("InputFieldEmail").GetComponent<TMP_InputField>().text;
        StartCoroutine(ChangePassword(email));
    }

    IEnumerator ChangePassword(string email)
    {
        var auth = FirebaseAuth.DefaultInstance;
        var registerTask = auth.SendPasswordResetEmailAsync(email);
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


            Debug.LogFormat($"Se envió e correo para cambio dde contraseña");

        }
    }
}
