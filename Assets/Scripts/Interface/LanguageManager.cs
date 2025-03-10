using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;
using System.Collections;

public class LanguageManager : MonoBehaviour
{
    public TMP_Dropdown languageDropdown;

    void Start()
    {
        // Llamamos a la corrutina para esperar que se inicialice la localización antes de proceder
        StartCoroutine(WaitForLocalizationAndPopulate());
    }

    // Corrutina para esperar que la localización se haya cargado correctamente
    IEnumerator WaitForLocalizationAndPopulate()
    {
        // Esperar hasta que la operación de inicialización de la localización haya terminado
        yield return LocalizationSettings.InitializationOperation;

        // Ahora que la localización está lista, llenamos el Dropdown
        PopulateDropdown();

        // Agregar el listener para el cambio de idioma
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
