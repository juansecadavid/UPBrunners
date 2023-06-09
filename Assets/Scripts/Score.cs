using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;
    public int increment = 1;       // Incremento entre n�meros consecutivos
    public float speed = 10f;        // Velocidad de incremento del contador

    public int currentNumber=0;      // N�mero actual del contador
    private float timer=0f;            // Temporizador para controlar la velocidad

    void Update()
    {
        // Incrementar el temporizador en funci�n del tiempo transcurrido
        timer += Time.deltaTime;

        // Si el temporizador supera la velocidad deseada, incrementar el n�mero y reiniciar el temporizador
        if (timer >= (1f / speed))
        {
            currentNumber += increment;
            scoreText.text = $"{currentNumber}";
            timer = 0f;
        }

        // Mostrar el n�mero actual en la consola
        Debug.Log(currentNumber);
    }
}
