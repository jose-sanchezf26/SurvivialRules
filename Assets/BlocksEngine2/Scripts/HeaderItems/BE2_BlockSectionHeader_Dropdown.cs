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
    public class BE2_BlockSectionHeader_Dropdown : MonoBehaviour, I_BE2_BlockSectionHeaderItem, I_BE2_BlockSectionHeaderInput
    {
        BE2_Dropdown _dropdown;
        RectTransform _rectTransform;

        public Transform Transform => transform;
        public Vector2 Size => _rectTransform ? _rectTransform.sizeDelta : GetComponent<RectTransform>().sizeDelta;
        public I_BE2_Spot Spot { get; set; }
        public float FloatValue { get; set; }
        public string StringValue { get; set; }
        public BE2_InputValues InputValues { get; set; }
        private BE2_Block block;
        private string oldValue;

        void OnValidate()
        {
            Awake();
        }

        void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _dropdown = BE2_Dropdown.GetBE2Component(transform);
            // Añadido para los eventos al escribir en inputs
            if (transform.parent != null &&
                transform.parent.parent != null &&
                transform.parent.parent.parent != null)
                block = transform.parent.parent.parent.GetComponent<BE2_Block>();
            if (_dropdown != null)
            {
                _dropdown.onValueChanged?.AddListener(OnValueChanged);
                oldValue = _dropdown.GetValue();
            }
            Spot = GetComponent<I_BE2_Spot>();
        }

        // Función para cuando cambia el valor en el dropdown
        void OnValueChanged(int n)
        {
            EventLogger.Instance.LogEvent(new EventData("sr-change_drop_down", new ChangeDropDownEvent(block.ExtractBlockName(), block.id, GetInputIndex(), oldValue.ToString(), _dropdown.GetValue())));
            oldValue = _dropdown.GetValue();
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
                    if (child.gameObject.activeSelf && (child.GetComponent<BE2_Block>() != null || child.GetComponent<TMPro.TMP_InputField>() != null || child.GetComponent<TMP_Dropdown>() != null)) // Filtra solo los que tienen el script
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
            if (_dropdown != null)
                _dropdown.onValueChanged.AddListener(delegate { UpdateValues(); });
        }

        void OnDisable()
        {
            if (_dropdown != null)
                _dropdown.onValueChanged.RemoveAllListeners();
        }

        void Start()
        {
            GetComponent<BE2_DropdownDynamicResize>().Resize(0);
            UpdateValues();
        }

        //void Update()
        //{
        //
        //}

        public void UpdateValues()
        {
            bool isText = false;
            if (_dropdown.GetOptionsCount() > 0)
            {
                StringValue = _dropdown.GetSelectedOptionText();
            }
            else
            {
                StringValue = "";
            }

            // v2.12 - dropdown input now returns the index of the selected item as FloatValue
            FloatValue = _dropdown.value;

            InputValues = new BE2_InputValues(StringValue, FloatValue, isText);
        }
    }
}
