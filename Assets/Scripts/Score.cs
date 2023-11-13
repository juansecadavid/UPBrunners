using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;
    public float increment = 1;
    [SerializeField]// Incremento entre números consecutivos
    private float speed = 10f;        // Velocidad de incremento del contador

    private int currentNumber=0;      // Número actual del contador
    private float timer=0f;            // Temporizador para controlar la velocidad
    private float scoreMultiplyer=1;
    public int CurrentNumber { get => currentNumber; set => currentNumber = value; }
    public float ScoreMultiplyer { get => scoreMultiplyer; set => scoreMultiplyer = value; }

    void Update()
    {
        // Incrementar el temporizador en función del tiempo transcurrido
        timer += Time.deltaTime;

        // Si el temporizador supera la velocidad deseada, incrementar el número y reiniciar el temporizador
        if (timer >= (1f / speed))
        {
            float incre = increment * scoreMultiplyer;
            currentNumber= (int)(currentNumber + incre);
            scoreText.text = $"{currentNumber}";
            timer = 0f;
        }
    }
}
