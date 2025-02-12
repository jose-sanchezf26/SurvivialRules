using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Net.WebSockets;
using System.Threading;
using Unity.VisualScripting;

public class DbReporter : MonoBehaviour
{
    #region Clases y Estructuras
    [System.Serializable]
    public class StartGameData
    {
        public string game_id = "shapes_playtest_2";
        public string group = FlowManager.instance.groupID;
        public string version_num = version;
        public OSEnvConfig env_configs = new OSEnvConfig();

        [System.Serializable]
        public class OSEnvConfig
        {
            public string OS = SystemInfo.operatingSystem ?? "unknown";
            public bool unityEditor = Application.isEditor;
        }
    }

    [System.Serializable]
    public class StartLevelData
    {
        public string user = FlowManager.instance.loggedInUser;
        public string group = FlowManager.instance.groupID;
        public string task_id;
        public string set_id;
        public bool fullscreen = Screen.fullScreen;
        public Vector2Int resolution = new Vector2Int(Screen.width, Screen.height);
        public string conditions;
    }

    public class DataObj
    {
        public string type;
        public string user;
        public string data;
        public Action<string> callback;

        public DataObj(string newType, string newData, Action<string> newCallback = null)
        {
            type = newType;
            data = newData;
            callback = newCallback;
            if (FlowManager.instance != null) { user = FlowManager.instance.loggedInUser; } else { user = "unknown"; }
        }

        public string ToJson() => JsonUtility.ToJson(this);
    }

    public class ResponseMessage
    {
        public int status;
        public string message;

        public override string ToString() => $"Response {status}: '{message}'";
    }
    #endregion

    #region Variables
    public static DbReporter instance;
    public const string version = "0.6.2";

    private ClientWebSocket ws;
    private Queue<DataObj> toPost = new Queue<DataObj>();
    private bool needRestart = false;
    private CancellationTokenSource cancelTokenSource;
    private string serverUri = "ws://localhost:10109/ws/";
    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private async void Start()
    {
        await ConnectWebSocket();
    }
    #endregion

    #region Métodos Principales
    public static void SendStartGameEvent(Action<string> callback)
    {
        var data = new StartGameData();
        SendEvent("start_game", JsonUtility.ToJson(data), callback);
    }

    // TODO: COMENTADO PORQUE NO EXISTE PUZZLEDATA
    // public static void SendStartLevelEvent(PuzzleData whichLevel)
    // {
    //     var data = new StartLevelData
    //     {
    //         task_id = whichLevel?.puzzleName ?? "Sandbox",
    //         set_id = FlowManager.instance.selectedSet?.name ?? "None",
    //         conditions = whichLevel?.GetConditionsString() ?? "{}"
    //     };
    //     SendEvent("start_level", JsonUtility.ToJson(data));
    // }

    public static void SendEvent(string type, string data, Action<string> callback = null)
    {
        instance.toPost.Enqueue(new DataObj(type, data, callback));
    }
    #endregion

    #region WebSocket
    private async Task ConnectWebSocket()
    {
        ws = new ClientWebSocket();
        cancelTokenSource = new CancellationTokenSource();

        try
        {
            await ws.ConnectAsync(new Uri(serverUri), cancelTokenSource.Token);
            Debug.Log("Conectado a WebSocket");
            _ = Task.Run(() => ListenWebSocket());
            _ = Task.Run(() => SendQueuedMessages());
        }
        catch (Exception ex)
        {
            Debug.LogError("Error en conexión WebSocket: " + ex.Message);
        }
    }

    private async Task ListenWebSocket()
    {
        var buffer = new byte[1024];

        while (ws.State == WebSocketState.Open)
        {
            var result = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), cancelTokenSource.Token);
            if (result.MessageType == WebSocketMessageType.Text)
            {
                string message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                Debug.Log("Mensaje recibido: " + message);
            }
        }
    }

    private async Task SendQueuedMessages()
    {
        while (ws.State == WebSocketState.Open)
        {
            if (toPost.Count > 0)
            {
                DataObj dataObj = toPost.Dequeue();
                string jsonMessage = dataObj.ToJson();
                var bytes = Encoding.UTF8.GetBytes(jsonMessage);
                await ws.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, cancelTokenSource.Token);
            }
            await Task.Delay(100);
        }
    }

    private async Task RestartWebSocket()
    {
        if (ws != null)
        {
            await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "Reiniciando conexión", CancellationToken.None);
            ws.Dispose();
        }
        await ConnectWebSocket();
    }

    private void OnApplicationQuit()
    {
        if (ws != null)
        {
            ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "Cerrando aplicación", CancellationToken.None).Wait();
        }
    }
    #endregion
}
