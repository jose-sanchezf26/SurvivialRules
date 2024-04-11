using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Canvas[] BlockEngine;
    public Canvas Inventory;

    void Start()
    {
        SetEnabled(true);
        Inventory.enabled = false;
    }

    void Update()
    {
        // Si se pulsa una tecla se puede ver el inventario
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetEnabled(Inventory.enabled);
            Inventory.enabled = !Inventory.enabled;
        }
    }

    private void SetEnabled(bool enabled)
    {
        foreach (var canvas in BlockEngine)
        {
            canvas.enabled = enabled;
        }
    }
}
