using TMPro;
using UnityEngine;
using UnityEngine.Localization;
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

    private ActionBlockTooltip currentActionToolTip = null;


    void Start()
    {
        // Ocultar tooltip al iniciar
        tooltipPanel.SetActive(false);
    }

    // Mostrar el tooltip con el texto proporcionado
    public void ShowTooltip(string description, Vector2 position, Color blockColor)
    {
        tooltipText.text = description;
        tooltipPanel.transform.position = new Vector2(position.x + offsetX, position.y + offsetY);
        tooltipPanel.GetComponent<Image>().color = blockColor;
        tooltipPanel.SetActive(true);
    }

    // Ocultar el tooltip
    public void HideTooltip()
    {
        tooltipPanel.SetActive(false);
    }

    public bool IsTooltipActive()
    {
        return tooltipPanel.activeSelf;
    }

    public void SetCurrentBlock(ActionBlockTooltip currentBlock)
    {
        currentActionToolTip = currentBlock;
    }

    public ActionBlockTooltip GetCurrentBlock()
    {
        return currentActionToolTip;
    }
}