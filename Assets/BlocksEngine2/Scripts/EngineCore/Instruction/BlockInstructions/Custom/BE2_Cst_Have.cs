using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MG_BlocksEngine2.Block.Instruction;
using MG_BlocksEngine2.Block;


public class BE2_Cst_Have : BE2_InstructionBase, I_BE2_Instruction
{
    I_BE2_BlockSectionHeaderInput _input0;
    BE2_InputValues _v0;
    public new void Function()
    {
        ExecuteNextInstruction();
    }

    public new string Operation()
    {
        // Obtiene el valor del input del bloque
        _input0 = Section0Inputs[0];
        _v0 = _input0.InputValues;

        bool result = false;
        if (_v0.isText)
        {
            result = TargetObject.Player.ItemInInventory(_v0.stringValue);
        }

        if (result)
        {
            return "true";
        }
        else
        {
            return "false";
        }
    }

}
