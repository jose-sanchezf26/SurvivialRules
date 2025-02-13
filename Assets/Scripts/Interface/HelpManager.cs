using UnityEngine;
using UnityEngine.UI;

public class HelpManager : MonoBehaviour
{
    // Paneles de ayuda del juego
    public GameObject[] helpPanels;
    private int currentPanelIndex = 0;
    // Botón para abrir la ayuda
    public Button helpButton;

    // Paneles del juego, representando un SBR
    public GameObject KBPanel;
    public GameObject DBPanel;

    // Variable para bloquear controles de las ventanas
    public bool isTutorialActive;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (var helppanel in helpPanels)
        {
            helppanel.SetActive(false);
        }

        helpButton.onClick.AddListener(OpenHelp);
        isTutorialActive = false;
    }

    // Función para abrir la ayuda
    public void OpenHelp()
    {
        if (!isTutorialActive)
        {
            currentPanelIndex = 0;
            helpPanels[currentPanelIndex].SetActive(true);
            KBPanel.SetActive(false);
            DBPanel.SetActive(false);
            isTutorialActive = true;
            Time.timeScale = 0;

            EventLogger.Instance.LogEvent(new EventData("sr-start_tutorial"));
        }
    }


    // Se encarga de abrir la siguiente ventana
    public void NextHelpPanel()
    {
        helpPanels[currentPanelIndex].SetActive(false);
        currentPanelIndex += 1;
        if (currentPanelIndex == 1 | currentPanelIndex == 3)
        {
            KBPanel.SetActive(true);
            DBPanel.SetActive(false);
        }
        else
        {
            KBPanel.SetActive(false);
            DBPanel.SetActive(true);
        }

        if (currentPanelIndex < helpPanels.Length)
        {
            helpPanels[currentPanelIndex].SetActive(true);
        }
        else
        {
            CloseHelp();
        }
    }

    public void CloseHelp()
    {
        foreach (var helppanel in helpPanels)
        {
            helppanel.SetActive(false);
        }
        KBPanel.SetActive(true);
        isTutorialActive = false;
        Time.timeScale = 1;
        EventLogger.Instance.LogEvent(new EventData("sr-end_tutorial"));
    }
}
