using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

using MG_BlocksEngine2.Utils;
using TMPro;

namespace MG_BlocksEngine2.Block
{
    // v2.10 - Dropdown and InputField references in the block header inputs replaced by BE2_Dropdown and BE2_InputField to enable the use of legacy or TMP components
    public class BE2_BlockSectionHeader_InputField : MonoBehaviour, I_BE2_BlockSectionHeaderItem, I_BE2_BlockSectionHeaderInput
    {
        BE2_InputField _inputField;
        RectTransform _rectTransform;

        public Transform Transform => transform;
        public Vector2 Size => _rectTransform ? _rectTransform.sizeDelta : GetComponent<RectTransform>().sizeDelta;
        public I_BE2_Spot Spot { get; set; }
        public float FloatValue { get; set; }
        public string StringValue { get; set; }
        public BE2_InputValues InputValues { get; set; }
        private BE2_Block block;

        void OnValidate()
        {
            Awake();
        }

        void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _inputField = BE2_InputField.GetBE2Component(transform);

            // Añadido para los eventos al escribir en inputs
            if (transform.parent != null &&
                transform.parent.parent != null &&
                transform.parent.parent.parent != null)
                block = transform.parent.parent.parent.GetComponent<BE2_Block>();
            if (_inputField != null)
            {
                _inputField.onSelect?.AddListener(OnInputFieldSelected);
                _inputField.onDeselect?.AddListener(OnInputFieldDeselected);
            }

            Spot = GetComponent<I_BE2_Spot>();
        }

        // Función para cuando se selecciona el input
        void OnInputFieldSelected(string even)
        {
            EventLogger.Instance.LogEvent(new EventData("sr-select_input", new SelectInputEvent(block.ExtractBlockName(), block.id, GetInputIndex(), _inputField.text)));
        }
        // Función para cuando se deselecciona el input
        void OnInputFieldDeselected(string even)
        {
            EventLogger.Instance.LogEvent(new EventData("sr-deselect_input", new SelectInputEvent(block.ExtractBlockName(), block.id, GetInputIndex(), _inputField.text)));
        }

        // Función para obtener el indice del input
        public int GetInputIndex()
        {
            Transform parent = this.transform.parent;
            int index = 1;
            if (parent != null)
            {
                foreach (Transform child in parent)
                {
                    if (gameObject.GetInstanceID() == child.gameObject.GetInstanceID())
                    {
                        return index;
                    }
                    if (child.gameObject.activeSelf && (child.GetComponent<BE2_Block>() != null || child.GetComponent<TMP_InputField>() != null || child.GetComponent<TMP_Dropdown>() != null)) // Filtra solo los que tienen el script
                    {
                        index++;
                    }
                }
            }
            return 0;
        }

        void OnEnable()
        {
            UpdateValues();
            _inputField.onEndEdit.AddListener(delegate { UpdateValues(); });
        }

        void OnDisable()
        {
            _inputField.onEndEdit.RemoveAllListeners();
        }

        void Start()
        {
            UpdateValues();
        }

        public void UpdateValues()
        {
            bool isText;
            string stringValue = "";
            if (_inputField.text != null)
            {
                stringValue = _inputField.text;
            }
            StringValue = stringValue;

            float floatValue = 0;
            try
            {
                floatValue = float.Parse(StringValue, CultureInfo.InvariantCulture);
                isText = false;
            }
            catch
            {
                isText = true;
            }
            FloatValue = floatValue;

            InputValues = new BE2_InputValues(StringValue, FloatValue, isText);
        }
    }
}
