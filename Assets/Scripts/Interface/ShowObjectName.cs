using TMPro;
using UnityEngine;

public class ShowObjectName : MonoBehaviour
{
    public TextMeshPro textMesh;
    public string text;
    public TMP_FontAsset font;
    GameObject textObject;
    public float y_offset = -0.5f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textObject = new GameObject("ObjectNameText");
        textObject.transform.localPosition = new Vector3(0, y_offset, 0); // Posición debajo del objeto

        // Agregar TextMeshPro y configurar propiedades
        textMesh = textObject.AddComponent<TextMeshPro>();
        if (text == "")
            textMesh.text = GetComponent<DetectableName>().displayName;
        textMesh.alignment = TextAlignmentOptions.Center;
        textMesh.fontSize = 6;
        textMesh.color = Color.white;
        textMesh.font = font;
        textMesh.gameObject.SetActive(false); // Iniciar oculto
    }

    void Update()
    {
        if (textMesh != null)
        {
            Vector3 globalPosition = transform.position;
            textObject.transform.position = new Vector3(globalPosition.x, globalPosition.y + y_offset, globalPosition.z);
        }
    }

    private void OnMouseEnter()
    {
        if (textMesh != null)
        {
            UpdateBackgroundSize();
            Vector3 globalPosition = transform.position;
            textObject.transform.position = new Vector3(globalPosition.x, globalPosition.y + y_offset, globalPosition.z);
            textMesh.gameObject.SetActive(true);
        }
    }

    private void OnMouseExit()
    {
        if (textMesh != null)
        {
            textMesh.gameObject.SetActive(false);
        }
    }

    private void UpdateBackgroundSize()
    {
        if (textMesh == null) return;
        Vector2 textSize = textMesh.GetPreferredValues();
    }

    private Sprite GenerateBackgroundSprite()
    {
        Texture2D texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, Color.white);
        texture.Apply();
        return Sprite.Create(texture, new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f));
    }

}
