using System;
using TMPro;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public static DifficultyManager Instance { get; private set; }

    // Inicia en dificultad 1
    public int currentDifficulty { get; private set; } = 1;  
    public int maxDifficulty = 5;
    // 2 minutos en segundos
    public float timeBetweenIncreases = 120f; 

    private float startTime;

    // Evento para notificar cambios
    public event Action<int> OnDifficultyChanged; 
    // Indicador de dificultad
    public TextMeshProUGUI text;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Evita duplicados
        }
    }

    private void Start()
    {
        startTime = Time.time;
    }

    private void Update()
    {
        // Comprueba si ha pasado el tiempo necesario para aumentar la dificultad
        int newDifficulty = Mathf.Min(1 + Mathf.FloorToInt((Time.time - startTime) / timeBetweenIncreases), maxDifficulty);
        
        if (newDifficulty != currentDifficulty)
        {
            currentDifficulty = newDifficulty;
            // Notificar a otros objetos
            OnDifficultyChanged?.Invoke(currentDifficulty); 
            text.text = "Dificultad : " + currentDifficulty;
        }
    }
}

