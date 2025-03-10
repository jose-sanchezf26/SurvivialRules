using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class FlowManager : MonoBehaviour
{
    public static FlowManager instance;
    public string groupID = "defaultGroup";
    public string loggedInUser = "defaultUser";
    public string session_id;
    public string game_id;
    public SelectedSet selectedSet;
    public bool sessionFinished = false;

    internal void GenerateGameID(bool generate)
    {
        if (generate)
            game_id = "Survival Rules";
        else
            game_id = "Survival Rules";

    }

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

    public void SetUsername(string username)
    {
        loggedInUser = username;
        // session_id = Guid.NewGuid().ToString();
        // EventData logInEvent = new EventData("sr-log_in", new PlayerEvent());
        // EventLogger.Instance.LogEvent(logInEvent);
    }
}



public class SelectedSet
{
    public string name = "DefaultSet";
}
