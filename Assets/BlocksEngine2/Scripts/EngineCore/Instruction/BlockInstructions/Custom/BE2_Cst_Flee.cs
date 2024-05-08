using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MG_BlocksEngine2.Block.Instruction;
using MG_BlocksEngine2.Block;


public class BE2_Cst_Flee : BE2_InstructionBase, I_BE2_Instruction
{

    public new bool ExecuteInUpdate => true;
    public new void Function()
    {
        Transform enemy = TargetObject.Player.detector.DetectedTransform("Enemy");
        if (enemy != null)
        {
            TargetObject.Player.Flee(enemy);
        }

        if (!TargetObject.Player.detector.ObjectDetected("Enemy"))
        {
            ExecuteNextInstruction();
        }
    }

    public new string Operation()
    {
        string result = "";

        return result;
    }

}
