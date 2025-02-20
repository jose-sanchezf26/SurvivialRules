using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using MG_BlocksEngine2.DragDrop;
using MG_BlocksEngine2.Block.Instruction;
using MG_BlocksEngine2.Core;
using System.Text.RegularExpressions;

namespace MG_BlocksEngine2.Block
{
    public class BE2_Block : MonoBehaviour, I_BE2_Block
    {
        [SerializeField]
        BlockTypeEnum _type;
        public BlockTypeEnum Type { get => _type; set => _type = value; }
        Transform _transform;
        public Transform Transform => _transform ? _transform : transform;
        public I_BE2_BlockLayout Layout { get; set; }
        public I_BE2_Instruction Instruction { get; set; }
        public I_BE2_BlockSection ParentSection { get; set; }
        public I_BE2_Drag Drag { get; set; }

        // Creación del campo de id del bloque
        private static int nextId = 0;
        public int id { get; set; }

        void OnValidate()
        {
            Awake();
        }

        void Awake()
        {
            // Se le asigna un id al bloque
            id = nextId++;

            _transform = transform;
            Layout = GetComponent<I_BE2_BlockLayout>();
            Instruction = GetComponent<I_BE2_Instruction>();

            // v2.8 - removed unused reference to Drop Manager from BE2_Block
            Drag = GetComponent<I_BE2_Drag>();
        }

        void Start()
        {
            // v2.1 - bugfix: fixed detection of spots between child blocks before first block is dropped 
            GetParentSection();
        }

        // v2.12 - listen to event moved to OnEnable in Block class
        void OnEnable()
        {
            BE2_MainEventsManager.Instance.StartListening(BE2EventTypes.OnPrimaryKeyUpEnd, GetParentSection);
        }

        void OnDestroy()
        {
            if (!FlowManager.instance.sessionFinished)
                EventLogger.Instance.LogEvent(new EventData("sr-delete_block", new CreateBlockEvent(ExtractBlockName(this.ToString()), id.ToString())));
        }

        public string ExtractBlockName()
        {
            string pattern = @"Block (Ins|Cst|Op) (\w+)\s*\(";
            Match match = Regex.Match(this.ToString(), pattern);
            if (match.Success) { return match.Groups[2].Value; }
            return string.Empty;
        }

        private string ExtractBlockName(string blockInstruction)
        {
            string pattern = @"Block (Ins|Cst|Op) (\w+)\s*\(";
            Match match = Regex.Match(blockInstruction, pattern);
            if (match.Success) { return match.Groups[2].Value; }
            return string.Empty;
        }

        void OnDisable()
        {
            BE2_MainEventsManager.Instance.StopListening(BE2EventTypes.OnPrimaryKeyUpEnd, GetParentSection);
        }

        void GetParentSection()
        {
            ParentSection = GetComponentInParent<I_BE2_BlockSection>();
        }

        //void Update()
        //{
        //
        //}

        // v2.8 - removed redundant AddSpotsToManager and RemoveSpotsFromManager methods from BE2_Block

        // v2.12 - added shadow component null check to BE2_Block.SetShadowActive 
        public void SetShadowActive(bool value)
        {
            if (Type != BlockTypeEnum.operation)
            {
                if (value)
                {
                    foreach (I_BE2_BlockSection section in Layout.SectionsArray)
                    {
                        if (section.Header != null && section.Header.Shadow)
                        {
                            section.Header.Shadow.enabled = true;
                        }
                        if (section.Body != null && section.Body.Shadow)
                        {
                            section.Body.Shadow.enabled = true;
                        }
                    }
                }
                else
                {
                    foreach (I_BE2_BlockSection section in Layout.SectionsArray)
                    {
                        if (section.Header != null && section.Header.Shadow)
                        {
                            section.Header.Shadow.enabled = false;
                        }
                        if (section.Body != null && section.Body.Shadow)
                        {
                            section.Body.Shadow.enabled = false;
                        }
                    }
                }
            }
        }
    }
}
