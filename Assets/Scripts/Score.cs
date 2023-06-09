using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;
    public int increment = 1;       // Incremento entre números consecutivos
    public float speed = 10f;        // Velocidad de incremento del contador

    public int currentNumber=0;      // Número actual del contador
    private float timer=0f;            // Temporizador para controlar la velocidad

    void Update()
    {
        // Incrementar el temporizador en función del tiempo transcurrido
        timer += Time.deltaTime;

        // Si el temporizador supera la velocidad deseada, incrementar el número y reiniciar el temporizador
        if (timer >= (1f / speed))
        {
            currentNumber += increment;
            scoreText.text = $"{currentNumber}";
            timer = 0f;
        }

        // Mostrar el número actual en la consola
        Debug.Log(currentNumber);
    }
}
