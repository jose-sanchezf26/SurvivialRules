using UnityEngine;

public class ChangeHitColor : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Color hitColor = Color.red;
    public float hitDuration = 0.5f;

    private Color originalColor;

    [HideInInspector]
    public bool isHit = false;
    private float hitTimer = 0f;

    void Start()
    {
        // spriteRenderer = GetComponent<SpriteRenderer>();
        // Guarda el color original del sprite
        originalColor = spriteRenderer.color;
    }

    void Update()
    {
        if (isHit)
        {
            spriteRenderer.color = hitColor;
            hitTimer += Time.deltaTime;

            // Si el tiempo ha pasado, restaura el color original y reinicia el temporizador
            if (hitTimer >= hitDuration)
            {
                spriteRenderer.color = originalColor;
                isHit = false;
                hitTimer = 0f;
            }
        }
    }
}