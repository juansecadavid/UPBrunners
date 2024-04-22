using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;
using TMPro;

public class LoadGame : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI highScoreText;
    [SerializeField]
    private GameObject[] skins;
    [SerializeField]
    private string[] names;
    [SerializeField]
    private TextMeshProUGUI name;

    public bool[] skinsDesbloqueadas; // Arreglo de booleanos para el estado de desbloqueo de las skins
    // Start is called before the first frame update
    void Awake()
    {
        //GetUserScore(FirebaseAuth.DefaultInstance.CurrentUser.UserId);
        SaveSystem.LoadVolume();
        SaveSystem.LoadSkin();
        SaveSystem.LoadSchool();
        SaveSystem.LoadAvailableSkin();
        
        skins[GameManager.Skin].SetActive(true);
        name.text = $"{names[GameManager.Skin]}";

        skinsDesbloqueadas = new bool[4]; 

        // Desbloquear la primera skin y bloquear las demás
        skinsDesbloqueadas[0] = true;
        for (int i = 1; i < 4; i++) // Iterar solo 3 veces ya que la primera skin ya está desbloqueada
        {
            skinsDesbloqueadas[i] = false;
        }
        for (int i = 0; i <= GameManager.AvailableSkins; i++)
        {
            skinsDesbloqueadas[i] = true;
        }
    }

    private void Start()
    {
        FirebaseDatabase.DefaultInstance.GetReference($"users/{FirebaseAuth.DefaultInstance.CurrentUser.UserId}/score").ValueChanged += HandleValueChanged;
    }

    private void Update()
    {
        name.text = $"{names[GameManager.Skin]}";
    }

    public void RightSkinBtn()
    {
        if (skinsDesbloqueadas[(GameManager.Skin + 1) % skins.Length]) 
        {
            skins[GameManager.Skin].SetActive(false);
            GameManager.Skin = (GameManager.Skin + 1) % skins.Length; 
            skins[GameManager.Skin].SetActive(true);
        }
    }
    
    public void LeftSkinBtn()
    {
        if (skinsDesbloqueadas[(GameManager.Skin + skins.Length - 1) % skins.Length]) 
        {
            skins[GameManager.Skin].SetActive(false);
            GameManager.Skin = (GameManager.Skin + skins.Length - 1) % skins.Length; 
            skins[GameManager.Skin].SetActive(true);
        }
    }
    private void HandleValueChanged(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }

        GameManager.HighScore = Convert.ToInt32(args.Snapshot.Value);
        highScoreText.text = $"HighScore {GameManager.HighScore}";
        //_label.text = (int)args.Snapshot.Value;
    }
    
    public void GetUserScore(string userId)
    {
        // Acceder al nodo del usuario específico
        DatabaseReference userRef = FirebaseDatabase.DefaultInstance.RootReference.Child("users").Child(userId);

        userRef.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                // Manejar el error, tarea fallida
                Debug.LogError("Error al obtener el score: " + task.Exception);
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.Exists && snapshot.HasChild("score"))
                {
                    // Si el nodo del usuario tiene un hijo llamado "score", lo obtenemos.
                    int userScore = Convert.ToInt32(snapshot.Child("score").Value);
                    GameManager.HighScore = userScore;
                    Debug.Log("El score del usuario con ID " + userId + " es: " + userScore);
                    // Aquí puedes hacer algo con el score, como mostrarlo en la UI.
                }
                else
                {
                    Debug.Log("El usuario no existe o no tiene un score.");
                }
            }
        });
    }
}
