using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WindowsManager : MonoBehaviour
{
    public TMP_InputField userInput;
    public GameObject errorMessage;
    public GameObject saveWindow;

    public void Start()
    {
        if (SceneManager.GetActiveScene().name == "LogIn")
        {
            errorMessage.SetActive(false);
            userInput.text = "";
        }
    }

    public void OpenGameScene()
    {
        FlowManager.instance.GenerateGameID(true);
        FlowManager.instance.sessionFinished = false;
        EventLogger.Instance.LogEvent(new EventData("sr-start_game", new PlayerEvent()));
        SceneManager.LoadScene("Game");
        Time.timeScale = 0f;
    }

    public void QuitGame()
    {
        EventLogger.Instance.LogEvent(new EventData("sr-log_out", new PlayerEvent()));
        Application.Quit();
    }

    public void OpenMenuScene()
    {
        Time.timeScale = 1f;
        FlowManager.instance.sessionFinished = true;
        EventLogger.Instance.LogEvent(new EventData("sr-end_game", new PlayerEvent()));
        FlowManager.instance.GenerateGameID(false);
        SceneManager.LoadScene("MainMenu");
    }

    public void OpenMenuSceneInLogIn()
    {
        Time.timeScale = 1f;
        if (CheckUser(userInput.text))
        {
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            errorMessage.SetActive(true);
        }
    }

    // Método para comprobar el usuario introducido
    private bool CheckUser(string user)
    {
        // Comprobar si existe y registrarlo
        if (user == "Jose")
        {
            FlowManager.instance.loggedInUser = user;
            FlowManager.instance.session_id = Guid.NewGuid().ToString();
            EventData logInEvent = new EventData("sr-log_in", new PlayerEvent());
            EventLogger.Instance.LogEvent(logInEvent);
            return true;
        }

        return false;
    }

    public void OpenLogInScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("LogIn");
    }

    public void SaveWhenExit()
    {
        EventLogger.Instance.LogEvent(new EventData("sr-save_sbr", new PlayerEvent()));
        saveWindow.GetComponentInParent<Canvas>().enabled = true;
        saveWindow.SetActive(true);
    }

}
