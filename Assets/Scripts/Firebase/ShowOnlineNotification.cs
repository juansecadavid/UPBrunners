using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowOnlineNotification : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI messageText;

    [SerializeField] private GameObject Image;
    public void ShowNotification(string message)
    {
        StartCoroutine(ShowMessage(message));
    }

    IEnumerator ShowMessage(string message)
    {
        messageText.text = $"{message} se ha conectado!!!";
        Image.SetActive(true);
        yield return new WaitForSeconds(5);
        Image.SetActive(false);
    }
}
