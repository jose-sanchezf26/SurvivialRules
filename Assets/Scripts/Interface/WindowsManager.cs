using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
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

    public void Restart()
    {
        FlowManager.instance.GenerateGameID(true);
        FlowManager.instance.sessionFinished = false;
        int timeSurvived = (int)Time.timeSinceLevelLoad;
        EventLogger.Instance.LogEvent(new EventData("sr-end_game", new EndGameEvent(timeSurvived, "death")));
        SaveTimeSurvived(timeSurvived);
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
        int timeSurvived = (int)Time.timeSinceLevelLoad;
        EventLogger.Instance.LogEvent(new EventData("sr-end_game", new EndGameEvent(timeSurvived, "exit")));
        SaveTimeSurvived(timeSurvived);
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

    public int LoadTimeSurvived()
    {
        int timeSurvivedRecord = 0;
        string filePath = Application.persistentDataPath + "/timeSurvived.txt";
        if (File.Exists(filePath))
        {
            // Leemos el archivo y lo convertimos a un entero
            string timeFromFile = File.ReadAllText(filePath);
            timeSurvivedRecord = int.Parse(timeFromFile);
        }
        return timeSurvivedRecord;
    }

    public void SaveTimeSurvived(int timeSurvived)
    {
        int timeSurvivedRecord = LoadTimeSurvived();
        if (timeSurvivedRecord < timeSurvived)
        {
            string filePath = Application.persistentDataPath + "/timeSurvived.txt";
            // Convertimos el entero a una cadena
            string timeToSave = timeSurvived.ToString();

            // Guardamos el archivo en la ruta definida
            File.WriteAllText(filePath, timeToSave);
            Debug.Log("Tiempo guardado: " + timeSurvived);
        }
    }

}
