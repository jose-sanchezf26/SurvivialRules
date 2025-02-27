using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private TextMeshProUGUI text;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        float timeElapsed = Time.time; 

        // Calcular horas, minutos y segundos
        int hours = Mathf.FloorToInt(timeElapsed / 3600); // 3600 segundos en una hora
        int minutes = Mathf.FloorToInt((timeElapsed % 3600) / 60); // Segundos restantes convertidos a minutos
        int seconds = Mathf.FloorToInt(timeElapsed % 60); // Resto de segundos

        // Formatear el tiempo en "hh:mm:ss"
        text.text = string.Format("{0:D2}:{1:D2}:{2:D2}", hours, minutes, seconds);
    }
}
