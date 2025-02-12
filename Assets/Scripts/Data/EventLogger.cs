using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using MG_BlocksEngine2.Block;
using MG_BlocksEngine2.Environment;
using MG_BlocksEngine2.Serializer;
using UnityEngine;

public class EventLogger : MonoBehaviour
{
    // Lista para almacenar los eventos
    private List<string> eventLog = new List<string>();

    // Singleton para que sea accesible desde cualquier parte del juego
    public static EventLogger Instance;

    // Cadena para crear la estructura del SBR
    public string treeStructure;

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
    public void LogEvent(string eventDescription, string type)
    {
        // string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string timestamp = System.DateTime.Now.ToString("HH:mm:ss");
        eventLog.Add($"{timestamp}: {eventDescription}");
        Debug.Log(timestamp + " " + eventDescription);
        I_BE2_ProgrammingEnv i_BE2_ProgrammingEnv = FindFirstObjectByType<BE2_ProgrammingEnv>();

        // Identifica el tipo de evento
        if (type == "modifySBR")
        {
            Debug.Log(CreateSBRString(i_BE2_ProgrammingEnv));
        }

        // Si ya hay 10 eventos, los envía al servidor
        if (eventLog.Count == MAX_EVENTS)
        {
            SendEvents("game_events");
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
    public void SendEvents(string type)
    {
        string jsonData = JsonUtility.ToJson(new EventData(eventLog));
        DbReporter.SendEvent(type, jsonData);
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

    // FUNCIONES PARA CREAR LA ESTRUCTURA DEL SBR
    public string CreateSBRString(I_BE2_ProgrammingEnv targetProgrammingEnv)
    {
        List<I_BE2_Block> orderedBlocks = UpdateBlocksList(targetProgrammingEnv);
        return BE2_BlocksSerializer.BlocksCodeToXMLWithList(orderedBlocks);
    }

    private static List<I_BE2_Block> UpdateBlocksList(I_BE2_ProgrammingEnv targetProgrammingEnv)
    {
        List<I_BE2_Block> BlocksList = new List<I_BE2_Block>();
        foreach (Transform child in targetProgrammingEnv.Transform)
        {
            if (child.gameObject.activeSelf)
            {
                I_BE2_Block childBlock = child.GetComponent<I_BE2_Block>();
                if (childBlock != null)
                    BlocksList.Add(childBlock);
            }
        }
        return BlocksList;
    }
}

