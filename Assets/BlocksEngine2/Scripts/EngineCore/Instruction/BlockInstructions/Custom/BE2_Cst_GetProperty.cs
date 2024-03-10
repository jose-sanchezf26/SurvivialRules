using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MG_BlocksEngine2.Block.Instruction;
using MG_BlocksEngine2.Block;
using System.Globalization;

// --- additional BE2 namespaces used for specific cases as accessing BE2 variables or the event manager
// using MG_BlocksEngine2.Core;
// using MG_BlocksEngine2.Environment;

public class BE2_Cst_GetProperty : BE2_InstructionBase, I_BE2_Instruction
{
     I_BE2_BlockSectionHeaderInput _input0;
     BE2_InputValues _v0;

    public new void Function()
    {
        ExecuteNextInstruction();
    }

    public new string Operation()
    {
        string result = "";
        _input0 = Section0Inputs[0];
        _v0 = _input0.InputValues;

        if(_v0.isText)
        {
            switch(_v0.stringValue)
            {
                case "Health": 
                    result = TargetObject.Player.Health.ToString(CultureInfo.InvariantCulture);
                    break; 
                case "Thrist": 
                    result = TargetObject.Player.Thirst.ToString(CultureInfo.InvariantCulture);
                    break;
                case "Hunger": 
                    result = TargetObject.Player.Hunger.ToString(CultureInfo.InvariantCulture);
                    break;
                case "Tiredness": 
                    result = TargetObject.Player.Tiredness.ToString(CultureInfo.InvariantCulture);
                    break;
            }
        }

        return result;
    }
}
