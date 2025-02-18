using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;


// Clases que definen los tipos de evento y sus campos
public class EventData
{
    // Usuario
    public string user;
    // ID de la sesión
    public string session_id;
    // ID de la partida
    public string game_id;
    // Tipo de evento
    public string eventType;
    //Fecha del evento
    public string time;
    // Variable para los datos del evento
    public PlayerEvent data;

    public EventData(string eventType, PlayerEvent data)
    {
        user = FlowManager.instance.loggedInUser;
        game_id = FlowManager.instance.game_id;
        session_id = FlowManager.instance.session_id;
        this.data = data;
        this.eventType = eventType;
        time = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
    }
    public string ToJson() => JsonConvert.SerializeObject(this, new JsonSerializerSettings
    {
        TypeNameHandling = TypeNameHandling.None, // Serializa correctamente la herencia
        Formatting = Formatting.Indented
    });
}


// Clase padre para el resto de eventos
[JsonObject(MemberSerialization.OptIn)]
public class PlayerEvent
{
    public PlayerEvent() { }
}

[JsonObject(MemberSerialization.OptIn)]
public class ModifySBREvent : PlayerEvent
{
    // Cadena correspondiente al SBR
    [JsonProperty]
    public string sbr;
    public ModifySBREvent() : base()
    {
        this.sbr = "";
    }
}

[JsonObject(MemberSerialization.OptIn)]
public class DropBlockEvent : PlayerEvent
{
    [JsonProperty]
    public string blockType;
    [JsonProperty]
    public string blockId;
    [JsonProperty]
    public float positionX;
    [JsonProperty]
    public float positionY;
    [JsonIgnore]
    public Vector2 position
    {
        get => new Vector2(positionX, positionY);
        set
        {
            positionX = value.x;
            positionY = value.y;
        }
    }
    // Cadena correspondiente al SBR
    [JsonProperty]
    public string sbr;

    public DropBlockEvent(string blockType, string blockId, Vector2 position) : base()
    {
        this.blockType = blockType;
        this.blockId = blockId;
        this.position = position;
        this.sbr = "";
    }
}

[JsonObject(MemberSerialization.OptIn)]
public class DropBlockFromEvent : DropBlockEvent
{
    [JsonProperty]
    public string parentBlockType;
    [JsonProperty]
    public string parentBlockId;

    public DropBlockFromEvent(string blockType, string blockId, string parentBlockType, string parentBlockId, Vector2 position) : base(blockType, blockId, position)
    {
        this.parentBlockType = parentBlockType;
        this.parentBlockId = parentBlockId;
        this.position = position;
    }
}

[JsonObject(MemberSerialization.OptIn)]
public class CreateBlockEvent : PlayerEvent
{
    [JsonProperty]
    public string blockType;
    [JsonProperty]
    public string blockId;

    public CreateBlockEvent(string blockType, string blockId) : base()
    {
        this.blockType = blockType;
        this.blockId = blockId;
    }
}

[JsonObject(MemberSerialization.OptIn)]
public class SelectBlockEvent : PlayerEvent
{
    [JsonProperty]
    public string blockType;
    [JsonProperty]
    public string blockId;
    [JsonProperty]
    public float positionX;
    [JsonProperty]
    public float positionY;
    [JsonIgnore]
    public Vector2 position
    {
        get => new Vector2(positionX, positionY);
        set
        {
            positionX = value.x;
            positionY = value.y;
        }
    }

    public SelectBlockEvent(string blockType, string blockId, Vector2 position) : base()
    {
        this.blockType = blockType;
        this.blockId = blockId;
        this.position = position;
    }
}

[JsonObject(MemberSerialization.OptIn)]
public class SelectBlockFromEvent : SelectBlockEvent
{
    [JsonProperty]
    public string parentBlockType;
    [JsonProperty]
    public string parentBlockId;

    public SelectBlockFromEvent(string blockType, string blockId, string parentBlockType, string parentBlockId, Vector2 position) : base(blockType, blockId, position)
    {
        this.parentBlockType = parentBlockType;
        this.parentBlockId = parentBlockId;
    }
}