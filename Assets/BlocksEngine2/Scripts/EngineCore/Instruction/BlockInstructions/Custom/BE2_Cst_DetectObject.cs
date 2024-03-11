using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MG_BlocksEngine2.Block.Instruction;
using MG_BlocksEngine2.Block;


public class BE2_Cst_DetectObject : BE2_InstructionBase, I_BE2_Instruction
{

    I_BE2_BlockSectionHeaderInput _input0;
    I_BE2_BlockSectionHeaderInput _input1;
    BE2_InputValues _v0;
    BE2_InputValues _v1;

    public new void Function()
    {

        ExecuteNextInstruction();
    }

    public new string Operation()
    {
        string result = "";
        _input0 = Section0Inputs[0];
        _input1 = Section0Inputs[1];
        _v0 = _input0.InputValues;
        _v1 = _input1.InputValues;

        if (_v1.isText)
        {
            bool isDetected = TargetObject.Player.detector.ObjectDetected(_v1.stringValue);
            if (isDetected)
            {
                result = "true";
            }
            else
            {
                result = "false";
            }
        }

        return result;
    }
}
