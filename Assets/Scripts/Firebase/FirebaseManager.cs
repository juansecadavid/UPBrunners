using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Auth;
using Firebase.Database;
using TMPro;
using UnityEngine;

public class FirebaseManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI AddFriendError;
    [SerializeField] private TMP_InputField addFriendOtherId;
    [SerializeField] private GameObject acceptFriendsContent;
    [SerializeField] private GameObject acceptFriendsPrefab;
    [SerializeField] private GameObject friendsContent;
    [SerializeField] private GameObject frindsPrefab;
    private List<string> acceptFriendList = new List<string>();
    private List<string> acceptFriendUserIdList = new List<string>();
    private List<string> frindsList = new List<string>();
    private List<string> frindsUserIdList = new List<string>();
    private bool userNotFound = false;
    private bool invitationSent = false;
    private bool acceptFriendUi = false;
    private bool firebaseReady=false;
    private bool friendUi = false;
    
    private void Start()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available) {
                // Firebase is ready for use
                firebaseReady = true;
            } else {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }

    public void CopyTextToClipboard()
    {
        GUIUtility.systemCopyBuffer = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
    }

    public void FetchFriends()
    {
        FirebaseDatabase.DefaultInstance.GetReference("users/" + FirebaseAuth.DefaultInstance.CurrentUser.UserId + "/friends").ValueChanged += FriendsHandleValueChanged;
    }
    public void FetchFriendRequests()
    {
        FirebaseDatabase.DefaultInstance.GetReference("users/" + FirebaseAuth.DefaultInstance.CurrentUser.UserId + "/friendRequests/received").ValueChanged += AcceptFriendsHandleValueChanged;
    }
    void FriendsHandleValueChanged(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.Log(args.DatabaseError.Message);
            return;
        }
        if (args.Snapshot.Exists)
        {
            frindsList.Clear();
            frindsUserIdList.Clear();
            int pendingFetchCount = (int)args.Snapshot.ChildrenCount; // Contar cuántas solicitudes hay para manejar
            if (pendingFetchCount == 0)
            {
                acceptFriendUi = true; // Si no hay solicitudes, activar UI inmediatamente
            }
            else
            {
                foreach (DataSnapshot childSnapshot in args.Snapshot.Children)
                {
                    Debug.Log("Llamé manualmente");
                    string senderId = childSnapshot.Key;
                    FetchUsernameFriends(senderId, () => {
                        // Decrementar el contador cada vez que una solicitud se completa
                        pendingFetchCount--;
                        if (pendingFetchCount <= 0)
                        {
                            friendUi = true; // Activar UI solo cuando todas las fetches están completas
                        }
                    });
                }
            }
        }
        else
        {
            Debug.Log("No hay solicitudes de amistad pendientes.");
        }
    }
    public void FetchFriendManually()
    {
        DatabaseReference requestsRef = FirebaseDatabase.DefaultInstance.GetReference("users/" + FirebaseAuth.DefaultInstance.CurrentUser.UserId + "/friends");
        requestsRef.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Error accessing database: " + task.Exception);
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    acceptFriendList.Clear();
                    acceptFriendUserIdList.Clear();
                    int pendingFetchCount = (int)snapshot.ChildrenCount; // Contar cuántas solicitudes hay para manejar
                    if (pendingFetchCount == 0)
                    {
                        acceptFriendUi = true; // Si no hay solicitudes, activar UI inmediatamente
                    }
                    else
                    {
                        foreach (DataSnapshot childSnapshot in snapshot.Children)
                        {
                            Debug.Log("Llamé manualmente");
                            string senderId = childSnapshot.Key;
                            FetchUsernameFriends(senderId, () => {
                                // Decrementar el contador cada vez que una solicitud se completa
                                pendingFetchCount--;
                                if (pendingFetchCount <= 0)
                                {
                                    friendUi = true; // Activar UI solo cuando todas las fetches están completas
                                }
                            });
                        }
                    }
                }
                else
                {
                    Debug.Log("No friend requests.");
                    //acceptFriendUi = true; // No hay solicitudes, activar UI
                }
            }
        });
    }
    void AcceptFriendsHandleValueChanged(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.Log(args.DatabaseError.Message);
            return;
        }
        if (args.Snapshot.Exists)
        {
            acceptFriendList.Clear();
            acceptFriendUserIdList.Clear();
            int pendingFetchCount = (int)args.Snapshot.ChildrenCount; // Contar cuántas solicitudes hay para manejar
            if (pendingFetchCount == 0)
            {
                acceptFriendUi = true; // Si no hay solicitudes, activar UI inmediatamente
            }
            else
            {
                foreach (DataSnapshot childSnapshot in args.Snapshot.Children)
                {
                    Debug.Log("Llamé manualmente");
                    string senderId = childSnapshot.Key;
                    FetchUsernameFriendsRequest(senderId, () => {
                        // Decrementar el contador cada vez que una solicitud se completa
                        pendingFetchCount--;
                        if (pendingFetchCount <= 0)
                        {
                            acceptFriendUi = true; // Activar UI solo cuando todas las fetches están completas
                        }
                    });
                }
            }
        }
        else
        {
            Debug.Log("No hay solicitudes de amistad pendientes.");
        }
    }
    public void FetchFriendRequestsManually()
    {
        DatabaseReference requestsRef = FirebaseDatabase.DefaultInstance.GetReference("users/" + FirebaseAuth.DefaultInstance.CurrentUser.UserId + "/friendRequests/received");
        requestsRef.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Error accessing database: " + task.Exception);
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    acceptFriendList.Clear();
                    acceptFriendUserIdList.Clear();
                    int pendingFetchCount = (int)snapshot.ChildrenCount; // Contar cuántas solicitudes hay para manejar
                    if (pendingFetchCount == 0)
                    {
                        acceptFriendUi = true; // Si no hay solicitudes, activar UI inmediatamente
                    }
                    else
                    {
                        foreach (DataSnapshot childSnapshot in snapshot.Children)
                        {
                            Debug.Log("Llamé manualmente");
                            string senderId = childSnapshot.Key;
                            FetchUsernameFriendsRequest(senderId, () => {
                                // Decrementar el contador cada vez que una solicitud se completa
                                pendingFetchCount--;
                                if (pendingFetchCount <= 0)
                                {
                                    acceptFriendUi = true; // Activar UI solo cuando todas las fetches están completas
                                }
                            });
                        }
                    }
                }
                else
                {
                    Debug.Log("No friend requests.");
                    //acceptFriendUi = true; // No hay solicitudes, activar UI
                }
            }
        });
    }
    public void SendFriendRequest()
    {
        if (addFriendOtherId.text != "")
        {
            if (addFriendOtherId.text!=FirebaseAuth.DefaultInstance.CurrentUser.UserId)
            {
                // Primero verificar si el usuario receptor existe en la base de datos
                DatabaseReference userRef = FirebaseDatabase.DefaultInstance.GetReference("users/" + addFriendOtherId.text);
                userRef.GetValueAsync().ContinueWith(task => {
                    if (task.IsFaulted)
                    {
                        // Manejar errores de conexión o de consulta
                        Debug.LogError("Error al intentar acceder a la base de datos.");
                    }
                    else if (task.IsCompleted)
                    {
                        DataSnapshot snapshot = task.Result;
                        if (!snapshot.Exists)
                        {
                            userNotFound = true;
                        }
                        else
                        {
                            // El usuario receptor existe, proceder a enviar la solicitud de amistad
                            FirebaseDatabase.DefaultInstance.GetReference("users/" + addFriendOtherId.text + "/friendRequests/received").Child(FirebaseAuth.DefaultInstance.CurrentUser.UserId).SetValueAsync(true);
                            FirebaseDatabase.DefaultInstance.GetReference("users/" + FirebaseAuth.DefaultInstance.CurrentUser.UserId + "/friendRequests/sent").Child(addFriendOtherId.text).SetValueAsync(true);
                            Debug.Log("Solicitud de amistad enviada correctamente.");
                            invitationSent = true;
                        }
                    }
                });
            }
            else
            {
                StopAllCoroutines();
                AddFriendError.text = "Debes poner un user id que no sea el tuyo";
                StartCoroutine(DisableError(AddFriendError));
            }
        }
        else
        {
            StopAllCoroutines();
            AddFriendError.text = "Debes poner un user id";
            StartCoroutine(DisableError(AddFriendError));
        }
        
    }

    void FetchUsernameFriendsRequest(string userId, Action onComplete)
    {
        DatabaseReference usernameRef = FirebaseDatabase.DefaultInstance.GetReference("users/" + userId + "/username");
        usernameRef.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Error al acceder a la información del usuario.");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.Exists && snapshot.Value != null)
                {
                    string username = snapshot.Value.ToString();
                    Debug.Log(username+"Hola");
                    acceptFriendList.Add(username);
                    acceptFriendUserIdList.Add(userId);
                }
            }

            onComplete();
        });
    }
    void FetchUsernameFriends(string userId, Action onComplete)
    {
        DatabaseReference usernameRef = FirebaseDatabase.DefaultInstance.GetReference("users/" + userId + "/username");
        usernameRef.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Error al acceder a la información del usuario.");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.Exists && snapshot.Value != null)
                {
                    string username = snapshot.Value.ToString();
                    Debug.Log(username+"Hola");
                    frindsList.Add(username);
                    frindsUserIdList.Add(userId);
                    Debug.Log("La lista tiene" + frindsList.Count);
                    
                }
            }

            onComplete();
        });
    }

    public void AcceptFriendRequest(string friendId)
    {
        // Agregar a la lista de amigos
        FirebaseDatabase.DefaultInstance.GetReference("users/" + FirebaseAuth.DefaultInstance.CurrentUser.UserId + "/friends").Child(friendId).SetValueAsync(true);
        FirebaseDatabase.DefaultInstance.GetReference("users/" + friendId + "/friends").Child(FirebaseAuth.DefaultInstance.CurrentUser.UserId).SetValueAsync(true);

        // Limpiar solicitudes
        ClearFriendRequestData(FirebaseAuth.DefaultInstance.CurrentUser.UserId, friendId);
    }

    public void RejectFriendRequest( string friendId)
    {
        // Solo limpiar solicitudes
        ClearFriendRequestData(FirebaseAuth.DefaultInstance.CurrentUser.UserId, friendId);
    }
    
    void ClearFriendRequestData(string myId, string friendId)
    {
        FirebaseDatabase.DefaultInstance.GetReference("users/" + myId + "/friendRequests/received").Child(friendId).RemoveValueAsync();
        FirebaseDatabase.DefaultInstance.GetReference("users/" + friendId + "/friendRequests/sent").Child(myId).RemoveValueAsync();
    }
    private void Update()
    {
        if (firebaseReady)
        {
            firebaseReady = false;
            FetchFriendRequests();
            FetchFriends();
            DatabaseReference presenceRef = FirebaseDatabase.DefaultInstance.GetReference("users/" + FirebaseAuth.DefaultInstance.CurrentUser.UserId + "/online");
            presenceRef.SetValueAsync(true);
        }
        if (userNotFound)
        {
            userNotFound = false;
            StopAllCoroutines();
            AddFriendError.text = "El user id es incorrecto, verificalo";
            StartCoroutine(DisableError(AddFriendError));
        }

        if (invitationSent)
        {
            invitationSent = false;
            StopAllCoroutines();
            AddFriendError.text = "Invitación enviada correctamente";
            StartCoroutine(DisableError(AddFriendError));
        }

        if (acceptFriendUi)
        {
            Debug.Log("Empecé a hacer la magia");
            if (acceptFriendsContent.transform.childCount!=0)
            {
                for (int i = acceptFriendsContent.transform.childCount - 1; i >= 0; i--)
                {
                    Debug.Log("Tocó Borrar");
                    GameObject child = acceptFriendsContent.transform.GetChild(i).gameObject;
                    Destroy(child);
                }
            }
            acceptFriendUi = false;
            RectTransform parentRectTransform = acceptFriendsContent.GetComponent<RectTransform>();
            int j = 0;
            foreach (string username in acceptFriendList)
            {
                Debug.Log("Agregué");
                //acceptFriendsPrefab.GetComponentInChildren<TextMeshProUGUI>().text = username;
                acceptFriendsPrefab.GetComponent<FriendRequestItem>().Setup(username,acceptFriendUserIdList[j],gameObject);
                GameObject newCopy = GameObject.Instantiate(acceptFriendsPrefab);
                newCopy.GetComponent<RectTransform>().SetParent(parentRectTransform,false);
                j++;
            }
        }

        if (friendUi)
        {
            if (friendsContent.transform.childCount!=0)
            {
                for (int i = friendsContent.transform.childCount - 1; i >= 0; i--)
                {
                    Debug.Log("Tocó Borrar");
                    GameObject child = friendsContent.transform.GetChild(i).gameObject;
                    Destroy(child);
                }
            }

            friendUi = false;
            RectTransform parentRectTransform = friendsContent.GetComponent<RectTransform>();
            int j = 0;
            //Debug.Log("La lista tiene" + frindsList.Count);
            foreach (string username in frindsList)
            {
                Debug.Log("Agregué");
                frindsPrefab.GetComponent<FriendItem>().SetUp(username,frindsUserIdList[j],gameObject);
                GameObject newCopy = GameObject.Instantiate(frindsPrefab);
                newCopy.GetComponent<RectTransform>().SetParent(parentRectTransform,false);
                j++;
            }
        }
    }

    IEnumerator DisableError(TextMeshProUGUI errorText)
    {
        yield return new WaitForSeconds(5);
        errorText.text = "";
    }
}
