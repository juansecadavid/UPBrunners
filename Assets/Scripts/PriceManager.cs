using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PriceManager : MonoBehaviour
{
    public TextMeshProUGUI textoPrecio;

    void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.gameObject.CompareTag("Skin 1"))
        {
           textoPrecio.text = "$250";
        }
        if(collision.gameObject.CompareTag("Skin 2"))
        {
           textoPrecio.text = "$150";
        }
        if(collision.gameObject.CompareTag("Skin 3"))
        {
           textoPrecio.text = "$100";
        }
        if(collision.gameObject.CompareTag("Skin 4"))
        {
           textoPrecio.text = "$300";
        }
        if(collision.gameObject.CompareTag("Skin 5"))
        {
           textoPrecio.text = "$150";
        }
    }
}
