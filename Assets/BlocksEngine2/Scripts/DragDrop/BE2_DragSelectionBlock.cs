using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using MG_BlocksEngine2.Block;
using MG_BlocksEngine2.Core;
using MG_BlocksEngine2.UI;
using System.Text.RegularExpressions;

namespace MG_BlocksEngine2.DragDrop
{
    public class BE2_DragSelectionBlock : MonoBehaviour, I_BE2_Drag
    {
        // v2.11 - references to drag drop manager and execution manager refactored in drag scripts
        BE2_DragDropManager _dragDropManager => BE2_DragDropManager.Instance;
        RectTransform _rectTransform;
        BE2_UI_SelectionBlock _uiSelectionBlock;
        ScrollRect _scrollRect;

        Transform _transform;
        public Transform Transform => _transform ? _transform : transform;
        public Vector2 RayPoint => _rectTransform.position;
        public I_BE2_Block Block => null;

        void Awake()
        {
            _transform = transform;
            _rectTransform = GetComponent<RectTransform>();
            _uiSelectionBlock = GetComponent<BE2_UI_SelectionBlock>();
            _scrollRect = GetComponentInParent<ScrollRect>();
        }

        Vector3 _envScale = Vector3.one;

        public void OnPointerDown()
        {
            _envScale = BE2_ExecutionManager.Instance.ProgrammingEnvsList.Find(x => x.Visible == true).Transform.localScale;
        }

        public void OnRightPointerDownOrHold()
        {

        }

        public void OnDrag()
        {
            _scrollRect.StopMovement();
            _scrollRect.enabled = false;

            GameObject instantiatedBlock = Instantiate(_uiSelectionBlock.prefabBlock);

            // CODIGO PARA AÑADIR INFORMACIÓN DEL LOG

            string blockName = ExtractBlockName(instantiatedBlock.ToString());
            EventLogger.Instance.LogEvent("Ha seleccionado el bloque " + blockName + " del selector de bloques");

            // ***

            instantiatedBlock.name = _uiSelectionBlock.prefabBlock.name;
            I_BE2_Block block = instantiatedBlock.GetComponent<I_BE2_Block>();
            block.Drag.Transform.SetParent(_dragDropManager.DraggedObjectsTransform, true);

            I_BE2_BlocksStack blocksStack = instantiatedBlock.GetComponent<I_BE2_BlocksStack>();

            // v2.10 - scales the new block to the programming env's zoom
            instantiatedBlock.transform.localScale = _envScale;

            instantiatedBlock.transform.position = transform.position;
            _dragDropManager.CurrentDrag = block.Drag;

            block.Drag.OnPointerDown();
            block.Drag.OnDrag();

            // v2.6 - adjustments on position and angle of blocks for supporting all canvas render modes
            block.Transform.localEulerAngles = Vector3.zero;
        }

        private string ExtractBlockName(string blockInstruction)
        {
            string pattern = @"Block (Ins|Cst|Op) (\w+)\s*\(";
            Match match = Regex.Match(blockInstruction, pattern);
            if (match.Success) { return match.Groups[2].Value; }
            return string.Empty;
        }

        public void OnPointerUp()
        {

        }
    }
}