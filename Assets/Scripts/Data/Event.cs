using System;
using System.Collections.Generic;
using UnityEngine;

// Clases que definen los tipos de evento y sus campos
public class EventData
{
    // Usuario
    public string user;
    // ID de la partida
    public string game_id;
    // Tipo de evento
    public string eventType;
    //Fecha del evento
    public string time;

    public EventData(string eventType)
    {
        user = FlowManager.instance.loggedInUser;
        game_id = FlowManager.instance.game_id;
        this.eventType = eventType;
        time = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
    }
    public string ToJson() => JsonUtility.ToJson(this);
}

public class PlayerEvent : EventData
{
    // Acción realizada
    public string action;
    public PlayerEvent(string eventType, string action) : base(eventType)
    {
        this.action = action;
    }
}

public class ModifySBREvent : PlayerEvent
{
    // Cadena correspondiente al SBR
    public string sbr;
    public ModifySBREvent(string eventType, string action, string sbr) : base(eventType, action)
    {
        this.sbr = sbr;
    }
}