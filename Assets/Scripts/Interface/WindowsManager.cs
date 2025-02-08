using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using TMPro;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WindowsManager : MonoBehaviour
{
    public TMP_InputField userInput;
    public GameObject errorMessage;

    public void Start()
    {
        if (SceneManager.GetActiveScene().name == "LogIn")
            errorMessage.SetActive(false);
    }

    public void OpenGameScene()
    {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenMenuScene()
    {
        Time.timeScale = 1f;
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
            return true;
        }

        return false;
    }

    public void OpenLogInScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("LogIn");
    }

}
