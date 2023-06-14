using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;
    private int increment = 1;
    [SerializeField]// Incremento entre n�meros consecutivos
    private float speed = 10f;        // Velocidad de incremento del contador

    private int currentNumber=0;      // N�mero actual del contador
    private float timer=0f;            // Temporizador para controlar la velocidad

    public int CurrentNumber { get => currentNumber; set => currentNumber = value; }

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
    }
}
