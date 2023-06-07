using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PriceManager : MonoBehaviour
{
   public TextMeshProUGUI textoPrecio;
   public RectTransform panel;
   public float tolerancia = 10f; // Tolerancia permitida

   private void Update()
   {
      float posicionX = panel.anchoredPosition.x;

      if(Mathf.Abs(posicionX - 105f) <= tolerancia || Mathf.Abs(posicionX - 833f) <= tolerancia)
      {
         textoPrecio.text = "Comprar $100";
      }
      if(Mathf.Abs(posicionX - 620f) <= tolerancia || Mathf.Abs(posicionX - 317f) <= tolerancia)
      {
         textoPrecio.text = "Comprar $300";
      }
      if( Mathf.Abs(posicionX - -401f) <= tolerancia || Mathf.Abs(posicionX - 1339f) <= tolerancia)
      {
         textoPrecio.text = "Comprar $200";
      }
   }
}
