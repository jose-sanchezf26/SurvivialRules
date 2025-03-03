using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class LanguageManager : MonoBehaviour
{
    public TMP_Dropdown languageDropdown;

    void Start()
    {
        PopulateDropdown();

        languageDropdown.onValueChanged.AddListener(OnLanguageChanged);
    }

    void PopulateDropdown()
    {
        // Obtener las lenguas disponibles del sistema de localización
        var availableLocales = LocalizationSettings.AvailableLocales.Locales;

        // Limpiar opciones previas
        languageDropdown.ClearOptions();

        // Crear lista de nombres de idiomas para el Dropdown
        var languageOptions = new System.Collections.Generic.List<string>();
        foreach (var locale in availableLocales)
        {
            languageOptions.Add(locale.Identifier.ToString());
        }

        // Asignar las opciones al Dropdown
        languageDropdown.AddOptions(languageOptions);

        // Establecer el valor inicial (Idioma actual)
        string currentLocale = LocalizationSettings.SelectedLocale.Identifier.ToString();
        languageDropdown.value = languageOptions.IndexOf(currentLocale);
    }

    void OnLanguageChanged(int index)
    {
        // Obtener el idioma seleccionado en el Dropdown
        var selectedLocale = LocalizationSettings.AvailableLocales.Locales[index];

        // Cambiar el idioma actual
        LocalizationSettings.SelectedLocale = selectedLocale;
    }
}
