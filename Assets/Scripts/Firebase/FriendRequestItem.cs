using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FriendRequestItem : MonoBehaviour
{
    public FirebaseManager firebaseManager;
    public TextMeshProUGUI usernameText;
    public string friendId;

    public void Setup(string username, string id, GameObject fireBaseM)
    {
        usernameText.text = username;
        friendId = id;
        firebaseManager = fireBaseM.GetComponent<FirebaseManager>();
        //acceptButton.onClick.AddListener(() => AcceptFriendRequest());
        //declineButton.onClick.AddListener(() => RejectFriendRequest());
    }

    public void AcceptFriendRequest()
    {
        firebaseManager.AcceptFriendRequest(friendId);
        Debug.Log("Accepted friend request from: " + friendId);
        Destroy(gameObject);
    }

    public void RejectFriendRequest()
    {
        firebaseManager.RejectFriendRequest(friendId);
        Debug.Log("Rejected friend request from: " + friendId);
        Destroy(gameObject);
    }
}
