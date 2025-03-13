using System.Collections;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;
using UnityEngine.Localization;

public class ActionBlockTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public LocalizedString actionDescription; // Descripción de la acción para este bloque
    private TooltipManager tooltipManager;
    // Para obtener la posición del bloque
    private RectTransform blockTransform;

    private Coroutine tooltipCoroutine;
    private Vector2 lastMousePosition;

    void Start()
    {
        // Buscar el TooltipManager en la escena
        tooltipManager = FindFirstObjectByType<TooltipManager>();
        blockTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (tooltipManager.IsTooltipActive() && this == tooltipManager.GetCurrentBlock())
        {
            if ((Vector3)lastMousePosition != Input.mousePosition)
            {
                tooltipManager.HideTooltip();
            } 
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (tooltipManager != null)
        {
            tooltipManager.SetCurrentBlock(this);
            
            // Iniciar la corutina que muestra el tooltip después de 1 segundo
            tooltipCoroutine = StartCoroutine(ShowTooltipWithDelay(1f));
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Ocultar el tooltip cuando el ratón sale del bloque
        if (tooltipManager != null)
        {
            tooltipManager.HideTooltip();
        }
    }

    private IEnumerator ShowTooltipWithDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay); // Espera de 1 segundo

        // Guardamos la posición del ratón
        lastMousePosition = Input.mousePosition;
        // Verificar que el ratón todavía está sobre el bloque después del retardo
        if (this == tooltipManager.GetCurrentBlock())
        {
            Vector2 blockPosition = RectTransformUtility.WorldToScreenPoint(null, blockTransform.position);
            tooltipManager.ShowTooltip(actionDescription.GetLocalizedString(), Input.mousePosition, transform.GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color);
        }
    }
}
