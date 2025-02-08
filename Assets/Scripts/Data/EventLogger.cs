using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EventLogger : MonoBehaviour
{
    // Lista para almacenar los eventos
    private List<string> eventLog = new List<string>();

    // Singleton para que sea accesible desde cualquier parte del juego
    public static EventLogger Instance;

    // Número de eventos que se envían al servidor
    private const int MAX_EVENTS = 10;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // Mantener el logger entre escenas si es necesario
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Método para añadir un evento
    public void LogEvent(string eventDescription)
    {
        // string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string timestamp = System.DateTime.Now.ToString("HH:mm:ss");
        eventLog.Add($"{timestamp}: {eventDescription}");
        Debug.Log(timestamp + " " + eventDescription);

        // Si ya hay 10 eventos, los envía al servidor
        if (eventLog.Count == MAX_EVENTS)
        {
            SendEvents();
        }
    }

    // Guardar los eventos en un archivo al final de la sesión
    public void SaveLog()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "event_log.txt");
        File.WriteAllLines(filePath, eventLog.ToArray());
        Debug.Log($"Log guardado en: {filePath}");
    }

    // Método para enviar los eventos al servidor
    public void SendEvents()
    {
        string jsonData = JsonUtility.ToJson(new EventData(eventLog));
        DbReporter.SendEvent("game_events", jsonData);
        Debug.Log("Eventos enviados al servidor");
        eventLog.Clear();
    }

    // Guardar automáticamente cuando el juego se cierra
    private void OnApplicationQuit()
    {
        SaveLog();
    }

    // Clase para estructurar los eventos en JSON
    [System.Serializable]
    private class EventData
    {
        public List<string> events;
        public EventData(List<string> events)
        {
            this.events = events;
        }
    }
}

