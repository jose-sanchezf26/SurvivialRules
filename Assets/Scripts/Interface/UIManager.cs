using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Canvas[] blockEngine;
    public Canvas inventory;
    public UnityEngine.UI.Image deathImage;
    private Player player;
    private bool notDie = true;

    // Texto de los objetos detectados
    public TextMeshProUGUI objectsDetectedText;
    // Texto de propiedades del jugador
    public TextMeshProUGUI properties;

    // Variable para comprobar el tiempo
    private float timeO = 0f;
    private float interval = 1f;


    void Start()
    {
        SetEnabled(true);
        inventory.enabled = false;
        deathImage.gameObject.SetActive(false);
        player = FindAnyObjectByType<Player>();
        player.PlayerDeath.AddListener(ShowDeathImage);
    }

    void Update()
    {
        // Si se pulsa una tecla se puede ver el inventario
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (notDie)
            {
                SetEnabled(inventory.enabled);
                inventory.enabled = !inventory.enabled;
            }
            else
            {
                SetEnabled(deathImage.gameObject.activeSelf);
                deathImage.gameObject.SetActive(!deathImage.gameObject.activeSelf);
            }

        }

        CreateBH();
    }

    private void ShowDeathImage()
    {
        deathImage.gameObject.SetActive(true);
        SetEnabled(false);
        inventory.enabled = false;
        notDie = false;
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
        string result = "Base de Hechos:\n";
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
}
