using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessagesOnPlay : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI messageField;
    public void ShowMessage(string message,float timeOnScreen)
    {
        StartCoroutine(Message(message,timeOnScreen));
    }
    IEnumerator Message(string message, float timeOnScreen)
    {
        messageField.text = message;
        messageField.gameObject.SetActive(true);
        yield return new WaitForSeconds(timeOnScreen);
        messageField.gameObject.SetActive(false);   
    }
}
