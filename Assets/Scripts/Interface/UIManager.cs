using System.Collections;
using System.Collections.Generic;
// using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public HelpManager helpManager;

    public Canvas[] blockEngine;
    public Canvas inventory;
    public UnityEngine.UI.Image deathImage;
    public UnityEngine.UI.Image BHImage;
    private Player player;
    private bool notDie = true;

    // Texto de los objetos detectados
    public TextMeshProUGUI objectsDetectedText;
    // Texto de propiedades del jugador
    public TextMeshProUGUI properties;
    // Texto con el tiempo sobrevivido
    public TextMeshProUGUI textTime;
    // Panel con las reglas ejecutadas
    public GameObject rulesPanel;

    // Variable para comprobar el tiempo
    private float timeO = 0f;
    private float interval = 0.25f;

    // Texto para visualizar la velocidad del juego
    public TextMeshProUGUI speedText;


    void Start()
    {
        SetEnabled(true);
        inventory.enabled = false;
        deathImage.gameObject.SetActive(false);
        BHImage.gameObject.SetActive(false);
        player = FindAnyObjectByType<Player>();
        player.PlayerDeath.AddListener(ShowDeathImage);
        startTime = Time.time;
    }

    void Update()
    {
        // Si se pulsa una tecla se puede ver el inventario
        // if (Input.GetKeyDown(KeyCode.UpArrow))
        // {
        //     rulesPanel.SetActive(false);
        //     if (notDie)
        //     {
        //         SetEnabled(inventory.enabled);
        //         inventory.enabled = !inventory.enabled;
        //     }
        //     else
        //     {
        //         SetEnabled(deathImage.gameObject.activeSelf);
        //         deathImage.gameObject.SetActive(!deathImage.gameObject.activeSelf);
        //     }

        // }
        // if (notDie)
        // {
        //     if (Input.GetKeyDown(KeyCode.RightArrow))
        //     {
        //         BHImage.gameObject.SetActive(!BHImage.gameObject.activeSelf);
        //     }
        // }
        // if (Input.GetKeyDown(KeyCode.LeftArrow))
        // {
        //     rulesPanel.SetActive(!rulesPanel.activeSelf);
        //     SetEnabled(!rulesPanel.activeSelf);
        //     //inventory.enabled = !rulesPanel.activeSelf;
        // }

        //NUEVO CONTROL DE VENTANAS
        if (notDie & !helpManager.isTutorialActive)
        {
            // Abrir el inventario y el sistema de crafteo
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                rulesPanel.SetActive(false);
                SetEnabled(false);
                inventory.enabled = true;
            }
            // Abrir la Base de Conocimientos
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                rulesPanel.SetActive(false);
                SetEnabled(true);
                inventory.enabled = false;
            }
            // Abrir el log de las reglas
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                rulesPanel.SetActive(true);
                SetEnabled(false);
                inventory.enabled = false;
            }
            // Abrir la Base de Hechos
            if (Input.GetKeyDown(KeyCode.RightArrow) & !rulesPanel.activeSelf)
            {
                BHImage.gameObject.SetActive(!BHImage.gameObject.activeSelf);
            }

            // Modifica base de hechos
            CreateBH();
            // Actualiza el tiempo sobrevivido
            TimeTracker();
        }
    }

    public TextMeshProUGUI textTimeDeath;
    private void ShowDeathImage()
    {
        deathImage.gameObject.SetActive(true);
        textTimeDeath.text = "You survived " + minutes + " minutes and " + seconds + " seconds";
        SetEnabled(false);
        inventory.enabled = false;
        BHImage.gameObject.SetActive(false);
        notDie = false;
        Time.timeScale = 0f;
    }

    private void SetEnabled(bool enabled)
    {
        foreach (var canvas in blockEngine)
        {
            canvas.enabled = enabled;
        }
    }

    // Método para generar la base de hechos
    private void CreateBH()
    {
        timeO += Time.deltaTime;
        // Se actualiza una vez por segundo
        if (timeO >= interval)
        {
            objectsDetectedText.text = DetectedObjects(player.NamesDetected());
            properties.text = GetProperties();

            timeO = 0f;
        }
    }

    // Obtiene una cadena con los objetos detectados
    private string DetectedObjects(List<DetectableName> objects)
    {
        string result = "Entities detected:\n";
        foreach (DetectableName dN in objects)
        {
            result += dN.displayName + "\n";
        }
        return result;
    }

    // Obtiene una cadena de las propiedades del jugador
    private string GetProperties()
    {
        int maxLevelProperties = player.maxLevelProperties;
        string result = "Facts:\n\n";
        result += "Health: " + player.Health.ToString() + " / " + maxLevelProperties + "\n";
        result += "Hunger: " + player.Hunger.ToString() + " / " + maxLevelProperties + "\n";
        result += "Thirst: " + player.Thirst.ToString() + " / " + maxLevelProperties + "\n";
        result += "Tiredness: " + player.Tiredness.ToString() + " / " + maxLevelProperties + "\n";

        // Objeto equipado
        ItemData equippedItem = player.equippedItem;
        if (equippedItem != null)
        {
            result += "Equipped item: " + player.equippedItem.displayName + "\n";
        }
        else
        {
            result += "Equipped item:\n";
        }

        // Tipo de ataque
        result += "Attack type: " + player.attackType.ToString() + "\n";

        // Movimiento que está realizando
        result += "Movement: " + player.GetMovement();


        return result;
    }

    private float startTime = 0;
    private int minutes = 0;
    private int seconds = 0;
    public void TimeTracker()
    {
        // Calcula el tiempo transcurrido desde el inicio del juego
        float elapsedTime = Time.time - startTime;

        // Formatea el tiempo en minutos y segundos
        minutes = Mathf.FloorToInt(elapsedTime / 60);
        seconds = Mathf.FloorToInt(elapsedTime % 60);

        // Actualiza el texto del temporizador
        textTime.text = "Time survived: ";
        textTime.text += string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        EventLogger.Instance.LogEvent(new EventData("sr-pause"));
    }

    public void Play()
    {
        Time.timeScale = 1f;
        EventLogger.Instance.LogEvent(new EventData("sr-continue"));
    }

    public void ModifySpeed()
    {
        if (Time.timeScale == 3f)
        {
            Time.timeScale = 1f;
            speedText.text = "x1";
            EventLogger.Instance.LogEvent(new EventData("sr-mod_speed_x1"));
        }
        else
        {
            if (Time.timeScale == 1f)
            {
                Time.timeScale = 2f;
                speedText.text = "x2";
                EventLogger.Instance.LogEvent(new EventData("sr-mod_speed_x2"));
            }
            else
            {
                if (Time.timeScale == 2f)
                {
                    Time.timeScale = 3f;
                    speedText.text = "x3";
                    EventLogger.Instance.LogEvent(new EventData("sr-mod_speed_x3"));
                }
            }
        }
    }


}
