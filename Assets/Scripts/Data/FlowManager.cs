using System;
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
            game_id = loggedInUser + System.DateTime.Now.ToString(" - dd/MM/yyyy HH:mm:ss");
        else
            game_id = "";

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
}

public class SelectedSet
{
    public string name = "DefaultSet";
}
