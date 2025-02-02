using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TooltipManager : MonoBehaviour
{
    // Panel que actúa como tooltip
    public GameObject tooltipPanel;
    // Texto dentro del tooltip
    public TextMeshProUGUI tooltipText;
    // Desplazamiento en el eje X
    public float offsetX = 50f;
    public float offsetY = 0;

    void Start()
    {
        // Ocultar tooltip al iniciar
        tooltipPanel.SetActive(false);
    }

    // Mostrar el tooltip con el texto proporcionado
    public void ShowTooltip(string description, Vector2 position)
    {
        tooltipText.text = description;
        tooltipPanel.transform.position = new Vector2(position.x + offsetX, position.y + offsetY);
        tooltipPanel.SetActive(true);
    }

    // Ocultar el tooltip
    public void HideTooltip()
    {
        tooltipPanel.SetActive(false);
    }
}