using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Canvas[] blockEngine;
    public Canvas inventory;
    public UnityEngine.UI.Image deathImage;
    private Player player;
    private bool notDie = true;

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
}
