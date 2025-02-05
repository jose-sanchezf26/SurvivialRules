using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EventLogger : MonoBehaviour
{
    // Lista para almacenar los eventos
    private List<string> eventLog = new List<string>();

    // Singleton para que sea accesible desde cualquier parte del juego
    public static EventLogger Instance;

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
    }

    // Guardar los eventos en un archivo al final de la sesión
    public void SaveLog()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "event_log.txt");
        File.WriteAllLines(filePath, eventLog.ToArray());
        Debug.Log($"Log guardado en: {filePath}");
    }

    // Opcional: guardar automáticamente cuando el juego se cierra
    private void OnApplicationQuit()
    {
        SaveLog();
    }
}

