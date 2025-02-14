using System;
using System.Collections.Concurrent;
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
    public PlayerEvent(string eventType) : base(eventType)
    {
     
    }
}

public class ModifySBREvent : PlayerEvent
{
    // Cadena correspondiente al SBR
    public string sbr;
    public ModifySBREvent(string eventType) : base(eventType)
    {
        this.sbr = "";
    }
}

public class DropBlockEvent : PlayerEvent
{
    public string blockType;
    public string blockId;
    public Vector2 position;
    // Cadena correspondiente al SBR
    public string sbr;

    public DropBlockEvent(string eventType, string blockType, string blockId, Vector2 position) : base(eventType)
    {
        this.blockType = blockType;
        this.blockId = blockId;
        this.position = position;
        this.sbr = "";
    }
}

public class DropBlockFromEvent : DropBlockEvent
{
    public string parentBlockType;
    public string parentBlockId;

    public DropBlockFromEvent(string eventType, string blockType, string blockId, string parentBlockType, string parentBlockId, Vector2 position) :base(eventType, blockType, blockId, position)
    {
        this.parentBlockType = parentBlockType;
        this.parentBlockId = parentBlockId;
        this.position = position;
    }
}

public class CreateBlockEvent : PlayerEvent
{
    public string blockType;
    public string blockId;

    public CreateBlockEvent(string eventType, string blockType, string blockId) : base(eventType)
    {
        this.blockType = blockType;
        this.blockId = blockId;
    }
}

public class SelectBlockEvent : PlayerEvent
{
    public string blockType;
    public string blockId;
    public Vector2 position;

    public SelectBlockEvent(string eventType, string blockType, string blockId, Vector2 position) :base(eventType)
    {
        this.blockType = blockType;
        this.blockId = blockId;
        this.position = position;
    }
}

public class SelectBlockFromEvent : SelectBlockEvent
{
    public string parentBlockType;
    public string parentBlockId;

    public SelectBlockFromEvent(string eventType, string blockType, string blockId, string parentBlockType, string parentBlockId, Vector2 position) :base(eventType, blockType, blockId, position)
    {
        this.parentBlockType = parentBlockType;
        this.parentBlockId = parentBlockId;
    }
}