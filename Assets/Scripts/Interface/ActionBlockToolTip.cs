using System.Collections;
using System.Numerics;
using UnityEngine;
using UnityEngine.EventSystems;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class ActionBlockTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string actionDescription; // Descripción de la acción para este bloque
    private TooltipManager tooltipManager;
    // Para obtener la posición del bloque
    private RectTransform blockTransform;

    private Coroutine tooltipCoroutine;

    void Start()
    {
        // Buscar el TooltipManager en la escena
        tooltipManager = FindFirstObjectByType<TooltipManager>();
        blockTransform = GetComponent<RectTransform>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (tooltipManager != null)
        {
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
        yield return new WaitForSeconds(delay); // Espera de 1 segundo

        // Verificar que el ratón todavía está sobre el bloque después del retardo
        Vector2 blockPosition = RectTransformUtility.WorldToScreenPoint(null, blockTransform.position);
        tooltipManager.ShowTooltip(actionDescription, Input.mousePosition);
    }
}
