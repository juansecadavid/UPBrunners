using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AuthErrorMessage : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI errorText;

    private void Start()
    {
        errorText = GetComponent<TextMeshProUGUI>();
    }

    public void ShowErrorMessage(string MessageError)
    {
        StopAllCoroutines();
        gameObject.SetActive(true);
        StartCoroutine(ShowError(MessageError));
    }

    private IEnumerator ShowError(string error)
    {
        errorText.text = error;
        yield return new WaitForSeconds(5);
        gameObject.SetActive(false);
    }
}
